using Common.Classes;

namespace AdventOfCode2023.Day16;

internal class Contraption(char[][] map)
{
    public char[][] Map { get; set; } = map;

    public List<Position<int>> EnergizedTiles = [];

    public bool IsPositionInContraption(Position<int> pos)
    {
        return pos.Y >= 0 && 
            pos.Y < Map.Length && 
            pos.X >= 0 && 
            pos.X < Map[pos.Y].Length;
    }

    public void EnergizeTile(Position<int> pos)
    {
        if(EnergizedTiles.Any(p => p.X == pos.X && p.Y == pos.Y))
        {
            return;
        }

        EnergizedTiles.Add(Position<int>.Clone(pos));
    }
}
