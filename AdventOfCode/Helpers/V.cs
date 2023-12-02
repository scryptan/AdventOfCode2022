using System;

namespace AdventOfCode.Helpers;

public struct V
{
    public int X { get; set; }
    public int Y { get; set; }

    public V(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public override bool Equals(object obj)
    {
        if (obj is not V v)
            return base.Equals(obj);
        
        return v.X == X && v.Y == Y;
    }

    public bool Equals(V other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator==(V first, V second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(V first, V second)
    {
        return !(first == second);
    }

    public static V operator -(V first, V second)
    {
        return new V
        {
            X = first.X - second.X,
            
        };
    }

    public override string ToString()
    {
        return $"X={X}, Y={Y}";
    }
}