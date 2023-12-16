using System.Text.RegularExpressions;

namespace AdventOfCode2023.Common.Extensions;

public static class StringExtensions
{
    public static int? GetFirstDigit(this string s)
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

    public static int? GetFirstNumber(this string s) =>
        GetAllNumbers(s).FirstOrDefault();

    public static List<int> GetAllNumbers(this string s)
    {
        var matches = Regex.Matches(s, @"-?\d+");

        return matches.Select(m => int.Parse(m.Value))
            .ToList();
    }

    public static long? GetFirstNumberLong(this string s) =>
        GetAllNumbersLong(s).FirstOrDefault();

    public static List<long> GetAllNumbersLong(this string s)
    {
        var matches = Regex.Matches(s, @"-?\d+");

        return matches.Select(m => long.Parse(m.Value))
            .ToList();
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

    public static bool Equals(this string s, string str, out int mismatchCount, int allowedNumMismatches = int.MaxValue)
    {
        int minLength = Math.Min(s.Length, str.Length);
        mismatchCount = Math.Max(s.Length, str.Length)-minLength;
        if(mismatchCount > allowedNumMismatches)
        {
            return false;
        }

        for (int i = 0; i < minLength; i++)
        {
            if (s[i] != str[i])
            {
                if (++mismatchCount > allowedNumMismatches)
                {
                    return false;
                }
            }
        }

        return true;
    } 
}
