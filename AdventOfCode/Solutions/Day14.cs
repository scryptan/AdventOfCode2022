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
    public class Day14
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day14.txt")
                .Split($"{Environment.NewLine}")
                .Select(x => x.Trim())
                .ToList();
            
            // Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }

        private string Part1(List<string> lines)
        {
            var mapDiff = 0;
            var (maxX, maxY, map) = GetMap(lines, mapDiff);
            var sandStartX = map.GetLength(0) - (maxX - 500 - mapDiff/4 + 1);
            // map[sandStartX, 0] = 3;
            // map[sandStartX, maxY + mapDiff/4 + 1 + 2] = 3;
            PrintMap(map);
            Simulate(map, sandStartX);
            PrintMap(map);

            var res = CountSandPieces(map);
            
            return $"Part 1: {res} pieces rest";
        }

        private string Part2(List<string> lines)
        {
            var mapDiff = 1000;
            var (maxX, maxY, map) = GetMap(lines, mapDiff);
            var sandStartX = map.GetLength(0) - (maxX - 500 - mapDiff/4 + 1);
            var floorY = maxY + 2;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                map[x, floorY] = 1;
            }
            // PrintMap(map);
            Simulate(map, sandStartX);
            PrintMap(map);

            var res = CountSandPieces(map);
            return $"Part 2: {res} pieces rest";
        }

        private void Simulate(byte[,] map, int xStart)
        {
            (int x, int y) sandPoint = (xStart, 0);

            var rightCorner = map.GetLength(0);
            var downCorner = map.GetLength(1);
            bool shouldClear = true;
            
            while (sandPoint.y < downCorner - 1)
            {
                if(map[xStart, 0] == 2)
                    break;
                
                // PrintMap(map);
                if (shouldClear)
                {
                    map[sandPoint.x, sandPoint.y] = 0;
                }
                else
                {
                    shouldClear = true;
                }

                if (CanGoTo(map, sandPoint.x, sandPoint.y + 1))
                {
                    ++sandPoint.y;
                    map[sandPoint.x, sandPoint.y] = 2;
                    continue;
                }
                
                if(sandPoint.x - 1 < 0)
                    break;

                if (CanGoTo(map, sandPoint.x - 1, sandPoint.y + 1))
                {
                    ++sandPoint.y;
                    --sandPoint.x;
                    
                    map[sandPoint.x, sandPoint.y] = 2;
                    continue;
                }
                
                if(sandPoint.x + 1 >= rightCorner)
                    break;

                if (CanGoTo(map, sandPoint.x + 1, sandPoint.y + 1))
                {
                    ++sandPoint.y;
                    ++sandPoint.x;
                    
                    map[sandPoint.x, sandPoint.y] = 2;
                    continue;
                }
                
                map[sandPoint.x, sandPoint.y] = 2;
                sandPoint = (xStart, 0);
                shouldClear = false;
            }

            if (shouldClear)
                map[sandPoint.x, sandPoint.y] = 0;
        }

        private bool CanGoTo(byte[,] map, int x, int y) => map[x, y] != 1 && map[x, y] != 2;
        private (int maxX, int maxY, byte[,] map) GetMap(List<string> linesToBuild, int mapDiff)
        {
            var maxX = 0;
            var minX = int.MaxValue;
            
            var maxY = 0;
            var minY = int.MaxValue;
            
            foreach (var line in linesToBuild)
            {
                foreach (var lineByArrow in line.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                {
                    var lineSplit = lineByArrow.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    var x = lineSplit.First();
                    var y = lineSplit.Last();
    
                    if (maxX < x)
                        maxX = x;
                    
                    if (minX > x)
                        minX = x;
                    
                    if (maxY < y)
                        maxY = y;
                    
                    if (minY > y)
                        minY = y;
                }
            }

            maxX += mapDiff / 2;
            // maxY += mapDiff / 2;
            
            var map = new byte[maxX - minX + 1, maxY + 3];

            foreach (var line in linesToBuild)
            {
                (int x, int y) previousPoint = (-1, -1);
                foreach (var lineByArrow in line.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                {
                    var lineSplit = lineByArrow.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    var x = map.GetLength(0) - (maxX - lineSplit.First() + 1) + mapDiff / 4;
                    var y = map.GetLength(1) - (maxY - lineSplit.Last() + 1) - 2;

                    if (previousPoint is {x: >= 0, y: >= 0})
                    {
                        if(previousPoint.x == x)
                            for (var dy = 0; dy <= Math.Abs(previousPoint.y - y); dy++)
                                map[x, previousPoint.y + dy * Math.Sign(y - previousPoint.y)] = 1;
                        
                        if(previousPoint.y == y)
                            for (var dx = 0; dx <= Math.Abs(previousPoint.x - x); dx++)
                                map[previousPoint.x + dx * Math.Sign(x - previousPoint.x), y] = 1;
                        
                    }

                    previousPoint.x = x;
                    previousPoint.y = y;
                }
            }
            
            return (maxX, maxY, map);
        }

        private long CountSandPieces(byte[,] map)
        {
            long res = 0L;
            for (int y = 0; y < map.GetLength(1); y++)
                for (int x = 0; x < map.GetLength(0); x++)
                    if (map[x, y] == 2)
                        ++res;

            return res;
        }
        private void PrintMap(byte[,] map)
        {
            var sb = new StringBuilder();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    var symbol = map[x, y] switch
                    {
                        0 => '.',
                        1 => '#',
                        2 => 'o',
                        3 => '+',
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    sb.Append(symbol);
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb);
        }
    }
}