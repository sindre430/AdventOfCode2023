namespace AdventOfCode2023.Day8;

internal class Map
{
    public readonly List<Direction> directions = [];
    public readonly Dictionary<string, MapNode> nodes = [];

    public MapNode GetNode(string nodeName) => 
        nodes.FirstOrDefault(mn => mn.Value.Name.Equals(nodeName)).Value;
    
    public Map(string[] mapLines)
    {
        var directionLines = mapLines.TakeWhile((l, i) => l != string.Empty);
        directions = directionLines
            .SelectMany(l => l.Select(c => c))
            .Select(c => Enum.Parse<Direction>(c.ToString()))
            .ToList();

        var nodeLines = mapLines.Skip(directionLines.Count() + 1).ToArray();
        foreach(var line in nodeLines)
        {
            var node = new MapNode(line);
            nodes.Add(node.Name, node);
        }

        foreach(var node in nodes.Values)
        {
            var nodeSteps = node.RawLine.Split('=')
                .Last()
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Split(',')
                .Select(s => s.Trim())
                .ToList();

            node.LeftNode = nodes[nodeSteps[0]];
            node.RightNode = nodes[nodeSteps[1]];
        }
    }

    public Direction GetDirection(long index)
    {
        index %= directions.Count;
        return directions[(int)index];
    }
}
