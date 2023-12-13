namespace Common.Classes;

public class Position<T>(T x, T y)
{
    public T X { get; set; } = x;

    public T Y { get; set; } = y;

    public T ManhattanDistance(Position<T> position) =>
        Math.Abs((dynamic) X - (dynamic) position.X) +
        Math.Abs((dynamic) Y - (dynamic) position.Y);
}
