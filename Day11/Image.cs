using Common.Classes;

namespace AdventOfCode2023.Day11;

internal class Image
{
    public char[][] ImageCharacters { get; set; }
    public List<Position<int>> Galaxies { get; set; } = [];

    public Image(string[] imageLines) : 
        this(GetCharTable(imageLines)) { } 

    public Image(char[][] imageCharacters)
    {
        ImageCharacters = imageCharacters;
        for (int y = 0; y < imageCharacters.Length; y++)
        {
            for (int x = 0; x < imageCharacters[y].Length; x++)
            {
                if (imageCharacters[y][x] == '#')
                {
                    Galaxies.Add(new Position<int>(x, y));
                }
            }
        }
    }

    public static string[] GetImageLines(char[][] charTable) =>
        charTable.Select(c => new string(c)).ToArray();

    public static char[][] GetCharTable(string[] imageLines) =>
        imageLines.Select(l => l.ToCharArray()).ToArray();

    public Image Expand(int emptyLineMultiplier = 2)
    {
        var allColumnsWithoutGalaxies = GetColumnIndexesWhereAllValuesMatchCharacter('.');
        var allRowsWithoutGalaxies = GetRowIndexesWhereAllValuesMatchCharacter('.');

        var stringList = GetImageLines(ImageCharacters).ToList();
        
        // Copy Rows
        // Go backwards to not mess up lower indexes
        for(var i=allRowsWithoutGalaxies.Count-1; i>=0; i--)
        {
            var curRowIndex = allRowsWithoutGalaxies[i];
            var rowToCopy = stringList[curRowIndex];

            for(var j=1; j<emptyLineMultiplier; j++)
            {
                stringList.Insert(curRowIndex, rowToCopy);
            }
        }

        // Copy Columns
        // Go backwards to not mess up lower indexes
        for (var i=allColumnsWithoutGalaxies.Count-1; i>=0; i--)
        {
            var curColumnIndex = allColumnsWithoutGalaxies[i];

            for(var j=0; j<stringList.Count; j++)
            {
                for(var k=1; k<emptyLineMultiplier; k++)
                {
                    stringList[j] = stringList[j].Insert(curColumnIndex, stringList[j][curColumnIndex].ToString());
                }
            }
        }

        return new Image(stringList.ToArray());
    }

    public List<int> GetColumnIndexesWhereAllValuesMatchCharacter(char value)
    {
        var res = new List<int>();
        var numColumns = ImageCharacters.First().Length;
        for(var i=0; i<numColumns; i++)
        {
            var matches = true;
            foreach(var row in ImageCharacters)
            {
                if (matches)
                {
                    matches = row[i].Equals(value);
                }
            }

            if (matches)
            {
                res.Add(i);
            }
        }

        return res;
    }

    public List<int> GetRowIndexesWhereAllValuesMatchCharacter(char value)
    {
        var res = new List<int>();
        for (var i=0; i<ImageCharacters.Length; i++)
        {
            if (ImageCharacters[i].All(v => v.Equals(value)))
            {
                res.Add(i);
            }
        }

        return res;
    }

    public void Print() =>
        GetImageLines(ImageCharacters).ToList()
        .ForEach(Console.WriteLine);
}
