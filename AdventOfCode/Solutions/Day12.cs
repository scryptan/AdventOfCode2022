using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions
{
    public class Day12
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day12.txt")
                .Split("\n")
                .Select(x => x.Trim().ToCharArray())
                .ToArray();

            var nodes = new Node[input.Length, input.First().Length];
            Node start = default;
            Node end = default;
            var starts = new List<Node>();

            for (int y = 0; y < input.First().Length; y++)
            {
                for (int x = 0; x < input.Length; x++)
                {
                    var point = new V(x, y);
                    var newNode = new Node
                    {
                        Point = point,
                        Value = input[x][y] - 'a',
                    };

                    nodes[x, y] = newNode;

                    if (input[x][y] == 'S')
                        start = newNode;

                    if (input[x][y] == 'E')
                        end = newNode;

                    if (input[x][y] == 'a')
                        starts.Add(newNode);
                }
            }

            starts.Add(start);
            start!.Value = 0;
            end!.Value = 25;

            Console.WriteLine(Part1(nodes, start, end));
            // Console.WriteLine(Part2(nodes, starts, end));
            Console.WriteLine(Part2(nodes, end, starts));
        }

        private string Part1(Node[,] map, Node start, Node end)
        {
            var res = 0L;

            var visited = new HashSet<Node>
            {
                start
            };
            var queue = new Queue<Node>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Point == end.Point)
                {
                    res = end.Cost;
                    break;
                }

                foreach (var neighbor in GetNeighbors(map, node).Where(n => n.Value <= node.Value + 1))
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        neighbor.Cost = node.Cost + 1;

                        visited.Add(neighbor);
                    }
                }

                if (queue.Count == 0)
                    throw new Exception("BFD doesn't found the way");
            }


            return $"Part 1: {res} path found";
        }

        /*private string Part2(Node[,] map, List<Node> starts, Node end)
        {
            var res = new List<long>();

            foreach (var start in starts)
            {
                foreach (var node in map)
                {
                    node.Cost = 0;
                }
                var visited = new HashSet<Node>
                {
                    start
                };
                var queue = new Queue<Node>();
                queue.Enqueue(start);

                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();

                    if (end.Point == node.Point)
                    {
                        res.Add(node.Cost);
                        break;
                    }

                    foreach (var neighbor in GetNeighbors(map, node).Where(n => n.Value <= node.Value + 1))
                    {
                        if (!visited.Contains(neighbor))
                        {
                            queue.Enqueue(neighbor);
                            neighbor.Cost = node.Cost + 1;

                            visited.Add(neighbor);
                        }
                    }

                }
            }


            return $"Part 2: {res.Min()} path found";
        }*/

        private string Part2(Node[,] map, Node start, List<Node> ends)
        {
            var res = 0L;

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y].Value = Math.Abs(25 - map[x, y].Value);
                }
            }
            foreach (var node in map)
            {
                node.Cost = 0;
            }

            var visited = new HashSet<Node>
            {
                start
            };
            var queue = new Queue<Node>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (ends.Any(x => x.Point == node.Point))
                {
                    res = node.Cost;
                    break;
                }

                foreach (var neighbor in GetNeighbors(map, node).Where(n => n.Value <= node.Value + 1))
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        neighbor.Cost = node.Cost + 1;

                        visited.Add(neighbor);
                    }
                }
            }


            return $"Part 2: {res} path found";
        }

        private StringBuilder GetMap(Node[,] map, Func<Node, int> getValue)
        {
            var sb = new StringBuilder();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    var value = getValue(map[x, y]);
                    var str = value < 10 ? $" {value} " : $"{value} ";
                    sb.Append(str);
                }

                sb.AppendLine();
            }

            return sb;
        }

        private List<Node> GetNeighbors(Node[,] map, Node node)
        {
            var point = node.Point;
            var res = new List<Node>(4);
            foreach (var (dx, dy) in new List<(int dx, int dy)> {(0, 1), (0, -1), (1, 0), (-1, 0)})
            {
                var x = point.X + dx;
                var y = point.Y + dy;
                if (x < 0 || y < 0 || x >= map.GetLength(0) || y >= map.GetLength(1))
                    continue;
                var nodeToCheck = map[x, y];

                // if(nodeToCheck.Value >= node.Value || nodeToCheck.Value < node.Value && nodeToCheck.Value - node.Value == -1)
                res.Add(nodeToCheck);
            }

            return res;
        }
    }

    internal class Node
    {
        public V Point { get; set; }
        public int Value { get; set; }
        public int Cost { get; set; }
    }
}