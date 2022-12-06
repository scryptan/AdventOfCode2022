using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day5
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day5.txt")
                .Split("\n")
                .Select(x => x.Trim())
                .ToList();
            // [M]     [B]             [N]
            // [T]     [H]     [V] [Q]         [H]
            // [Q]     [N]     [H] [W] [T]     [Q]
            // [V]     [P] [F] [Q] [P] [C]     [R]
            // [C]     [D] [T] [N] [N] [L] [S] [J]
            // [D] [V] [W] [R] [M] [G] [R] [N] [D]
            // [S] [F] [Q] [Q] [F] [F] [F] [Z] [S]
            // [N] [M] [F] [D] [R] [C] [W] [T] [M]
            // 1   2   3   4   5   6   7   8   9 

            var four = new List<char>
            {
                'F',
                'T',
                'R',
                'Q',
                'D',
            };
            var five = new List<char>
            {
                'B',
                'V',
                'H',
                'Q',
                'N',
                'M',
                'F',
                'R',
            };
            var six = new List<char>{
                'Q',
                'W',
                'P',
                'N',
                'G',
                'F',
                'C',
            };
            var seven = new List<char>{
                'T',
                'C',
                'L',
                'R',
                'F',
                'W',
            };
            var eight = new List<char>{
               'S',
               'N',
               'Z',
               'T',
            };
            var nine = new List<char>{
                'N',
                'H',
                'Q',
                'R',
                'J',
                'D',
                'S',
                'M',
            };

            four.Reverse();
            five.Reverse();
            six.Reverse();
            seven.Reverse();
            eight.Reverse();
            nine.Reverse();
            
            var stacks = new List<Stack<char>>
            {
                new(new List<char> { 'N', 'S', 'D', 'C', 'V', 'Q', 'T' }),
                new(new List<char> { 'M', 'F', 'V'}),
                new(new List<char> { 'F', 'Q', 'W', 'D', 'P', 'N', 'H', 'M' }),
                new(four),
                new(five),
                new(six),
                new(seven),
                new(eight),
                new(nine),
            };


            var res = "";
            foreach (var line in input)
            {
                var instructions = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var count = int.Parse(instructions[1]);
                var from = int.Parse(instructions[3]);
                var to = int.Parse(instructions[5]);
                
                var stackFrom = stacks[from - 1];
                var stackTo = stacks[to - 1];
                
                var tmp = new Stack<char>();
                for (int i = 0; i < count; i++)
                    tmp.Push(stackFrom.Pop()); //123 
                
                for (int i = 0; i < count; i++)
                    stackTo.Push(tmp.Pop());
            }

            foreach (var stack in stacks)
            {
                res += stack.Peek();
            }
            Console.WriteLine(res);
        }
    }
}