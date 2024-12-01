using System.Diagnostics;
using AdventOfCode2024.Solutions;

var solution = new Day1();

var start = Stopwatch.StartNew();
solution.Solution();
Console.WriteLine($"{start.ElapsedMilliseconds * 1000}µs");