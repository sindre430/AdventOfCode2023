namespace Common.Classes;

public class Position<T>(T x, T y)
{
    public T X { get; set; } = x;

    public T Y { get; set; } = y;
}
