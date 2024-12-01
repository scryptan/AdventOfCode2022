namespace AdventOfCode2024.Solutions;

public class Day2
{
    private int _redMax = 12; //only 12 red cubes, 13 green cubes, and 14 blue cubes
    int _greenMax = 13;
    int _blueMax = 14;

    public void Solution()
    {
        var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
            .Split("\n")
            .Select(x => x.Trim())
            .ToList();


        Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    private string Part1(List<string> lines)
    {
        var res = 0L;

        var impossible = new HashSet<int>();
        var gameIds = new List<int>();
        for (int i = 0; i < lines.Count; i++)
        {
            var gameId = i + 1;
            gameIds.Add(gameId);

            var line = lines[i].Replace($"Game {gameId}: ", String.Empty);
            var sets = line.Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (var set in sets)
            {
                var redCount = 0;
                var greenCount = 0;
                var blueCount = 0;

                var squares = set.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var square in squares)
                {
                    var splitted = square.Split(" ",
                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    var count = Convert.ToInt32(splitted[0]);
                    switch (splitted[1])
                    {
                        case "red":
                            redCount += count;
                            break;
                        case "green":
                            greenCount += count;
                            break;
                        case "blue":
                            blueCount += count;
                            break;
                    }
                }

                if (redCount > _redMax || greenCount > _greenMax || blueCount > _blueMax)
                {
                    impossible.Add(gameId);
                    break;
                }
            }
        }

        res = gameIds.Except(impossible).Sum();

        return $"Part 1: {res}";
    }

    private string Part2(List<string> lines)
    {
        var res = 0L;

        var powers = new List<int>();
        for (int i = 0; i < lines.Count; i++)
        {
            var gameId = i + 1;

            var line = lines[i].Replace($"Game {gameId}: ", String.Empty);
            var sets = line.Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            
            var localMaxRed = 0;
            var localMaxGreen = 0;
            var localMaxBlue = 0;
            
            foreach (var set in sets)
            {
                var redCount = 0;
                var greenCount = 0;
                var blueCount = 0;

                var squares = set.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var square in squares)
                {
                    var splitted = square.Split(" ",
                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    var count = Convert.ToInt32(splitted[0]);
                    switch (splitted[1])
                    {
                        case "red":
                            redCount += count;
                            break;
                        case "green":
                            greenCount += count;
                            break;
                        case "blue":
                            blueCount += count;
                            break;
                    }
                }

                if (localMaxRed < redCount)
                    localMaxRed = redCount;
                
                if (localMaxGreen < greenCount)
                    localMaxGreen = greenCount;
                
                if (localMaxBlue < blueCount)
                    localMaxBlue = blueCount;
            }
            
            powers.Add(localMaxRed * localMaxGreen * localMaxBlue);
            
        }

        res = powers.Sum();


        return $"Part 2: \n{res}";
    }
}