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

    public static List<List<T>> Split<T>(this List<T> list, int chunkSize)
    {
        var splitLists = new List<List<T>>();

        for (int i = 0; i < list.Count; i += chunkSize)
        {
            splitLists.Add(list.GetRange(i, System.Math.Min(chunkSize, list.Count - i)));
        }

        return splitLists;
    }

    public static List<string> Mirror(this List<string> input, char mirrorLine)
    {
        if (input == null || input.Count == 0)
        {
            throw new ArgumentException("Input list should not be null or empty.");
        }

        if(input.Any(s => s.Length != input.Count))
        {
           // throw new ArgumentException("All string in list must have the same length as numbers of strings in the list");
        }

        var mirrored = new List<string>();

        if (mirrorLine == '|')
        {
            for (var row = 0; row < input.Count; row++)
            {
                mirrored.Add(new string(input[row].Reverse().ToArray()));
            }
        }
        else if(mirrorLine == '-')
        {
            for(var row=input.Count-1; row>=0; row--)
            {
                mirrored.Add(input[row]);
                //mirrored[input.Count-row-1] = input[row];
            }
        }
        else if (mirrorLine == '\\')
        {
            for(var row=0; row<input.Count; row++)
            {
                for(var col=0; col<input[row].Length; col++)
                {
                    if(row == 0)
                    {
                        mirrored.Add("");
                    }
                    mirrored[col] += input[row][col];
                }
            }
        }
        else if(mirrorLine == '/')
        {
            for(var row=input.Count-1; row>=0; row--)
            {
                var colLength = input[row].Length;
                for(var col=colLength-1; col>=0; col--)
                {
                    mirrored[colLength-col-1] += input[row][col];
                }
            }
        }
        else
        {
            throw new ArgumentException($"Invalid mirror line: {mirrorLine}. Supported: '|', '-', '\\', '/'");
        }
        
        return mirrored;
    }

    public static List<List<string>> SeparateByEmptyLines(this List<string> inputList)
    {
        List<List<string>> separatedLists = [];
        List<string> tempList = [];

        foreach (string str in inputList)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (tempList.Count != 0)
                {
                    separatedLists.Add(tempList);
                    tempList = [];
                }
            }
            else
            {
                tempList.Add(str);
            }
        }

        if (tempList.Any())
        {
            separatedLists.Add(tempList);
        }

        return separatedLists;
    }
}
