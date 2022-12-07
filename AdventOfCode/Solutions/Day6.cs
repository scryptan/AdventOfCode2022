using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day6
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day6.txt")
                .Split("\n")
                .Select(x => x.Trim())
                .ToList()
                .Single();

            var res = 0L;
            var batch = 14;
            for (int i = 0; i < input.Length - batch; i++)
            {
                var sub = input.Substring(i, batch);
                if (sub.ToCharArray().ToHashSet().Count == batch)
                {
                    res = i + batch;
                    break;
                }
            }

            Console.WriteLine(res);
        }
    }
}