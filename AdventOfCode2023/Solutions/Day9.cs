namespace AdventOfCode2023.Solutions;

public class Day9
{
    private const StringSplitOptions SplitOptions =
        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n", SplitOptions)
            .Select(x => x.Split(" ", SplitOptions).Select(int.Parse).ToArray())
            .ToList();


        Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    private string Part1(List<int[]> values)
    {
        var res = 0L;

        foreach (var value in values)
        {
            var extrapolationList = CreateExtrapolationList(value);
            res += CalculateExtrapolatedValue(extrapolationList);
        }

        return $"Part 1: {res}";
    }

    private string Part2(List<int[]> values)
    {
        var res = 0L;

        foreach (var value in values)
        {
            var extrapolationList = CreateExtrapolationList(value);
            res += CalculateExtrapolatedValuePart2(extrapolationList);
        }

        return $"Part 2: {res}";
    }

    private List<List<int>> CreateExtrapolationList(int[] values)
    {
        var res = new List<List<int>>(new[] { new List<int>(values) });

        var currLine = res.Last();
        while (currLine.Any(x => x != 0))
        {
            var newLine = new List<int>();
            for (int i = 0; i < currLine.Count - 1; i++)
            {
                newLine.Add(currLine[i + 1] - currLine[i]);
            }

            res.Add(newLine);
            currLine = newLine;
        }

        return res;
    }

    private long CalculateExtrapolatedValue(List<List<int>> values)
    {
        var res = 0L;

        for (int i = 0; i < values.Count - 1; i++)
        {
            res += values[i][^1];
        }

        return res;
    }

    private long CalculateExtrapolatedValuePart2(List<List<int>> values)
    {
        var res = 0L;

        for (int i = values.Count - 1; i >= 0; i--)
        {
            res = values[i][0] - res;
        }

        return res;
    }
}