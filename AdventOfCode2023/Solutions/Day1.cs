namespace AdventOfCode2023.Solutions;

public class Day1
{
    public void Solution()
    {
        var textDigits = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        var input = File.ReadLines("./Inputs/day1.txt");
        var res = 0L;
        var list = new List<long>();

        foreach (var line in input)
        {
            var firstDigit = -1;
            var secondDigit = -1;
            for (int i = 0; i < line.Length; i++)
            {
                var substr = line.Substring(i);
                var testDigit = textDigits.Keys.FirstOrDefault(x => substr.StartsWith(x));
                    
                if (char.IsDigit(line[i]) || testDigit != null)
                {
                    if (firstDigit == -1)
                    {
                        if (char.IsDigit(line[i]))
                        {
                            firstDigit = int.Parse(line[i].ToString());
                        }
                        else
                        {
                            firstDigit = textDigits[testDigit];
                        }
                    }
                    else
                    {
                        if (char.IsDigit(line[i]))
                        {
                            secondDigit = int.Parse(line[i].ToString());
                        }
                        else
                        {
                            secondDigit = textDigits[testDigit];
                        }
                    }
                }
                    
            }

            if (secondDigit == -1) secondDigit = firstDigit;
            list.Add(firstDigit * 10 + secondDigit);
        }

        res = list.Sum();
        foreach (var a in list)
        {
            Console.WriteLine(a);
        }

        Console.WriteLine(res);
    }
}