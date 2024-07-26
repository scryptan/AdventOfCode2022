using System.Numerics;

namespace AdventOfCode2023.Solutions;

public class Day8
{
    private const StringSplitOptions SplitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n", SplitOptions)
            .ToList();

        var rule = input.First();
        var dict = new Dictionary<string, (string Left, string Right)>();

        foreach (var line in input.Skip(1))
        {
            var key = line.Split("=", SplitOptions).First();
            var values = line.Split("=", SplitOptions).Last().Split(",", SplitOptions);

            dict.Add(key, (values.First().Replace("(", string.Empty), values.Last().Replace(")", string.Empty)));
        }

        Console.WriteLine(Part1(rule, dict));
        Console.WriteLine(Part2(rule, dict));
    }

    private string Part1(string rule, Dictionary<string, (string Left, string Right)> map)
    {
        var res = "";

        var currentStep = "AAA";
        var steps = 0;
        while (currentStep != "ZZZ")
        {
            var currentRule = rule[steps % rule.Length];

            currentStep = currentRule == 'L' ? map[currentStep].Left : map[currentStep].Right;
            ++steps;
        }

        res = steps.ToString();

        return $"Part 1: {res}";
    }

    private string Part2(string rule, Dictionary<string, (string Left, string Right)> map)
    {
        var res = "";

        var steps = map.Keys.Where(x => x.Last() == 'A').ToList();
        var stepsCount = new List<long>();

        foreach (var step in steps)
        {
            var tempStep = step;
            var stepCount = 0l;

            while (tempStep[2] != 'Z')
            {
                var currentRule = rule[(int)(stepCount % rule.Length)];
                tempStep = currentRule == 'L' ? map[tempStep].Left : map[tempStep].Right;
                ++stepCount;
            }

            stepsCount.Add(stepCount);
        }

        res = MathHelpers.LeastCommonMultiple(stepsCount).ToString();

        return $"Part 2: {res}";

        bool ShouldContinue()
        {
            foreach (var step in steps)
            {
                if (step[^1] != 'Z') return true;
            }

            return false;
        }
    }
}

public static class MathHelpers
{
    public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
        => a / GreatestCommonDivisor(a, b) * b;

    public static T LeastCommonMultiple<T>(this IEnumerable<T> values) where T : INumber<T>
        => values.Aggregate(LeastCommonMultiple);
}