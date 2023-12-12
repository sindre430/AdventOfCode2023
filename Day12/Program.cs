using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day12;

public class Program
{
    public static void Main() { }

    public static async Task Part1(string[] recordLines)
    {
        var records = recordLines.Select(r => new Record(r)).ToList();

        var tasks = records.Select(r => Task.Run(() => r.GetAllPossibleCombinations(r.Line, r.DamagedSpringsPattern)));
        await Task.WhenAll(tasks);

        var sumPossibleCombinations = tasks.Select(t => t.Result.Count).Sum();

        Console.WriteLine();
        Console.WriteLine($"Sum of possible combinations: {sumPossibleCombinations}");
    }

    public static async Task Part2(string[] records)
    {
        var sb = new StringBuilder();
        var newRecords = records.Select(r => new Record(r))
            .Select(r => $"{string.Join('?', Enumerable.Repeat(r.Line, 5))} {string.Join(',', Enumerable.Repeat(r.DamagedSpringsPattern, 5))}")
            .ToArray();

        foreach(var rec in newRecords)
        {
            if (!rec.StartsWith("???????"))
            {
                continue;
            }

            var recc = new Record(rec);
            var regex = "[.?]?"+string.Join(@"[\.|\?]*", recc.DamagedSpringsPattern.Split(',').Select(groupLength => $"[#?]{{{groupLength}}}"));
            if (!Regex.Match(recc.Line, regex).Success)
            {
                var a = 0;
            }
        }

        await Part1(newRecords);
    }
}