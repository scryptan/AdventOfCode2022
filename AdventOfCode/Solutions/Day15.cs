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
    public class Day15
    {
        private const string FileUri = "./Inputs/day15.txt";

        public void Solution()
        {
            var input = File.ReadAllText(FileUri)
                .Split($"{Environment.NewLine}")
                .Select(x => Parse(x.Trim()))
                .ToList();

            Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }

        private string Part1(List<(V sensor, V beacon)> lines)
        {
            var res = GetRanges(lines, 2_000_000).Select(x =>  x.end - x.start).Sum();

            return $"Part 1: {res} pieces rest";
        }

        private string Part2(List<(V sensor, V beacon)> lines)
        {
            const long uppBound = 4_000_000;
            for (int y = 0; y < uppBound; y++)
            {
                var ranges = GetRanges(lines, y);
                var rangesLength = ranges.Count;
                var start = ranges.First().start;
                var end = ranges.First().end;
                
                if(rangesLength == 1 && start <= 0 && end >= uppBound)
                    continue;
                
                if(rangesLength == 1 && start > 0)
                    return $"Part 2: {0 * uppBound + y} first";
                
                if(rangesLength == 1 && end < uppBound)
                    return $"Part 2: {uppBound * uppBound + y} second";
                
                if(rangesLength == 2)
                    return $"Part 2: {(end + 1) * uppBound + y} third";
            }
            var res = 0;
            return $"Part 2: {res} pieces rest";
        }

        private List<(long start, long end)> GetRanges(List<(V sensor, V beacon)> lines, int lineToSearch)
        {
            var ranges = new List<(long start, long end)>();

            foreach (var (sensor, beacon) in lines)
            {
                var radius = GetManhattanLength(beacon, sensor);
                var distance = Math.Abs(lineToSearch - sensor.Y);
                if (distance < radius)
                {
                    var left = radius - distance;
                    var start = sensor.X - left;
                    var end = sensor.X + left;
                    ranges.Add((start, end));
                }
            }

            ranges.Sort();
            var i = 0;
            while (i < ranges.Count - 1)
            {
                if (ranges[i].end >= ranges[i + 1].start)
                {
                    ranges[i] = (ranges[i].start, Math.Max(ranges[i].end, ranges[i + 1].end));
                    ranges.RemoveAt(i + 1);
                    continue;
                }

                i++;
            }

            return ranges;
        }

        private (V sensor, V beacon) Parse(string line)
        {
            var splitLine = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var forSensor = splitLine[0]
                .Replace("Sensor at", string.Empty)
                .Replace("x=", string.Empty)
                .Replace("y=", string.Empty)
                .Trim()
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var sensor = new V(forSensor.First(), forSensor.Last());

            var forBeacon = splitLine[1]
                .Replace("closest beacon is at ", string.Empty)
                .Replace("x=", string.Empty)
                .Replace("y=", string.Empty)
                .Trim()
                .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var beacon = new V(forBeacon.First(), forBeacon.Last());
            return (sensor, beacon);
        }

        private long GetManhattanLength(V start, V end)
        {
            return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
        }
    }
}