using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day4;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] cardLines)
    {
        var cards = cardLines.Select(cl => new Card(cl))
            .ToList();

        foreach(var card in cards)
        {
            var cardPoints = card.GetPoints();
            Console.WriteLine($"{card.RawLine} (Points: {cardPoints}) {(cardPoints > 0 ? $"(Numbers: {string.Join(", ", card.GetWinningNumbers())})" : string.Empty)}");
        }

        var points = cards.Select(c => c.GetPoints())
            .Aggregate((sum, val) => sum + val);

        Console.WriteLine();
        Console.WriteLine($"Total points: {points}");
    }

    public static void Part2(string[] cardLines)
    {
        
    }
}