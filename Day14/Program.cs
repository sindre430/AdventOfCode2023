
using AdventOfCode2023.Common.Services;

namespace AdventOfCode2023.Day14;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] platformLines)
    {
        var platform = new Platform(platformLines);

        platform.Tilt("N");

        platform.Print();

        var sum = 0;
        var numLines = platform.Lines.Count;
        for(var i=0; i<numLines; i++)
        {
            var rocks = platform.Lines[i].Count(c => c == 'O');
            sum += rocks * (numLines - i);
        }

        Console.WriteLine();
        Console.WriteLine($"Sum: {sum}");
    }

    public static void Part2(string[] platformLines)
    {
        var platform = new Platform(platformLines);

        void runCycle() {
            platform.Tilt("N");
            platform.Tilt("W");
            platform.Tilt("S");
            platform.Tilt("E");
        }
        platform.Print();

        for (var i = 0; i < 1000000000; i++)
        {
            runCycle();
        }

        Console.WriteLine();
        platform.Print();

        var sum = 0;
        var numLines = platform.Lines.Count;
        for (var i = 0; i < numLines; i++)
        {
            var rocks = platform.Lines[i].Count(c => c == 'O');
            sum += rocks * (numLines - i);
        }

        Console.WriteLine();
        Console.WriteLine($"Sum: {sum}");
    }
}