using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace AdventOfCode.Solutions
{
    public class Day1
    {
        public void Solution()
        {
            var input = File.ReadLines("./Inputs/day1.txt");
            var res = 0L;
            var tmp = 0L;
            var list = new List<long>();  
            
            foreach (var line in input)
            {
                if (long.TryParse(line, out var lineParsed))
                {
                    tmp += lineParsed;
                }
                else
                {
                    list.Add(tmp);
                    tmp = 0;
                }
            }

            res = list.OrderByDescending(x => x).ToList().Take(3).Sum();
            Console.WriteLine(res);
        }
    }
}