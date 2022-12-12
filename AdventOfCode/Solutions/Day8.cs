using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day8
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day8.txt")
                .Split("\n")
                .Select(x => x.Trim().ToCharArray().Select(c => int.Parse(c.ToString())).ToList())
                .ToList();

            var res = 0L;
            var lineLength = input.First().Count;
            for (int i = 1; i < input.Count - 1; i++)
            {
                for (int j = 1; j < lineLength - 1; j++)
                {
                    var currTree = input[i][j];

                    //  to hte left
                    var left = j - 1;
                    var leftCount = 0;
                    var isLeft = false;
                    while (left >= 0)
                    {
                        leftCount++;
                        if (currTree <= input[i][left])
                            break;
                        
                        --left;
                    }

                    if (left <= -1)
                        isLeft = true;

                    var right = j + 1;
                    var rightCount = 0;
                    var isRight = false;
                    while (right <= lineLength - 1)
                    {
                        rightCount++;
                        if (currTree <= input[i][right])
                            break;
                        ++right;
                    }

                    if (right >= lineLength)
                        isRight = true;

                    var upper = i - 1;
                    var upperCount = 0;
                    var isUpper = false;
                    while (upper >= 0)
                    {
                        upperCount++;
                        if (currTree <= input[upper][j])
                            break;

                        --upper;
                    }


                    if (upper <= -1)
                        isUpper = true;

                    var down = i + 1;
                    var downCount = 0;
                    var isDown = false;
                    while (down <= lineLength - 1)
                    {
                        downCount++;
                        if (currTree <= input[down][j])
                            break;

                        ++down;
                    }


                    if (down >= lineLength)
                        isDown = true;

                    if (isLeft || isRight || isUpper || isDown)
                    {
                        var temp = leftCount * rightCount * upperCount * downCount;
                        if (res < temp)
                            res = temp;
                    }
                }
            }

            // res += 2 * lineLength + 2 * input.Count - 4;

            Console.WriteLine(res);
        }
    }
}