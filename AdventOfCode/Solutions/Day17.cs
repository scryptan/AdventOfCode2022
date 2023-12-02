using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.XPath;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions
{
    public class Day17
    {
        private const string FileUri = "./Inputs/day17.txt";

        public void Solution()
        {
            var input = File.ReadAllText(FileUri)
                .Split($"{Environment.NewLine}")
                .Select(x =>x.Trim())
                .ToList();

            Console.WriteLine(Part1(Array.Empty<(V sensor, V beacon)>().ToList()));
            Console.WriteLine(Part2(Array.Empty<(V sensor, V beacon)>().ToList()));
        }

        private string Part1(List<(V sensor, V beacon)> lines)
        {

            var res = 0;
            return $"Part 1: {res} pieces rest";
        }

        private string Part2(List<(V sensor, V beacon)> lines)
        {
            var res = 0;
            return $"Part 2: {res} pieces rest";
        }
    }
}