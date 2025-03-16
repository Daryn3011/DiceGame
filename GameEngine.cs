using System.Security.Cryptography;

namespace GeneralNontransitiveDiceGame;

public class GameEngine
{
    private readonly List<Dice> _diceList;
    private readonly FairRandomGenerator _fairRandom;

    public GameEngine(List<Dice> diceList)
    {
        _diceList = diceList;
        _fairRandom = new FairRandomGenerator();
    }

    private bool DetermineFirstMove()
    {
        Console.WriteLine("Let's determine who makes the first move.");
        var (number, hmac) = _fairRandom.GenerateFairNumber(1);
        Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={hmac}).");
        Console.WriteLine("Try to guess my selection.");
        Console.WriteLine("0 - 0");
        Console.WriteLine("1 - 1");
        Console.WriteLine("X - exit");
        Console.WriteLine("? - help");
        var userChoice = GetUserInput(0, 1);
        var result = _fairRandom.ComputeResult(userChoice, 1);
        Console.WriteLine($"My selection: {result} (KEY={BitConverter.ToString(_fairRandom.GetKey()).Replace("-", "").ToLower()}).");
        return result != userChoice;
    }

    public void Play()
    {
        bool computerMakesFirstMove = DetermineFirstMove();

        var availableDice = _diceList.Select((dice, index) => (dice, index)).ToList();

        var firstPlayerDice = computerMakesFirstMove
            ? ComputerSelectDice(availableDice, "I make the first move and choose the")
            : UserSelectDice(availableDice);
        availableDice.RemoveAll(x => x.index == firstPlayerDice.index);

        var secondPlayerDice = computerMakesFirstMove
            ? UserSelectDice(availableDice)
            : ComputerSelectDice(availableDice, "I choose the");

        var firstThrow = PerformThrow(firstPlayerDice.dice, computerMakesFirstMove ? "Computer" : "User");
        var secondThrow = PerformThrow(secondPlayerDice.dice, computerMakesFirstMove ? "User" : "Computer");

        if (firstThrow > secondThrow)
        {
            Console.WriteLine(computerMakesFirstMove
                ? $"You lose ({firstThrow} > {secondThrow})!"
                : $"You win ({firstThrow} > {secondThrow})!");
        }
        else if (secondThrow > firstThrow)
        {
            Console.WriteLine(computerMakesFirstMove
                ? $"You win ({secondThrow} > {firstThrow})!"
                : $"You lose ({secondThrow} > {firstThrow})!");
        }
        else
        {
            Console.WriteLine($"It's a tie ({firstThrow} = {secondThrow})!");
        }
    }

    private (Dice dice, int index) ComputerSelectDice(List<(Dice dice, int index)> availableDice, string message)
    {
        var index = RandomNumberGenerator.GetInt32(availableDice.Count);
        Console.WriteLine($"{message} [{string.Join(",", availableDice[index].dice.Faces)}] dice.");
        return availableDice[index];
    }

    private (Dice dice, int index) UserSelectDice(List<(Dice dice, int index)> availableDice)
    {
        Console.WriteLine("Choose your dice:");
        for (int i = 0; i < availableDice.Count; i++)
            Console.WriteLine($"{i} - {string.Join(",", availableDice[i].dice.Faces)}");
        var choice = GetUserInput(0, availableDice.Count - 1);
        Console.WriteLine($"You choose the [{string.Join(",", availableDice[choice].dice.Faces)}] dice.");
        return availableDice[choice];
    }

    private (Dice dice, int index) ComputerSelectDice(List<(Dice dice, int index)> availableDice)
    {
        var index = RandomNumberGenerator.GetInt32(availableDice.Count);
        Console.WriteLine($"I make the first move and choose the [{string.Join(",", availableDice[index].dice.Faces)}] dice.");
        return availableDice[index];
    }

    private int PerformThrow(Dice dice, string player)
    {
        var fairRandom = new FairRandomGenerator();
        var (number, hmac) = fairRandom.GenerateFairNumber(dice.Faces.Length - 1);

        if (player == "Computer")
        {
            Console.WriteLine("It's time for my roll.");
        }
        else
        {
            Console.WriteLine("It's time for your roll.");
        }

        Console.WriteLine($"I selected a random value in the range 0..{dice.Faces.Length - 1} (HMAC={hmac}).");
        Console.WriteLine($"Add your number modulo {dice.Faces.Length}.");
        for (int i = 0; i < dice.Faces.Length; i++)
            Console.WriteLine($"{i} - {i}");
        Console.WriteLine("X - exit");
        Console.WriteLine("? - help");

        var userNumber = GetUserInput(0, dice.Faces.Length - 1);
        var result = fairRandom.ComputeResult(userNumber, dice.Faces.Length - 1);

        Console.WriteLine($"My number is {number} (KEY={BitConverter.ToString(fairRandom.GetKey()).Replace("-", "").ToLower()}).");
        Console.WriteLine($"The fair number generation result is {number} + {userNumber} = {result} (mod {dice.Faces.Length}).");

        var rollResult = dice.Faces[result];
        if (player == "Computer")
        {
            Console.WriteLine($"My roll result is {rollResult}.");
        }
        else
        {
            Console.WriteLine($"Your roll result is {rollResult}.");
        }

        return rollResult;
    }

    private int GetUserInput(int min, int max)
    {
        while (true)
        {
            Console.Write("Your selection: ");
            var input = Console.ReadLine();
            if (input == "?")
            {
                ProbabilityCalculator.DisplayProbabilityTable(_diceList);
                continue;
            }
            if (input == "X")
                Environment.Exit(0);
            if (int.TryParse(input, out var choice) && choice >= min && choice <= max)
                return choice;
            Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
        }
    }
}