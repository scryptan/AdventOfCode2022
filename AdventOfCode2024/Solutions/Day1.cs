namespace AdventOfCode2024.Solutions;

public class Day1
{
    public void Solution()
    {
        var input = File.ReadLines("./Inputs/day1.txt").ToList();


        Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    private string Part1(List<string> lines)
    {
        var res = 0L;
        var left = new List<int>(lines.Count);
        var right = new List<int>(lines.Count);
        foreach (var line in lines)
        {
            var sp = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(sp[0]));
            right.Add(int.Parse(sp[1]));
        }

        left.Sort();
        right.Sort();
        var distances = new List<int>(left.Count);
        for (int i = 0; i < left.Count; i++)
        {
            distances.Add(Math.Abs(left[i] - right[i]));
        }

        res = distances.Sum();
        // foreach (var a in list)
        // {
        //     Console.WriteLine(a);
        // }

        return res.ToString();
    }

    private string Part2(List<string> lines)
    {
        var res = 0L;
        var left = new List<int>(lines.Count);
        var right = new List<int>(lines.Count);

        foreach (var line in lines)
        {
            var sp = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            left.Add(int.Parse(sp[0]));
            right.Add(int.Parse(sp[1]));
        }

        var rightDict = new Dictionary<int, int>();
        foreach (var i in right)
        {
            var added = rightDict.TryAdd(i, 1);
            if (!added) rightDict[i] += 1;
        }

        var similarity = new List<long>();
        
        foreach (var i in left)
        {
            if (rightDict.TryGetValue(i, out var val))
            {
                similarity.Add(i * val);
            }
        }

        res = similarity.Sum();

        return res.ToString();
    }
}