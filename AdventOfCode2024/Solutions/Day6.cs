namespace AdventOfCode2024.Solutions;

public class Day6
{
    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n")
            .Select(x => x.Trim())
            .ToList();

        var times = input
            .First()
            .Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        var distances = input
            .Last()
            .Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        Console.WriteLine(Part1(times, distances));
        Console.WriteLine(Part2(times, distances));
    }

    private string Part1(List<int> times, List<int> distances)
    {
        var res = 1L;

        for (int i = 0; i < times.Count; i++)
        {
            var winningCount = 0;
            var currTime = times[i];
            var currDist = distances[i];

            for (int j = 1; j < currTime; j++)
            {
                var speed = j;
                var takenTime = j;
                var remainingTime = currTime - takenTime;

                var distance = speed * remainingTime;
                if (currDist < distance)
                {
                    ++winningCount;
                }
            }

            if (winningCount > 0)
                res *= winningCount;
        }


        return $"Part 1: {res}";
    }

  
    private string Part2(List<int> times, List<int> distances)
    {
        var res = 1L;

        var timesStr = "";
        foreach (var timeI in times)
        {
            timesStr += timeI;
        }

        var time = long.Parse(timesStr);
        
        var distStr = "";
        foreach (var distI in distances)
        {
            distStr += distI;
        }

        var dist = long.Parse(distStr);
        
        
        var winningCount = 0;
        var currTime = time;
        var currDist = dist;

        for (int j = 1; j < currTime; j++)
        {
            var speed = j;
            var takenTime = j;
            var remainingTime = currTime - takenTime;

            var distance = speed * remainingTime;
            if (currDist < distance)
            {
                ++winningCount;
            }
        }

        if (winningCount > 0)
            res *= winningCount;


        return $"Part 2: {res}";
    }
}