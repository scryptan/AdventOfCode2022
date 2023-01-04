using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using AdventOfCode.Helpers;
using Environment = System.Environment;

namespace AdventOfCode.Solutions
{
    public class Day11
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day11.txt")
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(x => x.Trim())
                .ToList();

            var monkeys = new List<Monkey>();
            foreach (var line in input)
            {
                var splitted = line.Split(Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var monkey = new Monkey
                {
                    Id = int.Parse(splitted[0].Split(' ',
                            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last()
                        .Replace(":", string.Empty)),
                    Items = splitted[1]
                        .Replace("Starting items: ", String.Empty)
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(long.Parse).ToList(),
                    Operation = splitted[2].Split(" =",
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last(),
                    Divisor = int.Parse(splitted[3].Split(' ',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last()),
                    TrueMonkeyId = int.Parse(splitted[4].Split(' ',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last()),
                    FalseMonkeyId = int.Parse(splitted[5].Split(' ',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last())
                };

                monkeys.Add(monkey);
            }

            Console.WriteLine(Part1(monkeys));
            Console.WriteLine("_______");
            Console.WriteLine(Part2(monkeys));
        }

        private string Part1(List<Monkey> monkeys)
        {
            var res = new List<int>();
            for (int i = 0; i < monkeys.Count; i++)
                res.Add(0);

            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (var monkeyStartingItem in monkey.Items)
                    {
                        res[monkey.Id]++;
                        var op = DoOperation(monkey, monkeyStartingItem);
                        var newWorry = op / 3;

                        if (newWorry % monkey.Divisor == 0)
                            monkeys[monkey.TrueMonkeyId].Items.Add(newWorry);
                        else
                            monkeys[monkey.FalseMonkeyId].Items.Add(newWorry);
                    }

                    monkey.Items.Clear();
                }
            }

            foreach (var monkey in monkeys)
            {
                Console.WriteLine($"Monkey {monkey.Id}: {string.Join(", ", monkey.Items)}");
            }


            for (int i = 0; i < res.Count; i++)
            {
                Console.WriteLine($"Monkey {i} inspected items {res[i]} times.");
            }

            var resPart = res.OrderDescending().Take(2).ToList();

            return $"Part 1: {resPart.First() * resPart.Last()}";
        }

        private string Part2(List<Monkey> monkeys)
        {
            var res = new List<int>();
            for (int i = 0; i < monkeys.Count; i++)
                res.Add(0);

            for (int i = 0; i < 10_000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    foreach (var item in monkey.Items)
                    {
                        res[monkey.Id]++;
                        var newWorry = DoOperation(monkey, item);

                        newWorry %= GetLeastCommonMultiple(monkeys);
                        
                        if (newWorry % monkey.Divisor == 0)
                            monkeys[monkey.TrueMonkeyId].Items.Add(newWorry);
                        else
                            monkeys[monkey.FalseMonkeyId].Items.Add(newWorry);
                    }

                    monkey.Items.Clear();
                }

            }

            foreach (var monkey in monkeys)
                Console.WriteLine($"Monkey {monkey.Id}: {string.Join(", ", monkey.Items)}");


            for (int i = 0; i < res.Count; i++)
                Console.WriteLine($"Monkey {i} inspected items {res[i]} times.");

            var resPart = res.OrderDescending().Take(2).Select(x => (long)x).ToList();

            return $"Part 2: {resPart.First() * resPart.Last()}";
        }

        private static long DoOperation(Monkey monkey, long item)
        {
            var temp = long.TryParse(monkey.Operation.Split(' ')[^1], out var value) ? value : item;

            if (monkey.Operation.Contains("+"))
            {
                item += temp;
            }
            else if (monkey.Operation.Contains("*"))
            {
                item *= temp;
            }

            return item;
        }

        private static long GetLeastCommonMultiple(IEnumerable<Monkey> monkeys) =>
            monkeys.Aggregate(1, (accum, monkey) => accum * monkey.Divisor);
    }

    internal class Monkey
    {
        public int Id { get; set; }
        public List<long> Items { get; set; } = new();
        public string Operation { get; set; }
        public int Divisor { get; set; }
        public int TrueMonkeyId { get; set; }
        public int FalseMonkeyId { get; set; }
    }
}