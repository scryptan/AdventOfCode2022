using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Helpers;

public static class Extensions
{
    public static IEnumerable<T> EveryNth<T>(this IEnumerable<T> enumerable, int n, int skipItems = 0) =>
        enumerable.Skip(skipItems).Where((_, index) => index % n == 0);

    public static string[] CreateMap(this IEnumerable<V> enumerable, string point = "#", string empty = ".")
    {
        var points = enumerable.ToHashSet();
        var minX = points.Min(x => x.X);
        var minY = points.Min(x => x.Y);
        var maxX = points.Max(x => x.X);
        var maxY = points.Max(x => x.Y);
        var map = new string[maxY - minY + 1];

        for (int y = minY; y < map.Length; y++)
        {
            var line = new List<string>();
            for (int x = 0; x < maxX - minX + 1; x++)
            {
                var p = new V(x + minX, y + minY);
                line.Add(points.Contains(p) ? point : empty);
            }

            map[y] = string.Join("", line);
        }

        return map;
    }
}