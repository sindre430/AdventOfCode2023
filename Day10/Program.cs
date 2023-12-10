namespace AdventOfCode2023.Day10;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] sketchLines)
    {
        var sketch = new Sketch(sketchLines);
        var loop = sketch.GetLoop('S');
        var farthestPipeFromStart = loop.Count / 2;

        Console.WriteLine($"Farthest pipe from start: {farthestPipeFromStart}");
    }

    public static void Part2(string[] sketchLines)
    {
        var sketch = new Sketch(sketchLines);
        var sections = sketch.GetGroundSections();
    }
}