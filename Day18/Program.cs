using Common.Classes;
using Common.Enums;
using Common.Services;
using System.Data;
using System.Xml.XPath;

namespace AdventOfCode2023.Day18;

public static class Program
{
    public static void Main() { }

    public static void Part1(string[] rawDigPlan)
    {
        var moves = rawDigPlan.Select(Move.Parse).ToArray();
        Run(moves);
    }

    public static void Part2(string[] rawDigPlan)
    {
        var moves = rawDigPlan.Select(Move.Parse).ToArray();
        foreach (var move in moves)
        {
            var rawDir = move.TrenchColor.Last();
            move.MoveDirection = rawDir switch
            {
                '0' => Direction.Right,
                '1' => Direction.Down,
                '2' => Direction.Left,
                '3' => Direction.Up,
                _ => throw new ArgumentException("Invalid move direction")
            };

            var rawDist = move.TrenchColor[1..^1];
            move.MoveDistance = Convert.ToInt32(rawDist, 16);
        }

        Run(moves);
    }

    private static void Run(Move[] moves)
    {
        var map = new List<List<char>> { new() { '#' } };
        var curPosition = new Position<int>(0, 0);
        foreach (var move in moves)
        {
            map.Dig(curPosition, move, out curPosition);
        }

        foreach (var row in map)
        {
            Console.WriteLine(new string(row.ToArray()));
        }

        var numGroundTiles = map.GetNumberOfGroundTiles();
        var numTiles = map.SelectMany(c => c).Count();

        Console.WriteLine();
        Console.WriteLine($"Number of ground tiles: {numGroundTiles}");
        Console.WriteLine($"Number of tiles: {numTiles}");
        Console.WriteLine($"Number of lagoon tiles: {numTiles - numGroundTiles}");
    }

    private static void DigOutInterior(this List<List<char>> map)
    {
        map.ForEach(row =>
        {
            var fill = false;
            var continuesTrench = false;
            for (var i = 0; i < row.Count; i++)
            {
                if (row[i] == '#')
                {
                    if (continuesTrench)
                    {
                        continue;
                    }
                    fill = !fill;
                    if(fill)
                    {
                        continuesTrench = true;
                    }
                }
                else if (fill)
                {
                    if (continuesTrench)
                    {
                        if (!row[i..].Any(c => c.Equals('#')))
                        {
                            break;
                        }
                        continuesTrench = false;

                    }
                    
                    row[i] = '#';
                }
            }
        });
    }

    private static void Dig(this List<List<char>> map, Position<int> curPosition, Move move, out Position<int> newPosition)
    {  
        newPosition = move.MoveDirection switch
        {
            Direction.Up => new Position<int>(curPosition.X, curPosition.Y - 1),
            Direction.Down => new Position<int>(curPosition.X, curPosition.Y + 1),
            Direction.Left => new Position<int>(curPosition.X - 1, curPosition.Y),
            Direction.Right => new Position<int>(curPosition.X + 1, curPosition.Y),
            _ => throw new ArgumentException("Invalid move direction")
        };

        if (newPosition.X >= map.First().Count)
        {
            map.ExpandMapWidth();
        }
        else if (newPosition.X < 0)
        {
            map.ExpandMapWidth(true);
            newPosition.X = 0;
        }
        else if (newPosition.Y >= map.Count)
        {
            map.ExpandMapHeight();
        }
        else if (newPosition.Y < 0)
        {
            map.ExpandMapHeight(true);
            newPosition.Y = 0;
        }

        try
        {
            map[newPosition.Y][newPosition.X] = '#';
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        move.MoveDistance--;

        if(move.MoveDistance > 0)
        {
            map.Dig(newPosition, move, out newPosition);
        }
    }

    private static void ExpandMapWidth(this List<List<char>> map, bool prepend = false)
    {
        if (prepend)
            map.ForEach(r => r.Insert(0, '.'));
        else
            map.ForEach(r => r.Add('.'));
    }

    private static void ExpandMapHeight(this List<List<char>> map, bool prepend = false)
    {
        var res = new char[map.First().Count];
        Array.Fill(res, '.');

        if (prepend)
            map.Insert(0, [.. res]);
        else
            map.Add([.. res]);
    }

    private static int GetNumberOfGroundTiles(this List<List<char>> map, Dictionary<int, List<int>>? visitedPositions = null, List<(int, int)>? positionsToProcess = null)
    {
        // Get all positions of '.' in border of map
        if(positionsToProcess is null)
        {
            var minY = 0;
            var maxY = map.Count - 1;
            var minX = 0;
            var maxX = map.First().Count - 1;
            positionsToProcess = [];
            for (var y = minY; y <= maxY; y++)
            {
                if(y == minY || y == maxY)
                {
                    for (var x = minX; x <= maxX; x++)
                    {
                        if (map[y][x] == '.')
                        {
                            positionsToProcess.Add((x, y));
                        }
                    }
                    continue;
                }

                if (map[y].First() == '.')
                {
                    positionsToProcess.Add((minX, y));
                }

                if (map[y].Last() == '.')
                {
                    positionsToProcess.Add((maxX, y));
                }
            }

            return GetNumberOfGroundTiles(map, [], positionsToProcess);
        }

        visitedPositions ??= [];
        positionsToProcess ??= [];

        var possiblePositions = new List<(int, int)>();
        foreach (var position in positionsToProcess)
        {
            if(!visitedPositions.TryGetValue(position.Item1, out var yPositions))
            {
                yPositions = [];
                visitedPositions[position.Item1] = yPositions;
            }
            yPositions.Add(position.Item2);

            possiblePositions.AddRange(new List<(int, int)>
            {
                (position.Item1 - 1, position.Item2),
                (position.Item1 + 1, position.Item2),
                (position.Item1, position.Item2 - 1),
                (position.Item1, position.Item2 + 1)
            });
        }

        possiblePositions = possiblePositions.Where(p => 
            !visitedPositions.Any(vp => vp.Key == p.Item1 && vp.Value.Contains(p.Item2)) && 
            p.Item1 >= 0 && 
            p.Item2 >= 0 && 
            p.Item1 < map.First().Count && 
            p.Item2 < map.Count &&
            map[p.Item2][p.Item1] == '.').Distinct().ToList();
        
        if(possiblePositions.Count == 0)
        {
            return visitedPositions.SelectMany(vp => vp.Value).Count();
        }

        return GetNumberOfGroundTiles(map, visitedPositions, possiblePositions);
    }
}



