namespace AdventOfCode2023.Day8;

public class MapNode(string rawLine)
{
    public string Name => 
        RawLine.Split('=')[0].Trim();

    public string RawLine { get; set; } = rawLine;

    public MapNode? LeftNode { get; set; }

    public MapNode? RightNode { get; set; }
}
