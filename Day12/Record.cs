using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day12;

internal class Record(string rawRecord)
{
    public string RawRecord { get; set; } = rawRecord;

    public string Line => 
        RawRecord.Split(' ')[0];

    public string DamagedSpringsPattern =>
        RawRecord.Split(' ')[1];

    public List<string> GetAllPossibleCombinations(string line, string damageSpringPattern, string? damageSpringRegexPattern = null)
    {
        List<string> combinations = [];

        if (string.IsNullOrEmpty(damageSpringRegexPattern))
        {
            damageSpringRegexPattern = "[.?]?" + string.Join(@"(\.|\?)*", damageSpringPattern.Split(',').Select(groupLength => $"([#?]{{{groupLength}}})"));
        }

        int index = line.IndexOf('?');
        if (index == -1)
        {
            if(string.Join(',', line.Split('.').Where(p => !string.IsNullOrEmpty(p))
                .Select(p => p.Length)).Equals(damageSpringPattern))
            {
                combinations.Add(line);
            }
        }
        else
        {
            // Replace '?' with either '.' og '#'
            string lineWithOperationalSpring = $"{line[..index]}#{line[(index + 1)..]}";
            string lineWithDamagedSpring = $"{line[..index]}.{line[(index + 1)..]}";

            if (Regex.Match(lineWithOperationalSpring, damageSpringRegexPattern).Success)
            {
                combinations.AddRange(GetAllPossibleCombinations(lineWithOperationalSpring, damageSpringPattern));
            }

            if (Regex.Match(lineWithDamagedSpring, damageSpringRegexPattern).Success)
            {
                combinations.AddRange(GetAllPossibleCombinations(lineWithDamagedSpring, damageSpringPattern));
            }
        }
        
        return combinations;
    }
}
