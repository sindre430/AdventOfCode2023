using System.Text;

namespace AdventOfCode2023.Day12;

public class Program
{
    public static void Main(string[] args)
    {
        var multiplier = 1;
        if (!string.IsNullOrEmpty(args[1]))
        {
            multiplier = int.Parse(args[1]);
        }

        Part2(File.ReadAllLines(args[0]), multiplier);
    }

    public static void Part1(string[] recordLines)
    {
        var records = recordLines.Select((r,i) => new Record(r, i)).ToList();
        var completedLinesCounter = 0;

        var tasks = records.Select(r =>
        {
            var combinations = r.GetPermutationCount();
            completedLinesCounter++;
            Console.WriteLine($"Line {r.Id + 1}: {combinations} (Lines completed: {completedLinesCounter})");
            return combinations;
        });

        var sumPossibleCombinations = tasks.Sum();

        Console.WriteLine();
        Console.WriteLine($"Sum of possible combinations: {sumPossibleCombinations}");
    }

    public static void Part2(string[] records, int multiplier)
    {
        var sb = new StringBuilder();
        var newRecords = records.Select(r => new Record(r))
            .Select(r => $"{string.Join('?', Enumerable.Repeat(r.Line, multiplier))} {string.Join(',', Enumerable.Repeat(r.DamagedSpringsPattern, multiplier))}")
            .ToArray();

        Part1(newRecords);
    }
}