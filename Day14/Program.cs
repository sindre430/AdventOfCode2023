
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

        var numCycleRuns = 1000000000;
        var foundLoop = false;

        var loop2Start = 0;
        var loopStart = 0;
        for (var i = 1; i <= numCycleRuns; i++)
        {
            runCycle();

            if (!foundLoop && platformHistory.TryGetValue(platform.Id, out int cycleRun))
            {
                foundLoop = true;
                var loopLength = i - cycleRun;
                loopStart = cycleRun;
                Console.WriteLine($"Found Loop. Loop start: {cycleRun}, Loop length: {loopLength}, i: {i}");
                platform.Print();

                var stepsInLoop = (numCycleRuns - cycleRun ) % loopLength;
                Console.WriteLine("Steps in loop: " + stepsInLoop);
                var closesPlatformIndex = loopStart + stepsInLoop;
                Console.WriteLine("Closest platform index: " + closesPlatformIndex);
                i = numCycleRuns - stepsInLoop;
                Console.WriteLine($"i = : {i}");
                //loop2Start = numCycleRuns - (numCycleRuns - i) % loopLength;
                //var lengthBefLoop = cycleRun - 1;
                //  i = numCycleRuns - ((numCycleRuns-lengthBefLoop)%loopLength) -1 ;
                continue;
            }
            else if (!foundLoop)
            {
                platformHistory.Add(platform.Id, i);
            }
        }
        
        platform.Print();
        Console.WriteLine();

        
       /* var plat2 = new Platform(platformLines);
        void runCycle2()
        {
            plat2.Tilt("N");
            plat2.Tilt("W");
            plat2.Tilt("S");
            plat2.Tilt("E");
        }

        for (var i = (loop2Start); i <= numCycleRuns; i++)
        {
            Console.WriteLine($"cycle: {i}");
            runCycle2();
        }
        Console.WriteLine("P2");
        plat2.Print();
        Console.WriteLine();
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