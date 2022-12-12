using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Solutions
{
    public class Day9
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day9.txt")
                .Split("\n")
                .Select(x => x.Trim())
                .ToList();

            var fieldSize = 490;
            var field = new int[fieldSize, fieldSize];
            var headPos = new Vector2(fieldSize / 2, fieldSize / 2);
            var tails = new List<Vector2>();
            for (int i = 0; i < 9; i++)
                tails.Add(headPos);
            var res = 0L;

            foreach (var line in input)
            {
                var split = line.Split(' ');
                var move = split[0];
                var step = int.Parse(split[1]);

                var headDir = move switch
                {
                    "R" => new Vector2(1, 0),
                    "L" => new Vector2(-1, 0),
                    "U" => new Vector2(0, -1),
                    "D" => new Vector2(0, 1),
                    _ => throw new ArgumentOutOfRangeException()
                };

                for (int i = 0; i < step; i++)
                {
                    headPos += headDir;
                    for (int j = 0; j < tails.Count; j++)
                    {
                        var tempHeadPos = headPos;
                        var tailPos = tails[j];

                        if (j > 0)
                            tempHeadPos = tails[j - 1];

                        if (j == tails.Count - 1)
                            ++field[(int)tailPos.Y, (int)tailPos.X];

                        if (Vector2.Distance(tailPos, tempHeadPos) > 1.5)
                        {
                            var tailDir = new Vector2();
                            if (Math.Abs(tempHeadPos.X - tailPos.X) > 0.1 && Math.Abs(tempHeadPos.Y - tailPos.Y) > 0.1)
                            {
                                if (tempHeadPos.X > tailPos.X)
                                    tailDir.X = 1;
                                else
                                    tailDir.X = -1;

                                if (tempHeadPos.Y > tailPos.Y)
                                    tailDir.Y = 1;
                                else
                                    tailDir.Y = -1;

                                tails[j] += tailDir;
                                continue;
                            }

                            if (Math.Abs(tempHeadPos.X - tailPos.X) > 0.1)
                            {
                                if (tempHeadPos.X > tailPos.X)
                                    tailDir.X = 1;
                                else
                                    tailDir.X = -1;
                                tails[j] += tailDir;
                                continue;
                            }

                            if (Math.Abs(tempHeadPos.Y - tailPos.Y) > 0.1)
                            {
                                if (tempHeadPos.Y > tailPos.Y)
                                    tailDir.Y = 1;
                                else
                                    tailDir.Y = -1;

                                tails[j] += tailDir;
                                continue;
                            }
                        }
                    }
                }
            }

            foreach (var i in field)
            {
                if (i > 0)
                    ++res;
            }

            var sb = new StringBuilder();
            for (int i = 0; i < fieldSize; i++)
            {
                var lineSb = new StringBuilder();
                for (int j = 0; j < fieldSize; j++)
                {
                    var r = field[j, i] > 0 ? "#" : ".";
                    lineSb.Append(r);
                }

                if (lineSb.ToString().Contains("#"))
                    sb.AppendLine(lineSb.ToString());
            }

            sb.AppendLine();
            File.WriteAllText(@"C:\Users\scryptan\Desktop\output.txt", sb.ToString());
            Console.WriteLine(res);
        }
    }
}