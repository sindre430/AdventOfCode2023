using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day13;

internal class Diagram(List<string> rowLines)
{
    readonly List<string> Rows = rowLines;

    public List<int> FindHorizontalReflectionRowIndex(int requiredNumSmudges = 0) =>
        FindHorizontalReflectionRowIndex(Rows, requiredNumSmudges);

    public List<int> FindVerticalReflectionColumnIndex(int requiredNumSmudges = 0) =>
        FindVerticalReflectionColumnIndex(Rows, requiredNumSmudges);

    public static List<int> FindHorizontalReflectionRowIndex(List<string> rows, int requiredNumSmudges = 0)
    {
        var res = new List<int>();

        var possibleIndexes = new Dictionary<int, int>();
        for (int i = 0; i < rows.Count - 1; i++)
        {
            if (rows[i].Equals(rows[i + 1], out int smudges, allowedNumMismatches: requiredNumSmudges))
            {
                possibleIndexes.Add(i, smudges);
            }
        }

        foreach(var entry in possibleIndexes)
        {
            var index = entry.Key;
            var totSmudges = entry.Value;

            var j = index + 2;
            var isMirroring = true;
            for (var i = index-1; i >= 0; i--, j++)
            {
                if (j >= rows.Count)
                {
                    break;
                }

                if (!rows[i].Equals(rows[j], out int smudges, allowedNumMismatches: requiredNumSmudges-totSmudges))
                {
                    isMirroring = false;
                    break;
                }
                totSmudges += smudges;
            }

            if (isMirroring && totSmudges == requiredNumSmudges)
            {
                res.Add(index);
            }
        }

        return res;
    }

    public static List<int> FindVerticalReflectionColumnIndex(List<string> rows, int requiredNumSmudges = 0)
    {
        var mirroredRows = rows.Mirror('\\');
        return FindHorizontalReflectionRowIndex(mirroredRows, requiredNumSmudges);
    }
}
