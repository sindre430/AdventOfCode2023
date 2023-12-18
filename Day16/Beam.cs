using Common.Classes;
using Common.Enums;

namespace AdventOfCode2023.Day16;
internal class Beam(Contraption contraption, Position<int> position, Direction direction)
{
    public Contraption Contraption { get; set; } = contraption;
    
    public Position<int> Position { get; set; } = position;

    public Direction Direction { get; set; } = direction;

    public static Dictionary<Position<int>, Direction> History = [];

    public void Tick(out Beam? newBeam)
    {
        newBeam = null;
        switch (Direction)
        {
            case Direction.Up:
                Position = new Position<int>(Position.X, Position.Y - 1);
                break;
            case Direction.Right:
                Position = new Position<int>(Position.X + 1, Position.Y);
                break;
            case Direction.Down:
                Position = new Position<int>(Position.X, Position.Y + 1);
                break;
            case Direction.Left:
                Position = new Position<int>(Position.X - 1, Position.Y);
                break;
        }

        if(History.Any(v => 
            v.Key.X == Position.X && 
            v.Key.Y == Position.Y && 
            v.Value == Direction)){
            Position = new Position<int>(-1, -1);
        }

        History.Add(Position, Direction);
        
        var newTile = Contraption.Map.ElementAtOrDefault(Position.Y)?.ElementAtOrDefault(Position.X) ?? null;
        if(newTile == null)
        {
            return;
        }

        switch (newTile)
        {
            case '|':
                if(Direction == Direction.Left || Direction == Direction.Right) 
                {
                    Direction = Direction.Up;
                    newBeam = new Beam(Contraption, Position, Direction.Down);
                }
                return;
            case '-':
                if (Direction == Direction.Up || Direction == Direction.Down)
                {
                    Direction = Direction.Right;
                    newBeam = new Beam(Contraption, Position, Direction.Left);
                }
                return;
            case '/':
                if (Direction == Direction.Up)
                    Direction = Direction.Right;
                else if (Direction == Direction.Right)
                    Direction = Direction.Up;
                else if (Direction == Direction.Down)
                    Direction = Direction.Left;
                else if (Direction == Direction.Left)
                    Direction = Direction.Down;
                return;
            case '\\':
                if (Direction == Direction.Up)
                    Direction = Direction.Left;
                else if (Direction == Direction.Right)
                    Direction = Direction.Down;
                else if (Direction == Direction.Down)
                    Direction = Direction.Right;
                else if (Direction == Direction.Left)
                    Direction = Direction.Up;
                return;
            default:
                return;
        }
    }
}
