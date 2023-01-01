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
    
    public static bool operator==(V first, V second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(V first, V second)
    {
        return !(first == second);
    }
}