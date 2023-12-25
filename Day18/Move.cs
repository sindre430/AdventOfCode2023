using Common.Enums;
namespace AdventOfCode2023.Day18;

public class Move(Direction direction, int distance, string trenchColor)
{
    public Direction MoveDirection { get; set; } = direction;

    public int MoveDistance { get; set; } = distance;

    public string TrenchColor { get; set; } = trenchColor;

    public static Move Parse(string moveString)
    {
        var moveParts = moveString.Split(' ');
        var direction = moveParts[0] switch
        {
            "U" => Direction.Up,
            "D" => Direction.Down,
            "R" => Direction.Right,
            "L" => Direction.Left,
            _ => throw new ArgumentException("Invalid move direction")
        };
        var trenchColor = moveParts[2].Replace("(", "")
            .Replace(")", "");

        return new Move(direction, int.Parse(moveParts[1]), trenchColor);
    }
}
