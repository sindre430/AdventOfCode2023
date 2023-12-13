using Common.Extensions;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2023.Day12;

/*
    TODO: 
    - Sett antall tråder i parallell 
    - terminer tråd for regex etter 5 sekunder...
*/

public class Program
{
    public static void Main() { }

    public static async Task Part1(string[] recordLines)
    {
        var records = recordLines.Select(r => new Record(r)).ToList();

        var tasks = records.Select(r => Task.Run(async () => 
            await r.GetAllPossibleCombinations(r.Line, r.DamagedSpringsPattern))).ToList();
        var taskLists = tasks.Split(5);

        var totalNumberOfTasks = tasks.Count;
        var currentTaskNumber = 0;
        foreach (var taskList in taskLists)
        {
            await Task.WhenAll(taskList);
            currentTaskNumber += taskList.Count;
            Debug.WriteLine($"Finished {currentTaskNumber}/{totalNumberOfTasks} tasks");
        }

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

        await Part1(newRecords);
    }
}