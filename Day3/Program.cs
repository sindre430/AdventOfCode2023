using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day3;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] schematicLines)
    {
        var numbers = GetAllNumbers(schematicLines);
        
        for(var i=0; i<schematicLines.Length; i++)
        {
            var line = schematicLines[i];
            var partNumbersOnLine = numbers.Where(n => n.LineNr == i && n.Symbols.Count > 0)
                .Select(n => n.Value);
            Console.WriteLine($"{line} ({string.Join(", ", partNumbersOnLine)})");
        }

        var sum = numbers.Where(n => n.Symbols.Count > 0)
            .Select(n => n.Value)
            .Sum();
        Console.WriteLine();
        Console.WriteLine($"Sum of part numbers: {sum}");
    }

    public static void Part2(string[] schematicLines)
    {
        var numbers = GetAllNumbers(schematicLines);
        var gearSymbols = numbers.SelectMany(n => n.Symbols)
            .GroupBy(x => x.Id)
            .Where(e => e.Count() == 2)
            .Select(e => e.First())
            .ToList();

        var gearSymbolIds = gearSymbols.Select(s => s.Id)
            .ToList();

        var allGearNumbers = numbers.Where(n => n.Symbols.Any(s => gearSymbolIds.Contains(s.Id)))
            .ToList();

        for (var i = 0; i < schematicLines.Length; i++)
        {
            var line = schematicLines[i];
            var partNumbersOnLine = allGearNumbers.Where(n => n.LineNr == i)
                .Select(n => n.Value);
            Console.WriteLine($"{line} ({string.Join(", ", partNumbersOnLine)})");
        }
        Console.WriteLine();

        var gearRatios = new List<int>();
        foreach (var gearSymbol in gearSymbols)
        {
            var gearParts = numbers.Where(n => n.Symbols.Any(s => s.Id == gearSymbol.Id))
                .ToList();

            var gearRatio = gearParts[1].Value * gearParts[0].Value;

            Console.WriteLine($"Gear on Line {gearSymbol.LineNr}, Position {gearSymbol.StartIndex}. Gear Ratio: {gearRatio} ({gearParts[0].Value} * {gearParts[1].Value})");
            gearRatios.Add(gearRatio);
        }
        Console.WriteLine();

        var sum = gearRatios.Sum();
        Console.WriteLine($"Sum of all gear ratios: {sum}");
    }

    private static List<Number> GetAllNumbers(string[] schematicLines)
    {
        var partNumbers = new List<Number>();

        for (var lineNr = 0; lineNr < schematicLines.Length; lineNr++)
        {
            var line = schematicLines[lineNr];
            var allNumbers = line.GetAllNumbers()
                .Select(Math.Abs);

            var partNumbersOnLine = new List<Number>();

            var prevEndIndex = 0;
            foreach (var number in allNumbers)
            {
                var numberObj = new Number()
                {
                    Value = number,
                    LineNr = lineNr
                };

                // Find start and end index of number
                var startIndex = line.IndexOf(number.ToString(), prevEndIndex);
                var endIndex = startIndex + number.ToString().Length - 1;
                while (
                    (startIndex > 0 && char.IsDigit(line[startIndex - 1])) ||
                    (endIndex < line.Length - 1 && char.IsDigit(line[endIndex + 1])))
                {
                    startIndex = line.IndexOf(number.ToString(), endIndex + 1);
                    endIndex = startIndex + number.ToString().Length - 1;
                }
                numberObj.StartIndex = startIndex;
                prevEndIndex = endIndex;

                // Add adjacent chars on same line
                char? leftChar = startIndex > 0 ? line[startIndex - 1] : null;
                char? rightChar = endIndex < line.Length - 1 ? line[endIndex + 1] : null;

                if (leftChar != null && leftChar != '.')
                {
                    numberObj.Symbols.Add(new Symbol()
                    {
                        Value = leftChar.Value,
                        LineNr = lineNr,
                        StartIndex = startIndex - 1
                    });
                }

                if (rightChar != null && rightChar != '.')
                {
                    numberObj.Symbols.Add(new Symbol()
                    {
                        Value = rightChar.Value,
                        LineNr = lineNr,
                        StartIndex = endIndex + 1
                    });
                }

                // Add adjacent chars on previous line
                if (lineNr > 0)
                {
                    var previousLine = schematicLines[lineNr - 1];
                    var adjacentCharsOnPreviousLine = getAdjacentCharsOnNeighbourLine(previousLine);
                    for (var i = 0; i < adjacentCharsOnPreviousLine.Length; i++)
                    {
                        var c = adjacentCharsOnPreviousLine[i];
                        if (c != '.')
                        {
                            numberObj.Symbols.Add(new Symbol()
                            {
                                Value = c,
                                LineNr = lineNr - 1,
                                StartIndex = startIndex - (startIndex > 0 ? 1 : 0) + i
                            });
                        }
                    }
                }

                // Add adjacent chars on next line
                if (lineNr < schematicLines.Length - 1)
                {
                    var nextLine = schematicLines[lineNr + 1];
                    var adjacentCharsOnNextLine = getAdjacentCharsOnNeighbourLine(nextLine);
                    for (var i = 0; i < adjacentCharsOnNextLine.Length; i++)
                    {
                        var c = adjacentCharsOnNextLine[i];
                        if (c != '.')
                        {
                            numberObj.Symbols.Add(new Symbol()
                            {
                                Value = c,
                                LineNr = lineNr + 1,
                                StartIndex = startIndex - (startIndex > 0 ? 1 : 0) + i
                            });
                        }
                    }
                }

                partNumbersOnLine.Add(numberObj);

                char[] getAdjacentCharsOnNeighbourLine(string line)
                {
                    var neighbourLineStartIndex = startIndex > 0 ? startIndex - 1 : startIndex;
                    var neighbourLineEndIndex = endIndex < line.Length - 1 ? endIndex + 1 : endIndex;
                    return line.Substring(neighbourLineStartIndex, neighbourLineEndIndex - neighbourLineStartIndex + 1)
                        .ToCharArray();
                }
            }

            partNumbers.AddRange(partNumbersOnLine);
        }

        return partNumbers;
    }
}