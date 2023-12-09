using System.Numerics;

namespace AdventOfCode2023.Day8;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] mapLines)
    {
        var map = new Map(mapLines);

        var startNodeName = "AAA";
        var endNodeName = "ZZZ";
        var currentNode = map.nodes.GetValueOrDefault(startNodeName) ??
            throw new Exception($"Node {startNodeName} not found");
        var numSteps = 0;
        while(!currentNode!.Name.Equals(endNodeName))
        {
            var direction = map.GetDirection(numSteps);
            Console.WriteLine($"{currentNode.Name} (Next: {direction})");
            currentNode = direction switch
            {
                Direction.L => currentNode.LeftNode,
                Direction.R => currentNode.RightNode,
                _ => throw new Exception("Invalid direction")
            };

            numSteps++;
        }

        Console.WriteLine();
        Console.WriteLine($"Reached {endNodeName} on {numSteps} steps");
    }

    public static void Part2(string[] mapLines)
    {
        var map = new Map(mapLines);
        var startNodes = map.nodes.Where(n => n.Key.EndsWith('A'))
            .Select(n => n.Value)
            .ToList();

        var stepsInLoops = new List<int>();
        for (var i=0; i<startNodes.Count; i++)
        {
            FindLoop(map, startNodes[i], out var stepsInLoop);
            stepsInLoops.Add(stepsInLoop);
        }

        BigInteger lcm = 1;
        foreach (int num in stepsInLoops)
        {
            lcm = Common.Services.Math.LCM(lcm, num);
        }

        Console.WriteLine();
        Console.WriteLine($"LCM: {lcm}");
    }

    private static void FindLoop(Map map, MapNode startNode, out int stepsInLoop)
    {
        var curStep = 0;
        var history = new List<(int, MapNode)>()
        {
            ( 0, startNode )
        };
        var curNode = startNode;

        while (true)
        {
            for (var i = 0; i < map.directions.Count; i++)
            {
                var direction = map.GetDirection(i);
                curNode = direction switch
                {
                    Direction.L => curNode.LeftNode!,
                    Direction.R => curNode.RightNode!,
                    _ => throw new Exception("Invalid direction")
                };

                curStep++;
                var directionIndex = curStep % map.directions.Count;

                var loopStartIndex = history.FindIndex(h => h.Item1 == directionIndex && h.Item2 == curNode);
                if (loopStartIndex > 0)
                {
                    stepsInLoop = history.Count - loopStartIndex;

                    Console.WriteLine($"Found loop start at {curStep} steps. Loop has a length of {stepsInLoop}, starting at step {loopStartIndex}");
                    var loop = history.Skip(loopStartIndex).ToList();
                    foreach (var (step, node) in loop)
                    {
                        if (node.Name.EndsWith('Z'))
                        {
                            Console.WriteLine($"  Step {step}: {node.Name}");
                        }
                    }

                    return;
                }

                history.Add((directionIndex, curNode));
            }
        }
    }
}