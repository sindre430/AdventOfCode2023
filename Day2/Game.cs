using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day2;

public class Game
{
    public int Id { get; set; }

    public List<GameSet> Sets { get; set; } = new List<GameSet>();

    public bool Valid { get; set; }

    public static Game? Parse(string s)
    {
        // String format:
        // Game *ID*: a blue, b red;x red, y green, z blue
        // : separates game from game sets
        // ; separates game sets
        // , separates number of cubes within a game set
        
        var split1 = s.Split(":");
        var gameId = split1[0].GetFirstNumber();
        if(!gameId.HasValue)
        {
            Console.WriteLine($"** No game id found. String: {s}");
            return null;
        }


        var gameSetsStr = split1[1]?.Split(";") ?? Array.Empty<string>();

        return new Game
        {
            Id = gameId.Value,
            Sets = gameSetsStr.Select(s => GameSet.Parse(s)).ToList(),
            Valid = false
        };
    }
}
