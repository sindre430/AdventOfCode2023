using Common.Classes;

namespace AdventOfCode2023.Day10;

internal class Sketch
{
    public readonly Tile[][] tiles = [];
    
    public Sketch(string[] rawDigram)
    {
        var diagram = rawDigram.Select(x => x.ToCharArray())
            .ToArray();
        tiles = diagram.Select((x, i) => x.Select((y, j) => new Tile(y, new Position<int>(j, i)))
            .ToArray()).ToArray();
    }

    public Loop GetLoop(char startCharacter)
    {
        var start = tiles.SelectMany(x => x)
            .FirstOrDefault(x => x.Value.Equals(startCharacter)) ??
            throw new Exception($"No start found (StartCharacter: {startCharacter})");

        var startPipe = new Pipe(start);
        var curPipe = startPipe;
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

        loop.First().PrevPipe = loop.Last();

        return new Loop(startPipe);
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
