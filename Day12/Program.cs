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

        Part2(File.ReadAllLines(args[0]), multiplier).GetAwaiter().GetResult();
    }

    public static async Task Part1(string[] recordLines)
    {
        var records = recordLines.Select((r,i) => new Record(r, i)).ToList();
        var completedLinesCounter = 0;
        var tasks = records.Select(r => Task.Run(() =>
        {
            var combinations = r.GetPermutationCount();
            completedLinesCounter++;
            Console.WriteLine($"Line {r.Id+1}: {combinations} (Lines completed: {completedLinesCounter})");
            return combinations;
        }));

        await Task.WhenAll(tasks);

        var sumPossibleCombinations = tasks.Select(t => t.Result).Sum();

        Console.WriteLine();
        Console.WriteLine($"Sum of possible combinations: {sumPossibleCombinations}");

        Console.ReadLine();
    }

    public static async Task Part2(string[] records, int multiplier)
    {
        var sb = new StringBuilder();
        var newRecords = records.Select(r => new Record(r))
            .Select(r => $"{string.Join('?', Enumerable.Repeat(r.Line, multiplier))} {string.Join(',', Enumerable.Repeat(r.DamagedSpringsPattern, multiplier))}")
            .ToArray();

        await Part1(newRecords);
    }
}