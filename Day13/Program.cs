using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day13;

public class Program
{
    public static void Main()
    {
    }

    public static void Part1(string[] patternLines, int numSmudges = 0)
    {
        var sum = 0;
        var rawDiagramLines = patternLines.ToList().SeparateByEmptyLines();
        foreach (var rawDiagramLine in rawDiagramLines)
        {
            var diagram = new Diagram(rawDiagramLine);

            var horizontalReflectionRowIndex = diagram.FindHorizontalReflectionRowIndex(numSmudges);
            if(horizontalReflectionRowIndex.Count > 0)
            {
                sum += (horizontalReflectionRowIndex.First()+1)*100;
            }
            var verticalReflectionColumnIndex = diagram.FindVerticalReflectionColumnIndex(numSmudges);
            if(verticalReflectionColumnIndex.Count > 0)
            {
                sum += verticalReflectionColumnIndex.First()+1;
            }
        }

        Console.WriteLine($"Sum: {sum}");
    }

    public static void Part2(string[] patternLines) => 
        Part1(patternLines, 1);
}