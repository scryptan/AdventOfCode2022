using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day2
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day2.txt").Split("\n").ToList();
            var res = 0;
            
            foreach (var line in input)
            {
                var splitted = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var elf = splitted[0][0] - 65;
                var me = splitted[1][0] - 'X';
                
                //10595
                //9541

                res += (elf + me + 2) % 3 + 1;

                if (me == 1) // draw
                {
                    res += 3;
                    continue;
                }

                if (me == 2) // win
                {
                    res += 6;
                }
                
                // break;
            }

            Console.WriteLine(res);
        }
    }
}