namespace AdventOfCode2023.Day5;

internal class Mapper
{
    public EntityType SourceType { get; set; }
    
    public EntityType DestinationType { get; set; }

    List<MapRange> Ranges { get; set; } = [];

    public Mapper(string[] mapLines)
    {
        var mapType = mapLines.FirstOrDefault() ??
            throw new InvalidOperationException("No MapType found");

        var mapTypeSplit = mapType.Replace("map:", string.Empty)
            .Split("-to-")
            .Select(s => s.Trim())
            .ToArray();
        SourceType = Enum.Parse<EntityType>(mapTypeSplit[0], ignoreCase: true);
        DestinationType = Enum.Parse<EntityType>(mapTypeSplit[1], ignoreCase: true);

        for(var i=1; i<mapLines.Length; i++)
        {
            Ranges.Add(new MapRange(mapLines[i]));
        }
    }

    public long MapSourceToDestination(long number)
    {
        var mapRange = Ranges.FirstOrDefault(mr => number >= mr.SourceRangeStart && number < mr.SourceRangeEnd);
        if(mapRange == null)
        {
            return number;
        }

        var startDiff = number - mapRange.SourceRangeStart;
        return mapRange.DestinationRangeStart + startDiff;
    }
}
