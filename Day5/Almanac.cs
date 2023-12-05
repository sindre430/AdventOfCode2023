using AdventOfCode2023.Common.Extensions;

namespace AdventOfCode2023.Day5;

internal class Almanac
{
    public List<long> Seeds { get; set; }

    public List<Mapper> Mappers { get; set; } = new List<Mapper>();

    public Almanac(string[] rawLines)
    {
        for(var i=0; i<rawLines.Length; i++)
        {
            var curLine = rawLines[i];

            // If Line contains seed information
            if (curLine.StartsWith("seeds:"))
            {
                Seeds = curLine.GetAllNumbersLong();
            }

            // If next Lines conatins mapping information
            if (curLine.Contains("map:"))
            {
                var mapLines = new List<string>();
                while (!string.IsNullOrEmpty(curLine))
                {
                    mapLines.Add(curLine);
                    if (rawLines.Length == ++i)
                    {
                        break;
                    }
                    curLine = rawLines[i];
                }

                Mappers.Add(new Mapper([.. mapLines]));
            }
        }
    }
}
