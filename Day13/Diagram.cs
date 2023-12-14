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

        for (int i = 0; i < rows.Count - 1; i++)
        {
            if (rows[i].Equals(rows[i + 1], out int smudges, allowedNumMismatches: requiredNumSmudges))
            {
                var totSmudges = smudges;
                var isMirroring = true;

                var j = i + 2;
                for (var k = i - 1; k >= 0; k--, j++)
                {
                    if (j >= rows.Count)
                        break;

                    if (!rows[k].Equals(rows[j], out int mismatchCount, allowedNumMismatches: requiredNumSmudges - totSmudges))
                    {
                        isMirroring = false;
                        break;
                    }

                    totSmudges += mismatchCount;
                }

                if (isMirroring && totSmudges == requiredNumSmudges)
                {
                    res.Add(i);
                }
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
