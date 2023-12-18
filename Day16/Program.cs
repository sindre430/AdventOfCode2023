using Common.Classes;
using Common.Enums;
using System.Data;
using System.Runtime.InteropServices;

namespace AdventOfCode2023.Day16;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] rawContraption)
    {
        var contraption = new Contraption(rawContraption.Select(s => s.ToCharArray()).ToArray());
        var beam = new Beam(contraption, new Position<int>(-1, 0), Direction.Right);

        ProcessContraption(contraption, beam);

        Console.WriteLine($"Num of energized tiles: {contraption.EnergizedTiles.Count}");
    }

    public static void Part2(string[] rawContraption)
    {
        var contraption = new Contraption(rawContraption.Select(s => s.ToCharArray()).ToArray());
        var mostEnergizedTiles = 0;

        // Process Vertical
        var numColumns = contraption.Map.First().Length;
        var numRows = contraption.Map.Length;
        for (var i = 0; i < numColumns; i++)
        {
            ProcessContraption(contraption, new Beam(contraption, new Position<int>(i, -1), Direction.Down));
            if (contraption.EnergizedTiles.Count > mostEnergizedTiles)
            {
                mostEnergizedTiles = contraption.EnergizedTiles.Count;
            }
            ProcessContraption(contraption, new Beam(contraption, new Position<int>(i, numRows), Direction.Up));
            if (contraption.EnergizedTiles.Count > mostEnergizedTiles)
            {
                mostEnergizedTiles = contraption.EnergizedTiles.Count;
            }
        }

        // Process Horizontal
        for (var i = 0; i < contraption.Map.Length; i++)
        {
            ProcessContraption(contraption, new Beam(contraption, new Position<int>(-1, i), Direction.Right));
            if (contraption.EnergizedTiles.Count > mostEnergizedTiles)
            {
                mostEnergizedTiles = contraption.EnergizedTiles.Count;
            }
            ProcessContraption(contraption, new Beam(contraption, new Position<int>(numColumns, i), Direction.Left));
            if (contraption.EnergizedTiles.Count > mostEnergizedTiles)
            {
                mostEnergizedTiles = contraption.EnergizedTiles.Count;
            }
        }

        Console.WriteLine($"Num energized tiles in best run: {mostEnergizedTiles}");
    }

    static void ProcessContraption(Contraption contraption, Beam startBeam)
    {
        contraption.EnergizedTiles.Clear();
        
        var beams = new List<Beam> { startBeam };
        var newBeams = new List<Beam>();
        var beamsToRemove = new List<Beam>();

        var beamsInMap = true;
        while (beamsInMap)
        {
            newBeams.Clear();
            beamsToRemove.Clear();

            foreach (var beam in beams)
            {
                beam.Tick(out var newBeam);
                if (newBeam != null)
                {
                    newBeams.Add(newBeam);
                }

                if (contraption.IsPositionInContraption(beam.Position))
                {
                    contraption.EnergizeTile(beam.Position);
                }
                else
                {
                    beamsToRemove.Add(beam);
                }
            }

            beams.RemoveAll(beamsToRemove.Contains);
            beams.AddRange(newBeams);

            beamsInMap = beams.Count != 0;
        }
    }
}