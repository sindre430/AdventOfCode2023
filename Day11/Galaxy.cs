using Common.Classes;

namespace Day11;

internal class Galaxy
{
    public Position<int> Position { get; set; }

    public Galaxy(Position<int> position)
    {
        Position = position;
    }
}
