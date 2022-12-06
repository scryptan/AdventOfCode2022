using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day4
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day4.txt")
                .Split("\n")
                .Select(x => x.Trim())
                .ToList();

            var res = 0L;
            int i = 0;
            for (i = 0; i < input.Count; i++)
            {
                var line = input[i].Split(',').Select(x =>
                {
                    var parsed = x.Split('-').Select(int.Parse);
                    return new IntRange(parsed.First(), parsed.Last());
                }).ToList();

                if (line.First().ContainsAny(line.Last()) || line.Last().ContainsAny(line.First()))
                    res++;
            }


            // res = priorities.Sum();
            Console.WriteLine(res);
        }
    }

    public class IntRange
    {
        public int Start { get; set; }
        public int End { get; set; }

        public IntRange(int start, int end)
        {
            Start = start;
            End = end;
        }

        public bool ContainsRange(IntRange range)
        {
            return Start <= range.Start && End >= range.End;
        }
        
        public bool ContainsAny(IntRange range)
        {
            return Start <= range.Start && End >= range.Start || Start <= range.End && End >= range.End;
        }
    }
}