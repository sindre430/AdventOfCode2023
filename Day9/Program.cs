namespace AdventOfCode2023.Day9;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] mapLines)
    {
        List<Sequence> sequences = [];
        foreach (var line in mapLines)
        {
            sequences.Add(new Sequence(line));
        }

        var sumAllNextPredictions = sequences.Sum(s => s.PredictNextValue());
        Console.WriteLine($"Sum of all next predictions: {sumAllNextPredictions}");
    }

    public static void Part2(string[] mapLines)
    {
        List<Sequence> sequences = [];
        foreach (var line in mapLines)
        {
            sequences.Add(new Sequence(line));
        }

        var sumAllPrevPredictions = sequences.Sum(s => s.PredictPrevValue());
        Console.WriteLine($"Sum of all prev predictions: {sumAllPrevPredictions}");
    }
}