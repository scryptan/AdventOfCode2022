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
    public class Day13
    {
        public void Solution()
        {
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
        }

        private string Part1()
        {
            long res =  GetPackets(File.ReadAllText("./Inputs/day13.txt"))
                .Chunk(2)
                .Select((pair, index) => Compare(pair[0], pair[1]) < 0 ? index + 1 : 0)
                .Sum();

            return $"Part 1: {res} path found";
        }

        private string Part2()
        {
             var divider = GetPackets("[[2]]\n[[6]]").ToList();
            var packets = GetPackets(File.ReadAllText("./Inputs/day13.txt")).Concat(divider).ToList();
            packets.Sort(Compare);
            return $"Part 2: {(packets.IndexOf(divider[0]) + 1) * (packets.IndexOf(divider[1]) + 1)} path found";
        }

        IEnumerable<JsonNode> GetPackets(string input) =>
            from line in input.Split("\n")
            where !string.IsNullOrEmpty(line)
            select JsonNode.Parse(line);

        int Compare(JsonNode nodeA, JsonNode nodeB)
        {
            if (nodeA is JsonValue && nodeB is JsonValue)
            {
                return (int) nodeA - (int) nodeB;
            }
            else
            {
                // It's AoC time, let's exploit FirstOrDefault! 
                // 😈 if all items are equal, compare the length of the arrays 
                var arrayA = nodeA as JsonArray ?? new JsonArray((int) nodeA);
                var arrayB = nodeB as JsonArray ?? new JsonArray((int) nodeB);
                return Enumerable.Zip(arrayA, arrayB)
                    .Select(p => Compare(p.First, p.Second))
                    .FirstOrDefault(c => c != 0, arrayA.Count - arrayB.Count);
            }

        }

        public T[][] Chunk<T>(IEnumerable<T> source, int chunksize)
        {
            var res = new List<T[]>();
            while (source.Any())
            {
                res.Add(source.Take(chunksize).ToArray());
                source = source.Skip(chunksize);
            }

            return res.ToArray();
        }
    }
}