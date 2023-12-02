﻿namespace AdventOfCode2023.Common.Extensions;

public static class StringExtensions
{
    public static int? GetFirstNumber(this string s)
    {
        foreach (var c in s)
        {
            if (char.IsDigit(c))
            {
                return int.Parse(c.ToString());
            }
        }

        return null;
    }

    public static string Reverse(this string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static string ReplaceFromIndexToIndex(this string originalString, int startIndex, int endIndex, string replacement)
    {
        if (startIndex < 0 || endIndex >= originalString.Length || startIndex > endIndex)
        {
            throw new ArgumentException("Invalid indices");
        }

        string before = originalString[..startIndex];
        string after = originalString[(endIndex + 1)..];

        return before + replacement + after;
    }
}
