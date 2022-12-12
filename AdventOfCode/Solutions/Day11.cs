using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions
{
    public class Day11
    {

        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day11.txt")
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
}