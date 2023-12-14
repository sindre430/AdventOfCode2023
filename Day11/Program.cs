namespace AdventOfCode2023.Day11;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] imageLines)
    {
        var image = new Image(imageLines);
        var expandedImage = image.Expand();

        var sum = 0;
        for(var i=0; i<expandedImage.Galaxies.Count; i++)
        {
            var curGalaxy = expandedImage.Galaxies[i];
            var galaxiesToPair = expandedImage.Galaxies.Skip(i+1);
            foreach(var galaxyToPair in galaxiesToPair)
            {
                sum += curGalaxy.ManhattanDistance(galaxyToPair);
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Sum of distances between galaxies: {sum}");
    }

    public static void Part2(string[] imageLines, int emptyLineMultiplier)
    {
        var image = new Image(imageLines);
        var rowsWithoutGalaxies = image.GetRowIndexesWhereAllValuesMatchCharacter('.');
        var columnsWithoutGalaxies = image.GetColumnIndexesWhereAllValuesMatchCharacter('.');

        long sum = 0;
        for (var i = 0; i < image.Galaxies.Count; i++)
        {
            var curGalaxy = image.Galaxies[i];
            var galaxiesToPair = image.Galaxies.Skip(i + 1);
            foreach (var galaxyToPair in galaxiesToPair)
            {
                var distance = curGalaxy.ManhattanDistance(galaxyToPair);
                
                // Add column expansion distance
                var smallestColumnIndex = Math.Min(curGalaxy.X, galaxyToPair.X);
                var largestColumnIndex = Math.Max(curGalaxy.X, galaxyToPair.X);
                var numberOfColumnsToExpand = columnsWithoutGalaxies.Count(c => c > smallestColumnIndex && c < largestColumnIndex);
                distance += numberOfColumnsToExpand * (emptyLineMultiplier-1);

                // Add row expansion distance
                var smallestRowIndex = Math.Min(curGalaxy.Y, galaxyToPair.Y);
                var largestRowIndex = Math.Max(curGalaxy.Y, galaxyToPair.Y);
                var numberOfRowsToExpand = rowsWithoutGalaxies.Count(c => c > smallestRowIndex && c < largestRowIndex);
                distance += numberOfRowsToExpand * (emptyLineMultiplier - 1);

                sum += distance;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Sum of distances between galaxies: {sum}");
    }
}