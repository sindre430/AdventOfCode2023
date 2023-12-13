using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day12;

internal class Record(string rawRecord)
{
    public string RawRecord { get; set; } = rawRecord;

    public string Line => 
        RawRecord.Split(' ')[0];

    public string DamagedSpringsPattern =>
        RawRecord.Split(' ')[1];

    public async Task<List<string>> GetAllPossibleCombinations(string line, string damageSpringPattern, string? damageSpringRegexPattern = null)
    {
        List<string> combinations = [];

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

            var lineIsValid = await ValidateLineWithPattern(lineWithOperationalSpring, damageSpringPattern);
            if(lineIsValid.Item1)
            {
                combinations.AddRange(await GetAllPossibleCombinations(lineWithOperationalSpring, damageSpringPattern));
            }

            lineIsValid = await ValidateLineWithPattern(lineWithDamagedSpring, damageSpringPattern);
            if(lineIsValid.Item1)
            {
                var newCombinations = await GetAllPossibleCombinations(lineWithDamagedSpring, damageSpringPattern);
                combinations.AddRange(newCombinations);
            }
        }
        
        return combinations;
    }

    public static async Task<(bool, string?)> ValidateLineWithPattern(string line, string damagedSpringsPattern)
    {
        try
        {
            CancellationTokenSource cancellationTokenSource = new();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));

            var recc = new Record($"{line} {damagedSpringsPattern}");
            var regexStr = "(?:[^#]|^)" + string.Join(@"[\.|\?]+", recc.DamagedSpringsPattern.Split(',').Select(groupLength => $"[#?]{{{groupLength}}}"));
            var regex = new Regex(
                regexStr,
                RegexOptions.Singleline, // Kanskje må denne bort?
                matchTimeout: TimeSpan.FromSeconds(10));
            Match match = regex.Match(line);
            if (match.Success)
            {
                var lineWithoutMatch = line.Remove(match.Index, match.Length);
                if (!match.Value.Contains('?') && lineWithoutMatch.Contains('#'))
                {
                    return (false, regexStr);
                }

                return (true, regexStr);
            }
            else
            {
                return (false, regexStr);
            }
/*

            return (await Task.Run(() =>
            {
                var match = Regex.Match(line, regex);
                if(match.Success)
                {
                    var lineWithoutMatch = line.Remove(match.Index, match.Length);
                    if (!match.Value.Contains('?') && lineWithoutMatch.Contains('#'))
                    {
                        return false;
                    }

                    return true;
                }

                return false;
            }, cancellationTokenSource.Token), regex);*/
        }
        catch(RegexMatchTimeoutException)
        {
            return (ValidateLineWithPatternByParts(line, damagedSpringsPattern), null);
        }
    }

    public static bool ValidateLineWithPatternByParts(string line, string damagedSpringsPattern)
    {
        var damagedSpringsPatternParts = damagedSpringsPattern.Split(',');
        var lineParts = line.Split('.');

        var stringStartIndex = 0;
        var linePartIndex = 0;
        var patternMatch = true;
        for (var i = 0; i < damagedSpringsPatternParts.Length; i++)
        {
            if (linePartIndex >= lineParts.Length)
            {
                patternMatch = false;
                break;
            }

            var curString = lineParts[linePartIndex][stringStartIndex..];
            var regex = $"[.?]?[#?]{{{int.Parse(damagedSpringsPatternParts[i])}}}";
            var match = Regex.Match(curString, regex);
            if (match.Success)
            {
                stringStartIndex += match.Index + match.Length;
            }
            else
            {
                i--;
                linePartIndex++;
                stringStartIndex = 0;
            }
        }

        return patternMatch;
    }
}
