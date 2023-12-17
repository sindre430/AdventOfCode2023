using System.Collections.Specialized;

namespace AdventOfCode2023.Day15;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] rawLines)
    {
        var steps = rawLines[0].Split(',');
        var sum = 0;
        foreach (var step in steps)
        {
            var result = RunHashAlgorithm(step);
            Console.WriteLine($"Step: {step}, Result: {result}");
            sum += result;
        }

        Console.WriteLine();
        Console.WriteLine($"Sum: {sum}");
    }

    public static void Part2(string[] rawLines)
    {
        var boxes = new Dictionary<int, OrderedDictionary>();
        
        var steps = rawLines[0].Split(',');
        foreach (var step in steps)
        {
            string label;
            int? value = null;
            if (step.Contains('='))
            {
                var parts = step.Split('=');
                label = parts[0];
                value = int.Parse(parts[1]);
            }
            else
            {
                label = step[..^1];
            }
            
            var boxNr = RunHashAlgorithm(label);
            boxes.TryGetValue(boxNr, out var box);
            if (value.HasValue)
            {
                if(box == null)
                {
                    box = [];
                    boxes[boxNr] = box;
                }

                if (box[label] != null)
                    box[label] = value.Value;
                else
                    box.Insert(box.Count, label, value.Value);
            }
            else
            {
                box?.Remove(label);
            }
        }

        var sum = 0;
        foreach (var box in boxes)
        {
            for(var i = 0; i < box.Value.Count; i++)
            {
                var value = box.Value[i];
                sum += (box.Key+1) * (i+1) * (int)value;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Sum: {sum}");
    }

    private static int RunHashAlgorithm(string s)
    {
        var chars = s.ToCharArray();
        var curValue = 0;
        foreach (var c in chars)
        {
            curValue += Convert.ToInt32(c);
            curValue *= 17;
            curValue %= 256;
        }

        return curValue;
    }
}