namespace AdventOfCode2023.Day2;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] gamesInput, int maxRedCubes, int maxGreenCubes, int maxBlueCubes)
    {
        var games = gamesInput.Select(gameStr => Game.Parse(gameStr))
            .ToList();

        // Validate games
        var aggregatedSumOfValidGameIds = 0;
        List<string> logLines = new();
        foreach(var game in games)
        {
            logLines.Clear();
            game.Valid = true;
            foreach(var set in game.Sets)
            {
                var validSet =
                    set.NumRedCubes <= maxRedCubes &&
                    set.NumGreenCubes <= maxGreenCubes &&
                    set.NumBlueCubes <= maxBlueCubes;

                logLines.Add($"  {set} ({(validSet ? "Valid" : "Not Valid")})");
                game.Valid = game.Valid && validSet;
            }

            if (game.Valid)
            {
                aggregatedSumOfValidGameIds += game.Id;
            }

            Console.WriteLine($"Game {game.Id} ({(game.Valid ? "Valid" : "Not Valid")}) (Sum of GameIds {aggregatedSumOfValidGameIds})");
            foreach(var line in logLines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }

    public static void Part2(string[] gamesInput)
    {
        var games = gamesInput.Select(gameStr => Game.Parse(gameStr))
             .ToList();

        var aggregatedSumOfGamePowers = 0;
        foreach (var game in games)
        {
            int minRedCubes = 0;
            int minGreenCubes = 0;
            int minBlueCubes = 0;
            
            foreach (var set in game.Sets)
            {
                if (set.NumRedCubes > minRedCubes)
                {
                    minRedCubes = set.NumRedCubes;
                }
                if (set.NumGreenCubes > minGreenCubes)
                {
                    minGreenCubes = set.NumGreenCubes;
                }
                if (set.NumBlueCubes > minBlueCubes)
                {
                    minBlueCubes = set.NumBlueCubes;
                }
            }

            Console.WriteLine($"Game {game.Id} (Min Red: {minRedCubes}, Min Green: {minGreenCubes}, Min Blue: {minBlueCubes})");
            foreach (var set in game.Sets)
            {
                Console.WriteLine($"  Red: {set.NumRedCubes}{(set.NumRedCubes == minRedCubes ? "(*)" : string.Empty)}, " +
                    $"Green: {set.NumGreenCubes}{(set.NumGreenCubes == minGreenCubes ? "(*)" : string.Empty)}, " +
                    $"Blue: {set.NumBlueCubes}{(set.NumBlueCubes == minBlueCubes ? "(*)" : string.Empty)}");
            }

            var power = minRedCubes * minGreenCubes * minBlueCubes;
            aggregatedSumOfGamePowers += power;
            Console.WriteLine($"Min Red: {minRedCubes}, Min Green: {minGreenCubes}, Min Blue: {minBlueCubes} | Power: {power}");
            Console.WriteLine();
        }

        Console.WriteLine($"Sum of Game Powers: {aggregatedSumOfGamePowers}");
        Console.WriteLine();
    }
}