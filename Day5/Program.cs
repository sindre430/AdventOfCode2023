using AdventOfCode2023.Common.Extensions;
using Common.Services.ConsoleTables;

namespace AdventOfCode2023.Day5;

public class Program
{
    public static void Main() { }

    public static void Part1(string[] almanacLines)
    {
        var almanac = new Almanac(almanacLines);

        // Map Seed values to Soil values
        var seedToSoilMapper = almanac.Mappers.First(m => m.SourceType == MapType.Seed && m.DestinationType == MapType.Soil);
        var soilValues = almanac.Seeds.Select(seedToSoilMapper.MapSourceToDestination);

        // Map Soil values to Fertilizer values
        var soilToFertilizerMapper = almanac.Mappers.First(m => m.SourceType == MapType.Soil && m.DestinationType == MapType.Fertilizer);
        var fertilizerValues = soilValues.Select(soilToFertilizerMapper.MapSourceToDestination);

        // Map Fertilizer values to Water values
        var fertilizerToWaterMapper = almanac.Mappers.First(m => m.SourceType == MapType.Fertilizer && m.DestinationType == MapType.Water);
        var waterValues = fertilizerValues.Select(fertilizerToWaterMapper.MapSourceToDestination);

        // Map Water values to Light values
        var waterToLightMapper = almanac.Mappers.First(m => m.SourceType == MapType.Water && m.DestinationType == MapType.Light);
        var lightValues = waterValues.Select(waterToLightMapper.MapSourceToDestination);

        // Map Light values to Temperature values
        var lightToTemperatureMapper = almanac.Mappers.First(m => m.SourceType == MapType.Light && m.DestinationType == MapType.Temperature);
        var temperatureValues = lightValues.Select(lightToTemperatureMapper.MapSourceToDestination);

        // Map Temperature values to Humidity values
        var temperatureToHumidityMapper = almanac.Mappers.First(m => m.SourceType == MapType.Temperature && m.DestinationType == MapType.Humidity);
        var humidityValues = temperatureValues.Select(temperatureToHumidityMapper.MapSourceToDestination);

        // Map Humidity values to Location values
        var humidityToLocationMapper = almanac.Mappers.First(m => m.SourceType == MapType.Humidity && m.DestinationType == MapType.Location);
        var locationValues = humidityValues.Select(humidityToLocationMapper.MapSourceToDestination);


        // Print out the results
        var table = new ConsoleTable("Seed", "Soil", "Fertilizer", "Water", "Light", "Temprature", "Humidity", "Location");
        for (var i=0; i<almanac.Seeds.Count; i++)
        {
            table.AddRow(
                almanac.Seeds.ElementAt(i),
                soilValues.ElementAt(i),
                fertilizerValues.ElementAt(i),
                waterValues.ElementAt(i),
                lightValues.ElementAt(i),
                temperatureValues.ElementAt(i),
                humidityValues.ElementAt(i),
                locationValues.ElementAt(i)
            );
        }
        table.Write(Format.Minimal);

        // Print lowest location number
        Console.WriteLine();
        Console.WriteLine($"Lowest Location number: {locationValues.Min()}");
    }

    public static void Part2(string[] cardLines)
    {
        var table = new ConsoleTable("one", "two", "three");
        table.AddRow(1, 2, 3)
             .AddRow("this line should be longer", "yes it is", "oh");

        table.Write(Format.Minimal);
        Console.WriteLine();

    }


    
}