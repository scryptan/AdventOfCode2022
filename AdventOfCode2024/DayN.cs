namespace AdventOfCode2024;

public class DayN
{
    private const StringSplitOptions SplitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n", SplitOptions)
            .ToList();


        Console.WriteLine(Part1());
        Console.WriteLine(Part2());
    }

    private string Part1()
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