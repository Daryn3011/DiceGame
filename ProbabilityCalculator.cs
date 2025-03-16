using Spectre.Console;

namespace GeneralNontransitiveDiceGame;

public class ProbabilityCalculator
{
    public static double CalculateProbability(Dice dice1, Dice dice2)
    {
        int wins = 0;
        int total = dice1.Faces.Length * dice2.Faces.Length;
        foreach (var face1 in dice1.Faces)
        foreach (var face2 in dice2.Faces)
            if (face1 > face2)
                wins++;
        return (double)wins / total;
    }

    public static void DisplayProbabilityTable(List<Dice> diceList)
    {
        AnsiConsole.MarkupLine("[bold yellow]Probability of the win for the user:[/]");
        AnsiConsole.MarkupLine("This table shows the probability of the user's dice winning against the computer's dice.");
        AnsiConsole.MarkupLine("Each row represents the user's dice, and each column represents the computer's dice.");
        AnsiConsole.MarkupLine("The diagonal cells (where the user's dice and computer's dice are the same) are marked with '-'.");

        var table = new Table();
        table.Border = TableBorder.Rounded;

        table.AddColumn(new TableColumn("[bold blue]User dice v[/]").Centered());
        for (int i = 0; i < diceList.Count; i++)
        {
            table.AddColumn(new TableColumn($"[bold blue]{string.Join(",", diceList[i].Faces)}[/]").Centered());
        }

        for (int i = 0; i < diceList.Count; i++)
        {
            var row = new List<string>();
            row.Add($"[bold green]{string.Join(",", diceList[i].Faces)}[/]");

            for (int j = 0; j < diceList.Count; j++)
            {
                if (i == j)
                {
                    row.Add("[grey]-[/]");
                }
                else
                {
                    double probability = CalculateProbability(diceList[i], diceList[j]);
                    row.Add($"{probability:F4}");
                }
            }

            table.AddRow(row.ToArray());
        }

        AnsiConsole.Write(table);
    }
}