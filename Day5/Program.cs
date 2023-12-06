using Common.Services.ConsoleTables;

namespace AdventOfCode2023.Day5;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] almanacLines)
    {
        var almanac = new Almanac(almanacLines);
        var result = ProcessAlmanac(almanac);

        // Print out the results
        var table = new ConsoleTable("Seed", "Soil", "Fertilizer", "Water", "Light", "Temprature", "Humidity", "Location");
        for (var i = 0; i < almanac.Seeds.Count; i++)
        {
            var objects = new List<long>
            {
                almanac.Seeds.ElementAt(i),
            };

            for (var j = 0; j < result.Count; j++)
            {
                objects.Add(result.ElementAt(j).Value.ElementAt(i));
            }

            table.AddRow([..objects]);
        }
        table.Write(Format.Minimal);

        // Print lowest location number
        Console.WriteLine();
        Console.WriteLine($"Lowest Location number: {result.Last().Value.Min()}");
    }

    public static void Part2(string[] almanacLines)
    {
        // Read seed info
        var seedLine = almanacLines.FirstOrDefault(l => l.StartsWith("seeds:"));
        var seedsParts = seedLine!.Split("seeds:")[1].Trim().Split(" ");

        // Process almanac part
        long curLowestLocationNumber = -1;
        void ProcessAlmanacPart(List<long> seedNumbers)
        {
            // Replace seed info in almanac
            var newAlmanacLines = almanacLines.Select(l => l.StartsWith("seeds:") ?
                $"seeds: {string.Join(" ", seedNumbers)}" : l).ToArray();

            // Process new almanac
            var almanac = new Almanac(newAlmanacLines);
            var lowestLocationNumber = ProcessAlmanacWithoutValueKeeping(almanac);

            // Replace value if lower
            if (curLowestLocationNumber == -1 || lowestLocationNumber < curLowestLocationNumber)
            {
                curLowestLocationNumber = lowestLocationNumber;
            }
        }

        // Process seed numbers and process new almanac parts
        var newSeedNumbers = new List<long>();
        for (var i = 0; i < seedsParts.Length; i+=2)
        {
            var startNumber = long.Parse(seedsParts[i]);
            var length = long.Parse(seedsParts[i + 1]);
            for (var j = 0; j < length; j++)
            {
                if (j > 0 && j % 1000 == 0)
                {
                    ProcessAlmanacPart(newSeedNumbers);
                    newSeedNumbers.Clear();
                }

                newSeedNumbers.Add(startNumber + j);
            }
            
            ProcessAlmanacPart(newSeedNumbers);
            newSeedNumbers.Clear();
        }

        // Print output
        Console.WriteLine($"Lowest Location number: {curLowestLocationNumber}");
    }

    private static Dictionary<Mapper, IEnumerable<long>> ProcessAlmanac(Almanac almanac)
    {
        var mapperValues = new Dictionary<Mapper, IEnumerable<long>>();

        // Map Seed values to Soil values
        var seedToSoilMapper = almanac.Mappers.First(m => m.SourceType == EntityType.Seed && m.DestinationType == EntityType.Soil);
        var soilValues = almanac.Seeds.Select(seedToSoilMapper.MapSourceToDestination);
        mapperValues.Add(seedToSoilMapper, soilValues);

        // Map Soil values to Fertilizer values
        var soilToFertilizerMapper = almanac.Mappers.First(m => m.SourceType == EntityType.Soil && m.DestinationType == EntityType.Fertilizer);
        var fertilizerValues = soilValues.Select(soilToFertilizerMapper.MapSourceToDestination);
        mapperValues.Add(soilToFertilizerMapper, fertilizerValues);

        // Map Fertilizer values to Water values
        var fertilizerToWaterMapper = almanac.Mappers.First(m => m.SourceType == EntityType.Fertilizer && m.DestinationType == EntityType.Water);
        var waterValues = fertilizerValues.Select(fertilizerToWaterMapper.MapSourceToDestination);
        mapperValues.Add(fertilizerToWaterMapper, waterValues);

        // Map Water values to Light values
        var waterToLightMapper = almanac.Mappers.First(m => m.SourceType == EntityType.Water && m.DestinationType == EntityType.Light);
        var lightValues = waterValues.Select(waterToLightMapper.MapSourceToDestination);
        mapperValues.Add(waterToLightMapper, lightValues);

        // Map Light values to Temperature values
        var lightToTemperatureMapper = almanac.Mappers.First(m => m.SourceType == EntityType.Light && m.DestinationType == EntityType.Temperature);
        var temperatureValues = lightValues.Select(lightToTemperatureMapper.MapSourceToDestination);
        mapperValues.Add(lightToTemperatureMapper, temperatureValues);

        // Map Temperature values to Humidity values
        var temperatureToHumidityMapper = almanac.Mappers.First(m => m.SourceType == EntityType.Temperature && m.DestinationType == EntityType.Humidity);
        var humidityValues = temperatureValues.Select(temperatureToHumidityMapper.MapSourceToDestination);
        mapperValues.Add(temperatureToHumidityMapper, humidityValues);

        // Map Humidity values to Location values
        var humidityToLocationMapper = almanac.Mappers.First(m => m.SourceType == EntityType.Humidity && m.DestinationType == EntityType.Location);
        var locationValues = humidityValues.Select(humidityToLocationMapper.MapSourceToDestination);
        mapperValues.Add(humidityToLocationMapper, locationValues);

        return mapperValues;
    }

    private static long ProcessAlmanacWithoutValueKeeping(Almanac almanac)
    {
        long lowestLocationNumber = -1;
        var mappers = new List<Mapper>
        {
            almanac.Mappers.First(m => m.SourceType == EntityType.Seed && m.DestinationType == EntityType.Soil),
            almanac.Mappers.First(m => m.SourceType == EntityType.Soil && m.DestinationType == EntityType.Fertilizer),
            almanac.Mappers.First(m => m.SourceType == EntityType.Fertilizer && m.DestinationType == EntityType.Water),
            almanac.Mappers.First(m => m.SourceType == EntityType.Water && m.DestinationType == EntityType.Light),
            almanac.Mappers.First(m => m.SourceType == EntityType.Light && m.DestinationType == EntityType.Temperature),
            almanac.Mappers.First(m => m.SourceType == EntityType.Temperature && m.DestinationType == EntityType.Humidity),
            almanac.Mappers.First(m => m.SourceType == EntityType.Humidity && m.DestinationType == EntityType.Location),
        };

        foreach (var seed in almanac.Seeds)
        {
            var curValue = seed;
            foreach(var mapper in mappers)
            {
                curValue = mapper.MapSourceToDestination(curValue);
            }

            if(lowestLocationNumber == -1 || curValue < lowestLocationNumber)
            {
                lowestLocationNumber = curValue;
            }
        }

        return lowestLocationNumber;
    }
}