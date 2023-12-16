namespace AdventOfCode2023.Day12;

internal class Record(string rawRecord, int id = -1)
{
    public static readonly Dictionary<string, Dictionary<string, long>> GroupLineCache = [];
    
    public int Id { get; set; } = id;

    public string RawRecord { get; set; } = rawRecord;

    public string Line =>
        RawRecord.Split(' ')[0];

    public string DamagedSpringsPattern =>
        RawRecord.Split(' ')[1];

    public long GetPermutationCount() =>
        GetCombinationsCount(Line, DamagedSpringsPattern.Split(',').Select(int.Parse).ToArray());

    public static void AddToCache(int[] groupPatterns, string line, int combinations)
    {
        if(string.IsNullOrEmpty(line))
        {
            return;
        }
        var groupId = GetGroupPatternId(groupPatterns);
        var curGroupLineCache = GroupLineCache.GetValueOrDefault(groupId);
        if (curGroupLineCache == null)
        {
            curGroupLineCache = new Dictionary<string, long> { { line, combinations } };
            GroupLineCache.Add(groupId, curGroupLineCache);
        }

        curGroupLineCache[line] = combinations;
    }

    public static long GetFromCache(int[] groupPatterns, string line)
    {
        var groupId = GetGroupPatternId(groupPatterns);
        var curGroupLineCache = GroupLineCache.GetValueOrDefault(groupId);
        return curGroupLineCache == null ? -1 :
            curGroupLineCache.GetValueOrDefault(line, -1);
    }

    private static string GetGroupPatternId(int[] groupPatterns) =>
        string.Join(',', groupPatterns);

    private static long GetCombinationsCount(string line, int[] damageSpringPattern, int group = 0, int amount = 0, int curLinePos = 0)
    {
        var curGroupPatterns = damageSpringPattern[group..];
        var curGroup = curGroupPatterns.Any() ? curGroupPatterns.First() : -1;

        // Reached end of line. If line is OK return 1, otherwise 0
        if (curLinePos >= line.Length)
        {
            return (curGroup == -1 || (group == damageSpringPattern.Length - 1 && amount == curGroup)) ?
                1 : 0;
        }

        var curLine = line[curLinePos..];
        if (curLine.Equals(".??.###"))
        {
            var i = 0;
        }
        var curChar = curLine.ElementAtOrDefault(0);
        long combinations;

        // If we have note started processing a group, we can check cahce for the current line
        // Return if line is found in cache
        if (amount == 0)
        {
            var cacheValue = GetFromCache(curGroupPatterns, curLine);
            if (cacheValue >= 0)
            {
                Console.WriteLine($"Found cached value for group #x{curGroup} and line {curLine} (Combinations: {cacheValue}) (Pattern: {GetGroupPatternId(curGroupPatterns)})");
                return cacheValue;
            }
        }

        // If current char is a '#' we either start processing new group or increase the amount of '#'s in current group prosessing
        if (curChar == '#')
        {
            // If ammount of '#'s is greater than the current group length, we set combinations to 0 (Bad line)
            if (amount+1 > curGroup)
            {
                combinations = 0;
                Console.WriteLine($"CurChar is '#'. amount is greater than group. Return 0");
                // AddToCache(curGroupPatterns, curLine, (int)combinations);
                return combinations;
            }

            // If next char is '.', marking end of group, and amount of '#'s is less than the current group length, we set combinations to 0 (Bad line)
            
            var nextChar = line.ElementAtOrDefault(curLinePos + 1);
            if(nextChar == '.')
            {
                if(amount+1 < curGroup)
                {
                    combinations = 0;
                    Console.WriteLine($"CurChar is '#', next is '.'. Group required more hashes. Return 0");
                    //AddToCache(curGroupPatterns, curLine, (int)combinations);
                    return combinations;
                }

                Console.WriteLine($"CurChar is '#', next is '#'. Increase group, clear amount");
                combinations = GetCombinationsCount(line, damageSpringPattern, ++group, 0, curLinePos + 1);
                Console.WriteLine($" {line} has {combinations} combinations");
            }
            else
            {
                Console.WriteLine($"CurChar is '#', next is either '?' or '#'");
                combinations = GetCombinationsCount(line, damageSpringPattern, group, amount+1, curLinePos + 1);
            }
        }

        // If cur char is '.' and previous char was '#' we stop processing current group
        // If the amount of '#'s in current group is less than the group length, we set combinations to 0 (Bad line)
     /*   char? prevChar = curLinePos > 0 ? line[curLinePos - 1] : null;
        if (curChar == '.')
        {
          //  amount = 0;
          //  combinations = GetPermutationCount(line, damageSpringPattern, group, 0, curLinePos + 1);
        }*/

        // If cur char is '?' try to process both cases: '#' and '.'
        else if(curChar == '?')
        {
            var hashLine = line[..curLinePos] + '#' + line[(curLinePos + 1)..];
            var dotLine = line[..curLinePos] + '.' + line[(curLinePos + 1)..];

            Console.WriteLine($"CurChar is '?'. Try '#'. {hashLine}");
            var hashCombinations = GetCombinationsCount(hashLine, damageSpringPattern, group, amount, curLinePos);
            Console.WriteLine($"CurChar is '?'. Try '.'. {dotLine}");
            var dotCombinations = GetCombinationsCount(dotLine, damageSpringPattern, group, amount, curLinePos);

            combinations = hashCombinations + dotCombinations;
            Console.WriteLine($"HashLine {hashLine} has {hashCombinations} combinations (Pattern: {GetGroupPatternId(curGroupPatterns)})");
            Console.WriteLine($"DotLine {dotLine} has {dotCombinations} combinations (Pattern: {GetGroupPatternId(curGroupPatterns)})");
        }
        else
        {
            if (amount > 0)
            {
                // Validate prev group
                if(amount < curGroup)
                {
                    combinations = 0;
                    Console.WriteLine($"CurChar is '{curChar}'. Stopped group processing. Not enough '#'. Return 0");
                    // AddToCache(curGroupPatterns, curLine, (int)combinations);
                    return combinations;
                }
                group++;
                amount = 0;
                Console.WriteLine($"Curchar is '{curChar}'. Increase group and clear amount");
            }
            combinations = GetCombinationsCount(line, damageSpringPattern, group, amount, curLinePos + 1);

            Console.WriteLine($"Line {line} has {combinations} combinations");
        }

        try
        {
            var cacheLine = line[(line.Length - (curLine.Length + amount))..];
            if (cacheLine.Equals(".?.###"))
            {
                var i = 0;
            }
            Console.WriteLine($"Add line {cacheLine} pattern: {GetGroupPatternId(damageSpringPattern[group..])}  to cache with combinations {combinations}");
            AddToCache(damageSpringPattern[group..], cacheLine, (int)combinations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
       
        return combinations;

        /*
                if (curChar != '?')
                {
                    throw new InvalidOperationException($"Invalid character in line {curChar}");
                }

                // Current char is '?'
                if (prevChar == '#')
                {
                    if (amount < curGroup)
                    {
                        return GetPermutationCount(line[..curLinePos] + '#' + line[(curLinePos + 1)..], damageSpringPattern, group, ++amount, permutations, curLinePos + 1);
                    }
                    else
                    {
                        var count = GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, group, 0, permutations, curLinePos + 1);
                        return count;
                    }
                }

                if (curGroup == -1)
                {
                    return GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, group, amount, permutations, curLinePos + 1);
                }

                permutations = GetPermutationCount(line[..curLinePos] + '#' + line[(curLinePos + 1)..], damageSpringPattern, group, ++amount, permutations, curLinePos + 1);
                permutations = GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, group, 0, permutations, curLinePos + 1);


                // var curLine = line[curLinePos..];
                var curGroupLineCache = GroupLineCache.ElementAtOrDefault(curGroup);
                if (curGroupLineCache == null)
                {
                    curGroupLineCache = new Dictionary<string, int>();
                    GroupLineCache.Add(curGroupLineCache);
                }

                if (curLine.Equals("???"))
                {
                    var i = 0;
                }
                curGroupLineCache[curLine] = (int)(permutations - oldPermutations);


                return permutations;*/
    }

    /*
     *  private static long GetPermutationCount(string line, int[] damageSpringPattern, int group = 0, int amount = 0, long permutations = 0, int curLinePos = 0)
    {
        void AddToCache(int group, string line, int permutations)
        {
            var curGroupLineCache = GroupLineCache.ElementAtOrDefault(group);
            if (curGroupLineCache == null)
            {
                curGroupLineCache = new Dictionary<string, int>();
                GroupLineCache.Add(curGroupLineCache);
            }

            curGroupLineCache[line] = permutations;
        }
        
        var oldPermutations = permutations;
        var curGroupLength = group < damageSpringPattern.Length ? damageSpringPattern[group] : -1;
        if (curLinePos >= line.Length)
        {
            return (curGroupLength == -1 || (group == damageSpringPattern.Length - 1 && amount == curGroupLength)) ?
                ++permutations : permutations;
        }

            var curLine = line[curLinePos..];
        if(amount == 0)
        {
            var cached = GroupLineCache.ElementAtOrDefault(curGroupLength)?.GetValueOrDefault(curLine);
            if (cached != null)
            {
                Console.WriteLine($"Found cached value for group {curGroupLength} and line {curLine} (Combinations: {cached.Value})");
                return cached.Value;
            }
        }
        

        var curChar = line[curLinePos];
        if (curChar == '#')
        {
            return ++amount > curGroupLength ? permutations :
                GetPermutationCount(line, damageSpringPattern, group, amount, permutations, curLinePos + 1);
        }

        char? prevChar = curLinePos > 0 ? line[curLinePos - 1] : null;
        if (curChar == '.')
        {
            if (prevChar == '#')
            {
                if (amount < curGroupLength)
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
        if (prevChar == '#')
        {
            if (amount < curGroupLength)
            {
                return GetPermutationCount(line[..curLinePos] + '#' + line[(curLinePos + 1)..], damageSpringPattern, group, ++amount, permutations, curLinePos + 1);
            }
            else
            {
                var count = GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, group, 0, permutations, curLinePos + 1);
                return count;
            }
        }

        if (curGroupLength == -1)
        {
            return GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, group, amount, permutations, curLinePos + 1);
        }

        permutations = GetPermutationCount(line[..curLinePos] + '#' + line[(curLinePos + 1)..], damageSpringPattern, group, ++amount, permutations, curLinePos + 1);
        permutations = GetPermutationCount(line[..curLinePos] + '.' + line[(curLinePos + 1)..], damageSpringPattern, group, 0, permutations, curLinePos + 1);

    
           // var curLine = line[curLinePos..];
            var curGroupLineCache = GroupLineCache.ElementAtOrDefault(curGroupLength);
            if (curGroupLineCache == null)
            {
                curGroupLineCache = new Dictionary<string, int>();
                GroupLineCache.Add(curGroupLineCache);
            }

        if (curLine.Equals("???"))
        {
            var i = 0;
        }
           curGroupLineCache[curLine] = (int)(permutations - oldPermutations);
       

        return permutations;
    }
     * */
}
