using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using AdventOfCode.Helpers;

namespace AdventOfCode.Solutions
{
    public class Day10
    {
        internal record Command(string Name, int Arg);

        internal record VmState(int Cycles, int X);

        public void Solution()
        {
            var input = File.ReadAllText($"./Inputs/{GetType().Name.ToLowerInvariant()}.txt")
                .Split("\n")
                .Select(x => x.Trim())
                .ToList();

            var commands = input.Select(x =>
            {
                var splt = x.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                return splt.Length == 2 ? new Command(splt[0], int.Parse(splt[1])) : new Command(splt[0], 0);
            }).ToArray();

            Console.WriteLine(Part1(commands));
            Console.WriteLine(Part2(commands));
        }

        private string Part1(IEnumerable<Command> commands)
        {
            var vm = RunCommands(commands.ToArray());
            var res = vm
                .EveryNth(40, skipItems: 19)
                .Sum(x => x.X * (x.Cycles + 1));

            return $"Part 1: {res}";
        }

        private string Part2(IEnumerable<Command> commands)
        {
            var vm = RunCommands(commands.ToArray());
            var res = vm
                .Where(x => Math.Abs(x.X - x.Cycles % 40) <= 1)
                .Select(x => new V(x.Cycles % 40, x.Cycles / 40))
                .CreateMap("██", "  ");
            
            return $"Part 2: \n{string.Join('\n', res)}";
        }

        private IEnumerable<VmState> RunCommands(params Command[] commands)
        {
            var cycle = 0;
            var x = 1;
            foreach (var command in commands)
            {
                switch (command)
                {
                    case ("addx", var v):
                    {
                        yield return new (cycle++, x);
                        yield return new (cycle++, x);
                        x += v;
                        break;
                    }
                    case ("noop", _):
                    {
                        yield return new(cycle++, x);
                        break;
                    }
                    default:
                        throw new NotSupportedException(command.ToString());
                }
            }
        }
    }
}