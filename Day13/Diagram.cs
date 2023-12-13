using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day13;

internal class Diagram(List<string> rowLines)
{
    readonly List<string> Rows = rowLines;

    public List<int> FindHorizontalReflectionRowIndex() =>
        FindHorizontalReflectionRowIndex(Rows);

    public List<int> FindVerticalReflectionColumnIndex() =>
        FindVerticalReflectionColumnIndex(Rows);

    public static List<int> FindHorizontalReflectionRowIndex(List<string> rows)
    {
        var res = new List<int>();

        var possibleIndexes = new List<int>();
        for (int i = 0; i < rows.Count - 1; i++)
        {
            if (rows[i].Equals(rows[i + 1]))
            {
                possibleIndexes.Add(i);
            }
        }

        foreach(var index in possibleIndexes)
        {
            var j = index + 1;
            var isMirroring = true;
            for (var i = index; i >= 0; i--, j++)
            {
                if (j >= rows.Count)
                {
                    break;
                }

                if (rows[i] != rows[j])
                {
                    isMirroring = false;
                    break;
                }
            }

            if (isMirroring)
            {
                res.Add(index);
            }
        }

        return res;
    }

    public static List<int> FindVerticalReflectionColumnIndex(List<string> rows)
    {
        var mirroredRows = rows.Mirror('\\');
        return FindHorizontalReflectionRowIndex(mirroredRows);
    }
}
