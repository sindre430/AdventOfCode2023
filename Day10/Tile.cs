using Common.Classes;

namespace AdventOfCode2023.Day10;

internal class Tile(char value, Position<int> position)
{
    public char Value { get; set; } = value;

    public Position<int> Position { get; set; } = position;
}
