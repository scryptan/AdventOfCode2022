namespace AdventOfCode2024.Solutions;

public class Day5
{
    private readonly List<Coord> _seedToSoil = new();
    private readonly List<Coord> _soilToFertilizer = new();
    private readonly List<Coord> _fertilizerToWater = new();
    private readonly List<Coord> _waterToLight = new();
    private readonly List<Coord> _lightToTemperature = new();
    private readonly List<Coord> _temperatureToHumidity = new();
    private readonly List<Coord> _humidityToLocation = new();

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

        var currList = _seedToSoil;
        int lineIndex = 1;
        foreach (var line in input.Skip(1))
        {
            if (string.IsNullOrEmpty(line))
                continue;

            if (line.Contains("map"))
            {
                var mapName =
                    line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[0];
                currList = mapName switch
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

            currList.Add(new Coord
            {
                SourceRangeStart = sourceRangeStart,
                DestinationRangeStart = destRangeStart,
                RangeLength = lengthRange
            });

            ++lineIndex;
            Console.WriteLine(lineIndex);
        }


        Console.WriteLine(Part1(seeds));
        Console.WriteLine(Part2(seeds));
    }

    private string Part1(List<long> seeds)
    {
        var res = long.MaxValue;

        foreach (var seed in seeds)
        {
            var location = GetLocationFromSeed(seed);
            res = Math.Min(res, location);
        }


        return $"Part 1: {res}";
    }

    private string Part2(List<long> seeds)
    {
        var mappings = new List<List<Coord>>
        {
            _seedToSoil,
            _soilToFertilizer,
            _fertilizerToWater,
            _waterToLight,
            _lightToTemperature,
            _temperatureToHumidity,
            _humidityToLocation
        };

        var curr = new List<(long start, long length)>();
        for (int i = 0; i < seeds.Count; i += 2)
        {
            var startSeed = seeds[i];
            var lengthSeed = seeds[i + 1];
            curr.Add((startSeed, lengthSeed));
        }

        foreach (var mapping in mappings)
        {
            var next = new List<(long start, long length)>();
            foreach (var c in curr)
            {
                var used = new List<(long start, long length)>();

                foreach (var m in mapping)
                {
                    var (start, end) = (Math.Max(c.start, m.SourceRangeStart),
                        Math.Min(c.start + c.length, m.SourceRangeStart + m.RangeLength));
                    
                    if(end < start) continue;
                    used.Add((start, end - start));
                    next.Add((m.DestinationRangeStart + (start - m.SourceRangeStart), end - start));
                }
                
                used.Sort((a, b) => a.start.CompareTo(b.start));
                var s = c.start;
                foreach (var u in used)
                {
                    if (s < u.start)
                        next.Add((s, u.start - s));
                    
                    s = u.start + u.length;
                }
                if (s < c.start + c.length)
                    next.Add((s, c.start + c.length - s));
            }

            curr = next.ToList();
        }

        var res = curr.Min(x => x.start);

        return $"Part 2: {res}";
    }

    private long GetLocationFromSeed(long seed)
    {
        var soil = GetValue(seed, _seedToSoil);
        var fertilizer = GetValue(soil, _soilToFertilizer);
        var water = GetValue(fertilizer, _fertilizerToWater);
        var light = GetValue(water, _waterToLight);
        var temperature = GetValue(light, _lightToTemperature);
        var humidity = GetValue(temperature, _temperatureToHumidity);

        return GetValue(humidity, _humidityToLocation);
    }

    private long GetValue(long seed, List<Coord> dictionary)
    {
        var values = dictionary
            .Select(x => x.GetValue(seed))
            .Where(x => x != -1)
            .ToArray();

        if (values.Length == 0) return seed;

        return values[0];
    }

    struct Coord
    {
        public long SourceRangeStart { get; set; }
        public long DestinationRangeStart { get; set; }
        public long RangeLength { get; set; }

        public long GetValue(long seed)
        {
            if (seed < SourceRangeStart || seed >= SourceRangeStart + RangeLength)
                return -1;

            return DestinationRangeStart + (seed - SourceRangeStart);
        }
    }
}