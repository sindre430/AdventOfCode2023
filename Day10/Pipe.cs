using Common.Classes;
using System.Runtime.CompilerServices;

namespace AdventOfCode2023.Day10;

internal class Pipe(char Value, Position<int> Position) : Tile(Value, Position)
{
    public Pipe? NextPipe { get; set; }

    public Pipe? PrevPipe { get; set; }

    public Pipe(Tile tile) : this(tile.Value, tile.Position) { }
}
