using System;
using System.Runtime.InteropServices;
using System.Threading;
using AdventOfCode.Solutions;

namespace AdventOfCode
{
    class Program
    {
        static void Main1(string[] args)
        {
            const int OneGig = 1048576 * 1024;
            var garbage = new byte[OneGig];
            var r = new Random();

            for (int i = 0; i < 20; i++)
            {
                var ptr = Marshal.AllocHGlobal(OneGig);
                r.NextBytes(garbage);
                Marshal.Copy(garbage, 0, ptr, garbage.Length);
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine($"Total increase by: {i+1} Gb");
            }

            Console.WriteLine("End of increase");
            Console.ReadKey();
        }
        
        static void Main(string[] args)
        {
            var solution = new Day12();
            solution.Solution();
        }
    }
}