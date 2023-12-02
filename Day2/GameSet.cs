using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day2;

public class GameSet
{
    public int NumRedCubes { get; set; }
    public int NumGreenCubes { get; set; }
    public int NumBlueCubes { get; set; }

    public static GameSet Parse(string s)
    {
        // String format:
        // x red, y green, z blue
        // x blue, y red
        // , separates number of cubes within a game set

        var res = new GameSet();

        var parts = s.Split(",");
        foreach(var part in parts)
        {
            if (part.Contains("red", StringComparison.OrdinalIgnoreCase))
            {
                res.NumRedCubes = part.GetFirstNumber() ?? 0;
            }
            else if (part.Contains("green", StringComparison.OrdinalIgnoreCase))
            {
                res.NumGreenCubes = part.GetFirstNumber() ?? 0;
            }
            else if (part.Contains("blue", StringComparison.OrdinalIgnoreCase))
            {
                res.NumBlueCubes = part.GetFirstNumber() ?? 0;
            }
        }

        return res;
    }

    public override string ToString() =>
        $"Red: {NumRedCubes}, Green: {NumGreenCubes}, Blue: {NumBlueCubes}";
}
