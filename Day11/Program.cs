using Day11;

namespace AdventOfCode2023.Day11;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] imageLines)
    {
        var image = new Image(imageLines);
        image.Print();

        Console.WriteLine();
        var expandedImage = image.Expand();
        expandedImage.Print();
    }

    public static void Part2(string[] imageLines)
    {
    }
}