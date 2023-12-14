
using AdventOfCode2023.Common.Services;
using System.Security.Cryptography.X509Certificates;

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

        var platformHistory = new Dictionary<long, int>()
        {
            { platform.Id, 0 }
        };

        void runCycle() {
            platform.Tilt("N");
            platform.Tilt("W");
            platform.Tilt("S");
            platform.Tilt("E");
        }
        

        for (var i = 1; i <= 1000000; i++)
        {
            runCycle();
            if(platformHistory.TryGetValue(platform.Id, out int cycleRun))
            {
                Console.WriteLine($"Found Loop. Loop start: {cycleRun}, Loop length: {i - cycleRun}");
                break;
            }

            platformHistory.Add(platform.Id, i);
        }
        /*
        Console.WriteLine();
        platform.Print();

        runCycle();
        Console.WriteLine();
        platform.Print();

        runCycle();
        Console.WriteLine();
        platform.Print();
        runCycle();
        Console.WriteLine();
        platform.Print();
        runCycle();
        Console.WriteLine();
        platform.Print();
        runCycle();
        Console.WriteLine();
        platform.Print();
        */
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