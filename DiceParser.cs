namespace GeneralNontransitiveDiceGame;

public abstract class DiceParser
{
    public static List<Dice> ParseDice(string[] args)
    {
        var diceList = new List<Dice>();
        foreach (var arg in args)
        {
            try
            {
                var faces = arg.Split(',').Select(int.Parse).ToArray();
                if (faces.Length < 2)
                    throw new ArgumentException("Each die must have at least 2 faces.");
                diceList.Add(new Dice(faces));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Invalid dice configuration: {arg}. {ex.Message}");
            }
        }
        if (diceList.Count < 3)
            throw new ArgumentException("At least 3 dice are required.");
        return diceList;
    }
}