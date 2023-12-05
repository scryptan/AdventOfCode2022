namespace AdventOfCode2023;

public class DayN
{

    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n")
            .Select(x => x.Trim())
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