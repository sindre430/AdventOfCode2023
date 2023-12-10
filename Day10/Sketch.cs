using Common.Classes;

namespace AdventOfCode2023.Day10;

internal class Sketch
{
    private readonly Tile[][] tiles = [];
    
    public Sketch(string[] rawDigram)
    {
        var diagram = rawDigram.Select(x => x.ToCharArray())
            .ToArray();
        tiles = diagram.Select((x, i) => x.Select((y, j) => new Tile(y, new Position<int>(j, i)))
            .ToArray()).ToArray();
    }

    public List<Pipe> GetLoop(char startCharacter)
    {
        var start = tiles.SelectMany(x => x)
            .FirstOrDefault(x => x.Value.Equals(startCharacter)) ??
            throw new Exception($"No start found (StartCharacter: {startCharacter})");

        var curPipe = new Pipe(start);
        var pipeCounter = 0;
        var loop = new List<Pipe>();
        while (curPipe.Position != start.Position || pipeCounter == 0)
        {
            if (curPipe.Equals('.'))
            {
                throw new InvalidOperationException("curPipe is not a pipe character");
            }
            
            loop.Add(curPipe);
            
            var fittingNeighborPipes = GetFittingNeighborPipes(curPipe, start.Value);
            if (fittingNeighborPipes.Count(x => x.Value != null) != 2)
                throw new Exception($"Pipe has more or less than two fitting neighbor pipes (Position: {curPipe.Position.X}, {curPipe.Position.Y})");

            curPipe.NextPipe = fittingNeighborPipes.Values.First(x => x != null && x.Position != curPipe.PrevPipe?.Position);
            curPipe.NextPipe!.PrevPipe = curPipe;

            curPipe = curPipe.NextPipe;
            pipeCounter++;
        }

        return loop;
    }

    public List<List<Tile>> GetGroundSections()
    {
        var sections = new List<List<Tile>>();

        for(var i=0; i< tiles.Length; i++)
        {
            for(var j = 0; j < tiles[i].Length; j++)
            {
                var curTile = tiles[i][j];

                // Ignore if tile is not a ground tile
                if (curTile.Value != '.')
                    continue;

                // Ignore if tile is already in a section
                if(sections.Any(x => x.Any(y => y.Position == curTile.Position)))
                    continue;

                // Process section
                var section = new List<Tile>() { curTile };

                void ProcessTile(Tile tile)
                {
                    var neighbors = GetConnectedNeighbors(tile.Position, new Dictionary<Direction, List<char>>()
                    {
                        { Direction.Left, new List<char> { '.' } },
                        { Direction.Right, new List<char> { '.' } },
                        { Direction.Top, new List<char> { '.' } },
                        { Direction.Bottom, new List<char> { '.' } }
                    });

                    var leftTile = neighbors[Direction.Left];
                    if (leftTile != null && !section.Any(t => t.Position == leftTile.Position))
                    {
                        section.Add(leftTile);
                        ProcessTile(leftTile);
                    }

                    var rightTile = neighbors[Direction.Right];
                    if (rightTile != null && !section.Any(t => t.Position == rightTile.Position))
                    {
                        section.Add(rightTile);
                        ProcessTile(rightTile);
                    }

                    var topTile = neighbors[Direction.Top];
                    if (topTile != null && !section.Any(t => t.Position == topTile.Position))
                    {
                        section.Add(topTile);
                        ProcessTile(topTile);
                    }

                    var bottomTile = neighbors[Direction.Bottom];
                    if (bottomTile != null && !section.Any(t => t.Position == bottomTile.Position))
                    {
                        section.Add(bottomTile);
                        ProcessTile(bottomTile);
                    }
                }

                ProcessTile(curTile);

                sections.Add(section);
            }
        }

        return sections;
    }

    private Dictionary<Direction, Pipe?> GetFittingNeighborPipes(Pipe pipe, char startingCharacter)
    {   
        var pipes = GetConnectedNeighbors(pipe.Position, new Dictionary<Direction, List<char>>()
        {
            { Direction.Left, new List<char> { '-', 'L', 'F', startingCharacter } },
            { Direction.Right, new List<char> { '-', 'J', '7', startingCharacter } },
            { Direction.Top, new List<char> { '|', '7', 'F', startingCharacter } },
            { Direction.Bottom, new List<char> { '|', 'L', 'J', startingCharacter } }
        });

        switch (pipe.Value)
        {
            case 'S':
                break;
            case '-':
                pipes[Direction.Top] = null;
                pipes[Direction.Bottom] = null;
                break;
            case '|':
                pipes[Direction.Left] = null;
                pipes[Direction.Right] = null;
                break;
            case 'L':
                pipes[Direction.Left] = null;
                pipes[Direction.Bottom] = null;
                break;
            case 'J':
                pipes[Direction.Bottom] = null;
                pipes[Direction.Right] = null;
                break;
            case '7':
                pipes[Direction.Top] = null;
                pipes[Direction.Right] = null;
                break;
            case 'F':
                pipes[Direction.Top] = null;
                pipes[Direction.Left] = null;
                break;
            default:
                pipes[Direction.Left] = null;
                pipes[Direction.Right] = null;
                pipes[Direction.Top] = null;
                pipes[Direction.Bottom] = null;
                break;
        }

        return pipes.ToDictionary(d => d.Key, t => t.Value == null ? null: new Pipe(t.Value));
    }

    private Dictionary<Direction, Tile?> GetConnectedNeighbors(Position<int> tilePosition, Dictionary<Direction, List<char>> possibleTileNeighbors)
    {
        var connectedNeighbors = new Dictionary<Direction, Tile?>();

        void AddIfValidNeighbor(Dictionary<Direction, Tile?> connectedNeighbors, Direction direction, Tile? tile) =>
            connectedNeighbors[direction] = tile != null && possibleTileNeighbors.TryGetValue(direction, out List<char>? neighbors) && neighbors.Contains(tile.Value) ? tile : null;

        int maxX = tiles[0].Length - 1;
        int maxY = tiles.Length - 1;

        Tile? leftTile = (tilePosition.X > 0) ? tiles[tilePosition.Y][tilePosition.X - 1] : null;
        Tile? rightTile = (tilePosition.X < maxX) ? tiles[tilePosition.Y][tilePosition.X + 1] : null;
        Tile? topTile = (tilePosition.Y > 0) ? tiles[tilePosition.Y - 1][tilePosition.X] : null;
        Tile? bottomTile = (tilePosition.Y < maxY) ? tiles[tilePosition.Y + 1][tilePosition.X] : null;

        AddIfValidNeighbor(connectedNeighbors, Direction.Left, leftTile);
        AddIfValidNeighbor(connectedNeighbors, Direction.Right, rightTile);
        AddIfValidNeighbor(connectedNeighbors, Direction.Top, topTile);
        AddIfValidNeighbor(connectedNeighbors, Direction.Bottom, bottomTile);

        return connectedNeighbors;
    }
}
