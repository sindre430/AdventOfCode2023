using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day6;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] raceInfo)
    {
        // Get time and distance info lines
        var timeInfoLine = raceInfo.FirstOrDefault(l => l.StartsWith("Time:")) ??
            throw new Exception("Time info not found");
        var distanceInfoLine = raceInfo.FirstOrDefault(l => l.StartsWith("Distance:")) ??
            throw new Exception("Distance info not found");
       
        // Get time and distance numbers
        var timeNumbers = timeInfoLine.GetAllNumbersLong();
        var distanceNumbers = distanceInfoLine.GetAllNumbersLong();
        if(timeNumbers.Count != distanceNumbers.Count)
        {
            throw new Exception("Time and Distance numbers count mismatch");
        }

        // Create Race objects
        var races = new List<Race>();
        for(var i=0; i<timeNumbers.Count; i++)
        {
            races.Add(new Race
            {
                Id = i,
                TimeInMs = timeNumbers[i],
                RecordDistanceInMm = distanceNumbers[i]
            });
        }

        // Get winning options
        var winningOptionsCombinationCount = 1;
        foreach(var race in races)
        {
            var winningOptions = race.GetWinningOptions();
            winningOptionsCombinationCount *= winningOptions.Count;
            Console.WriteLine($"Race {race.Id}: {winningOptions.Count} Winning Options");
        }

        // Print out the results
        Console.WriteLine();
        Console.WriteLine($"Winning Options Combination Count: {winningOptionsCombinationCount}");
    }

    public static void Part2(string[] raceInfo)
    {
        Part1(raceInfo);
    }
}