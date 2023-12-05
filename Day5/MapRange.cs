namespace AdventOfCode2023.Day5;

internal class MapRange
{
    public long SourceRangeStart { get; set; }

    public long DestinationRangeStart { get; set; }

    public long RangeLength { get; set; }

    public MapRange(string rawLine)
    {
        var splitLine = rawLine.Split(" ");
        SourceRangeStart = long.Parse(splitLine[1]);
        DestinationRangeStart = long.Parse(splitLine[0]);
        RangeLength = long.Parse(splitLine[2]);
    }
}
