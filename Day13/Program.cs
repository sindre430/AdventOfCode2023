using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day13;

public class Program
{
    public static void Main(string[] args)
    {
    }

    public static async Task Part1(string[] patternLines)
    {
        var sum = 0;
        var rawDiagramLines = patternLines.ToList().SeparateByEmptyLines();
        foreach (var rawDiagramLine in rawDiagramLines)
        {
            var diagram = new Diagram(rawDiagramLine);

            var horizontalReflectionRowIndex = diagram.FindHorizontalReflectionRowIndex();
            if(horizontalReflectionRowIndex.Count > 0)
            {
                sum += (horizontalReflectionRowIndex.First()+1)*100;
            }
            var verticalReflectionColumnIndex = diagram.FindVerticalReflectionColumnIndex();
            if(verticalReflectionColumnIndex.Count > 0)
            {
                sum += verticalReflectionColumnIndex.First()+1;
            }
        }

        Console.WriteLine($"Sum: {sum}");
    }

    public static async Task Part2(string[] patternLines)
    {


        await Task.Delay(1);
    }
}