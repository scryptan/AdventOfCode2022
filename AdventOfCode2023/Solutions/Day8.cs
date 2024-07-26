namespace AdventOfCode2023.Solutions;

public class Day8
{
    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n")
            .Select(x => x.Trim().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            .ToList();


        Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    private string Part1(List<string[]> input)
    {
        var res = "";
        return $"Part 1: {res}";
    }

    private string Part2(List<string[]> input)
    {
        var res = "";
        return $"Part 2: {res}";
    }
}