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
                var operationArray = splitted[2].Replace("Operation: new = ", string.Empty).Split(' ',
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                Func<BigInteger, BigInteger> operation = x => x;

                var opSet = false;
                if (operationArray[0] == "old" && operationArray[2] == "old")
                {
                    operation = operationArray[1] switch
                    {
                        "*" => i => BigInteger.Multiply(i, i),
                        "+" => i => BigInteger.Add(i, i),
                        "-" => i => BigInteger.Subtract(i, i),
                        "/" => i => BigInteger.Divide(i, i),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    opSet = true;
                }
                if (opSet != true && operationArray[0] == "old")
                {
                    var secondOp = int.Parse(operationArray[2]);
                    operation = operationArray[1] switch
                    {
                        "*" => i => BigInteger.Multiply(i, secondOp),
                        "+" => i => BigInteger.Add(i, secondOp),
                        "-" => i => BigInteger.Subtract(i, secondOp),
                        "/" => i => BigInteger.Divide(i, secondOp),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    opSet = true;
                }
                
                if (opSet != true && operationArray[2] == "old")
                {
                    var secondOp = int.Parse(operationArray[0]);
                    operation = operationArray[1] switch
                    {
                        "*" => i => BigInteger.Multiply(i, secondOp),
                        "+" => i => BigInteger.Add(i, secondOp),
                        "-" => i => BigInteger.Subtract(i, secondOp),
                        "/" => i => BigInteger.Divide(i, secondOp),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
                else if(opSet != true)
                {
                    var firstOp = int.Parse(operationArray[0]);
                    var secondOp = int.Parse(operationArray[2]);
                    operation = operationArray[1] switch
                    {
                        "*" => i => BigInteger.Multiply(firstOp, secondOp),
                        "+" => i => BigInteger.Add(firstOp, secondOp),
                        "-" => i => BigInteger.Subtract(firstOp, secondOp),
                        "/" => i => BigInteger.Divide(firstOp, secondOp),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }


                var monkey = new Monkey
                {
                    Id = int.Parse(splitted[0].Split(' ',
                            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last()
                        .Replace(":", string.Empty)),
                    Items = splitted[1]
                        .Replace("Starting items: ", String.Empty)
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(BigInteger.Parse).ToList(),
                    Operation = operation,
                    Test = c => BigInteger.Remainder(c, (BigInteger.Parse(splitted[3].Split(' ',
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Last()))) == 0,
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
                        var newWorry = BigInteger.Divide(monkey.Operation(monkeyStartingItem), 3);
                        
                        if (monkey.Test(newWorry))
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
                    foreach (var monkeyStartingItem in monkey.Items)
                    {
                        res[monkey.Id]++;
                        var newWorry = monkey.Operation(monkeyStartingItem);
                        
                        if (monkey.Test(newWorry))
                            monkeys[monkey.TrueMonkeyId].Items.Add(newWorry);
                        else
                            monkeys[monkey.FalseMonkeyId].Items.Add(newWorry);
                    }
                    monkey.Items.Clear();
                }

                Console.WriteLine(i);
            }
            
            foreach (var monkey in monkeys)
                Console.WriteLine($"Monkey {monkey.Id}: {string.Join(", ", monkey.Items)}");


            for (int i = 0; i < res.Count; i++)
                Console.WriteLine($"Monkey {i} inspected items {res[i]} times.");

            var resPart = res.OrderDescending().Take(2).ToList();
            
            return $"Part 2: {resPart.First() * resPart.Last()}";
        }
    }

    internal class Monkey
    {
        public int Id { get; set; }
        public List<BigInteger> Items { get; set; } = new();
        public Func<BigInteger, BigInteger> Operation { get; set; }
        public Func<BigInteger, bool> Test { get; set; }
        public int TrueMonkeyId { get; set; }
        public int FalseMonkeyId { get; set; }
    }
}