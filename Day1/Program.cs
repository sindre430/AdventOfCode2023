using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day1;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] args)
    {
        Console.WriteLine("Input Data:");
        var calibrationVaules = new List<int>();
        foreach (string value in args)
        {
            var num1 = value.GetFirstDigit();
            if (num1 == null)
            {
                Console.WriteLine($"No number found in input: {value}");
                continue;
            }

            var valReversed = value.Reverse();
            var num2 = valReversed.GetFirstDigit();
            var calValue = int.Parse($"{num1}{num2}");
            calibrationVaules.Add(calValue);

            Console.WriteLine($"{value}: (First: {num1}, Last: {num2}, Value: {calValue})");
        }

        Console.WriteLine("===================");
        Console.WriteLine($"Sum of values: {calibrationVaules.Sum()}");
    }

    public static void Part2(string[] args)
    {
        Console.WriteLine("Mapped Values:");
        for (var i=0; i<args.Length; i++)
        {
            var curVal = args[i];
            var mappedValue = ReplaceLetterDigits(curVal);
            Console.WriteLine($"{curVal} -> {mappedValue}");
            args[i] = mappedValue;
        }

        Console.WriteLine();
        Part1(args);
    }

    private static string ReplaceLetterDigits(string s)
    {
        var lettersToDigitDict = new Dictionary<string, string> { 
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" }
        };

        var replaceLetterDigits = (string s, bool reverse) =>
        {
            int? curStartIndex = null;
            KeyValuePair<string, string>? curMapping = null;
            for (var i = 0; i < lettersToDigitDict.Count; i++)
            {
                var mapping = lettersToDigitDict.ElementAt(i);
                var startIndex = reverse ? s.LastIndexOf(mapping.Key) : s.IndexOf(mapping.Key);
                if (startIndex > -1 && (curStartIndex == null ||
                    (reverse ? startIndex > curStartIndex : startIndex < curStartIndex)))
                {
                    curStartIndex = startIndex;
                    curMapping = mapping;
                }
            }

            return curMapping == null ? s :
                s.ReplaceFromIndexToIndex(curStartIndex!.Value, 
                    curStartIndex.Value + curMapping.Value.Key.Length - 1,
                    curMapping.Value.Value);
        };

        // Find first and last index of digits
        var firstDigitIndex = -1;
        var lastDigitIndex = -1;

        for (var i=0; i<s.Length; i++)
        {
            if (!char.IsDigit(s[i]))
            {
                continue;
            }

            if (firstDigitIndex == -1)
            {
                firstDigitIndex = i;
            }
            lastDigitIndex = i;
        }

        string result;
        if(firstDigitIndex == -1)
        {
            result = replaceLetterDigits(s, false);
        }
        else
        {
            // Split input string in parts and replace "letter digits"
            var part1 = replaceLetterDigits(s[..firstDigitIndex], false);
            var part2 = s[firstDigitIndex..lastDigitIndex];
            var part3 = replaceLetterDigits(s[lastDigitIndex..], true);

            result = $"{part1}{part2}{part3}";
        }

        // Recursive execution if result is not the same as input string
        return result.Equals(s) ? s :
            ReplaceLetterDigits(result);
    }
}