using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day3
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day3.txt")
                .Split("\n")
                .Select(x => x.Trim())
                .ToList();
            
            var res = 0L;
            var priorities = new List<int>();
            int i = 0;
            Console.WriteLine(input.Count);
            for (i = 0; i < input.Count; i+=3)
            {
                var first = input[i].ToCharArray().ToHashSet();
                var second = input[i+1].ToCharArray().ToHashSet();
                var third = input[i+2].ToCharArray().ToHashSet();

                var symbol = first.Intersect(second.Intersect(third)).Single();
                var result = (int)symbol;
                if (result >= 97)
                {
                    result -= 96;
                }
                else
                {
                    result = result - 64 + 26;
                }
             
                priorities.Add(result);
                Console.WriteLine(symbol);
            }

            
            // foreach (var line in input)
            // {
            //     var mid = line.Length / 2;
            //     
            //     var left = line.Substring(0, mid).ToCharArray().ToHashSet();
            //     var right = line.Substring(mid, mid).ToCharArray().ToHashSet();
            //     
            //     var result = (int)left.Intersect(right).ToList().Single();
            //     if (result >= 97)
            //     {
            //         result = result - 96;
            //     }
            //     else
            //     {
            //         result = (result - 64) + 26;
            //     }
            //  
            //     priorities.Add(result);
            // }

            res = priorities.Sum();
            Console.WriteLine(res);
        }
    }
}