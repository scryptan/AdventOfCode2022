namespace AdventOfCode2023.Solutions;

public class Day1
{
    public void Solution()
    {
        var input = File.ReadAllText("./Inputs/day3.txt")
            .Split("\n")
            .Select(x => x.Trim())
            .ToList();
        var res = 0L;

        Console.WriteLine(res);
    }
}