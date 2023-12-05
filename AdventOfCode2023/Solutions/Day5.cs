namespace AdventOfCode2023.Solutions;

public class Day5
{
    private readonly Dictionary<long, long> _seedToSoil = new();
    private readonly Dictionary<long, long> _soilToFertilizer = new();
    private readonly Dictionary<long, long> _fertilizerToWater = new();
    private readonly Dictionary<long, long> _waterToLight = new();
    private readonly Dictionary<long, long> _lightToTemperature = new();
    private readonly Dictionary<long, long> _temperatureToHumidity = new();
    private readonly Dictionary<long, long> _humidityToLocation = new();

    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n")
            .Select(x => x.Trim())
            .ToList();

        // seeds: 79 14 55 13
        var seeds = input
            .Take(1)
            .Single()
            .Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        var currentDict = _seedToSoil;

        foreach (var line in input.Skip(1))
        {
            if (string.IsNullOrEmpty(line))
                continue;

            if (line.Contains("map"))
            {
                var mapName = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[0];
                currentDict = mapName switch
                {
                    "seed-to-soil" => _seedToSoil,
                    "soil-to-fertilizer" => _soilToFertilizer,
                    "fertilizer-to-water" => _fertilizerToWater,
                    "water-to-light" => _waterToLight,
                    "light-to-temperature" => _lightToTemperature,
                    "temperature-to-humidity" => _temperatureToHumidity,
                    "humidity-to-location" => _humidityToLocation,
                    _ => throw new Exception("Unknown map")
                };

                continue;
            }
            
            var coords = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var destRangeStart = long.Parse(coords[0]);
            var sourceRangeStart = long.Parse(coords[1]);
            var lengthRange = long.Parse(coords[2]);
            
            for (var i = 0; i < lengthRange; i++)
                currentDict[sourceRangeStart + i] = destRangeStart + i;
        }


        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private string Part1(List<long> seeds)
    {
        var res = "";
        
        
        
        return $"Part 1: {res}";
    }

    private string Part2()
    {
        var res = "";

        return $"Part 2: \n{res}";
    }
}