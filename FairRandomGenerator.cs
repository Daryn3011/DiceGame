using System.Security.Cryptography;
using System.Text;

namespace GeneralNontransitiveDiceGame;

public class FairRandomGenerator
{
    private byte[] _key = new byte[32];
    private int _number;

    public (int number, string hmac) GenerateFairNumber(int rangeMax)
    {
        RandomNumberGenerator.Fill(_key);

        _number = RandomNumberGenerator.GetInt32(rangeMax + 1);

        var hmac = GenerateHmac(_number);
        return (_number, hmac);
    }

    public string GenerateHmac(int number)
    {
        using (var hmac = new HMACSHA256(_key))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(number.ToString()));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    public int ComputeResult(int userNumber, int rangeMax)
    {
        return (_number + userNumber) % (rangeMax + 1);
    }

    public byte[] GetKey() => _key;
}