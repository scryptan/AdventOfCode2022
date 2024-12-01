using AdventOfCode2024.Helpers;

namespace AdventOfCode2024.Solutions;

public class Day3
{
    private readonly record struct Number(int Value, HashSet<V> Positions);

    private readonly record struct Schematic(List<Number> Numbers, Dictionary<char, HashSet<V>> Symbols);

    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n")
            .Select(x => x.Trim())
            .ToList();

        var schematic = BuildSchematic(input);


        Console.WriteLine(Part1(schematic));
        Console.WriteLine(Part2(schematic));
    }

    private string Part1(Schematic schematic)
    {
        var res = 0;
        var symbolPositions = schematic.Symbols.Values
            .SelectMany(set => set)
            .ToHashSet();

        foreach (var number in schematic.Numbers)
        {
            var adj = number.Positions
                .SelectMany(pos => pos.GetAdjacentSet())
                .ToHashSet();

            if (symbolPositions.Any(adj.Contains))
            {
                res += number.Value;
            }
        }


        return $"Part 1: {res}";
    }

    private string Part2(Schematic schematic)
    {
        var res = 0L;

        var gearPositions = schematic.Symbols['*'];

        foreach (var pos in gearPositions)
        {
            var adjPos = pos.GetAdjacentSet();
            var adjNum = schematic.Numbers
                .Where(num => num.Positions.Any(adjPos.Contains))
                .ToList();

            if (adjNum.Count == 2)
            {
                res += adjNum[0].Value * adjNum[1].Value;
            }
        }


        return $"Part 2: {res}";
    }

    private static Schematic BuildSchematic(IReadOnlyList<string> lines)
    {
        var symbols = new Dictionary<char, HashSet<V>>();
        var numbers = new List<Number>();

        for (var y = 0; y < lines.Count; y++)
        for (var x = 0; x < lines[0].Length; x++)
        {
            if (lines[y][x] == '.')
            {
                continue;
            }

            if (!char.IsDigit(lines[y][x]))
            {
                if (!symbols.TryGetValue(lines[y][x], out var poses))
                {
                    symbols.Add(lines[y][x], new HashSet<V>
                    {
                        new(x, y)
                    });
                }
                else
                {
                    poses.Add(new V(x, y));
                }

                continue;
            }

            var positions = new HashSet<V> { new(x, y) };
            var span = 1;

            while (x + span < lines[0].Length && char.IsDigit(lines[y][x + span]))
            {
                positions.Add(item: new V(x: x + span, y));
                span++;
            }

            var value = int.Parse(lines[y][x..(x + span)]);
            var number = new Number(value, positions);

            numbers.Add(number);
            x += span - 1;
        }

        return new Schematic(numbers, symbols);
    }
}