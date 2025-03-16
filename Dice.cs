using System.Security.Cryptography;

namespace GeneralNontransitiveDiceGame;

public class Dice
{
    public int[] Faces { get; }

    public Dice(int[] faces)
    {
        Faces = faces;
    }

    public int Roll()
    {
        return Faces[RandomNumberGenerator.GetInt32(Faces.Length)];
    }
}