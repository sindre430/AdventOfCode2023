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
}
