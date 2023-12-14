using System.Text.RegularExpressions;
using AdventOfCode2023.Common.Services;

namespace AdventOfCode2023.Common.Extensions;

public static class ListExtensions
{
    public static List<string> RotateClockwise(this List<string> s) => 
        s.ConvertToCharArray().ConvertToMultiDimensionalArray().RotateMatrixClockwise().ConvertToJaggedArray().Select(x => new string(x)).ToList();

    public static char[][] ConvertToCharArray(this List<string> s)
    {
        var charArray = new char[s.Count][];
        for (int i = 0; i < s.Count; i++)
        {
            charArray[i] = s[i].ToCharArray();
        }
        return charArray;
    }
}
