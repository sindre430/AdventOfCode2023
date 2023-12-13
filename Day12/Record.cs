namespace AdventOfCode2023.Day12;

internal class Record(string rawRecord, int id = -1)
{
    public int Id { get; set; } = id;

    public string RawRecord { get; set; } = rawRecord;

    public string Line => 
        RawRecord.Split(' ')[0];

    public string DamagedSpringsPattern =>
        RawRecord.Split(' ')[1];

    public long GetPermutationCount() =>
        GetPermutationCount(Line, DamagedSpringsPattern.Split(',').Select(int.Parse).ToArray());

    private static long GetPermutationCount(string line, int[] damageSpringPattern, int group = 0, int amount = 0, long permutations = 0, int curLinePos = 0)
    {
        var curGroupLength = group < damageSpringPattern.Length ? damageSpringPattern[group] : -1;
        if (curLinePos >= line.Length)
        {
            return (curGroupLength == -1 || (group == damageSpringPattern.Length - 1 && amount == curGroupLength)) ?
                ++permutations : permutations;
        }
        
        var curChar = line[curLinePos];
        if(curChar == '#')
        {
            return ++amount > curGroupLength ? permutations :
                GetPermutationCount(line, damageSpringPattern, group, amount, permutations, curLinePos + 1);
        }

        char? prevChar = curLinePos > 0 ? line[curLinePos - 1] : null;
        if (curChar == '.')
        {
            if(prevChar == '#')
            {
                if(amount < curGroupLength)
                {
                    return permutations;
                }

                group++;
            }

            return GetPermutationCount(line, damageSpringPattern, group, 0, permutations, curLinePos + 1);
        }

        if (curChar != '?')
        {
            throw new InvalidOperationException($"Invalid character in line {curChar}");
        }

        // Current char is '?'
        if(prevChar == '#')
        {
            if(amount < curGroupLength)
            {
                return GetPermutationCount(line[..curLinePos] + '#' + line[(curLinePos + 1)..], damageSpringPattern, group, ++amount, permutations, curLinePos + 1);
            }
            else
            {
                return GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, ++group, 0, permutations, curLinePos + 1);
            }
        }

        if(curGroupLength == -1)
        {
            return GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, group, amount, permutations, curLinePos + 1);
        }

        permutations = GetPermutationCount(line[..curLinePos] + '#' + line[(curLinePos + 1)..], damageSpringPattern, group, ++amount, permutations, curLinePos + 1);
        permutations = GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, group, 0, permutations, curLinePos + 1);

        return permutations;
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
