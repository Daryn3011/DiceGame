namespace GeneralNontransitiveDiceGame;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            if (args.Length < 2)
                throw new ArgumentException("No dice provided. Usage: dotnet run <dice1> <dice2> ...");
            var diceList = DiceParser.ParseDice(args);
            var gameEngine = new GameEngine(diceList);
            gameEngine.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Example usage: dotnet run 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
        }
    }
}