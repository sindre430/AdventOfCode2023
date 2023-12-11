using Common.Services;

namespace AdventOfCode2023.Day10;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] sketchLines)
    {
        var sketch = new Sketch(sketchLines);
        var loop = sketch.GetLoop('S');
        var farthestPipeFromStart = loop.Pipes.Count / 2;

        Console.WriteLine($"Farthest pipe from start: {farthestPipeFromStart}");
    }

    public static void Part2(string[] sketchLines)
    {
        var sketch = new Sketch(sketchLines);
        var loop = sketch.GetLoop('S');
        var cornerPipes = loop.GetAllCornerPipes();
        var vertices = cornerPipes.Select(p => (p.Position.X, p.Position.Y)).ToList();
        var encosed = EnclosedCoordinatesFinder.GetEnclosedCoordinates(vertices);
        var enclosedTilesWithoutPipeTiles = encosed.Where(e => !loop.Pipes.Any(p => p.Position.X == e.Item1 && p.Position.Y == e.Item2))
            .ToList();

        Console.WriteLine($"Enclosed Tiles: {enclosedTilesWithoutPipeTiles.Count}");
    }
}