namespace Rogue_Warrior;

public class Vector2
{
    private int x, y;

    public Vector2(int x = default, int y = default)
    {
        this.x = x;
        this.y = y;
    }
    
    public Vector2(int a)
    {
        x = y = a;
    }

    public Vector2(int[] a)
    {
        x = a[0];
        y = a[1];
    }
    
    public Vector2()
    {
        x = y = 0;
    }

    public Vector2 Add(Vector2 b)
    {
        return CopyTo(Sum(this, b));
    }

    public Vector2 Minus(Vector2 b)
    {
        return CopyTo(Subtract(this, b));
    }

    public Vector2 Multiply(Vector2 b)
    {
        return CopyTo(Multiply(this, b));
    }
    
    public Vector2 Divide(Vector2 b)
    {
        return CopyTo(Divide(this, b));
    }

    public Vector2 Normalize()
    {
        x = x switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => x
        };

        y = y switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => y
        };

        return this;
    }

    public double GetLength()
    {
        return Math.Sqrt(x * x + y * y);
    }

    public Vector2 Reverse()
    {
        (x, y) = (y, x);

        return this;
    }

    public Vector2 CopyTo(Vector2 a)
    {
        x = a.x;
        y = a.y;

        return this;
    }
    
    public Vector2 GetOpposite()
    {
        return new Vector2(-x, -y);
    }

    public Vector2 Abs()
    {
        x = Math.Abs(x);
        y = Math.Abs(y);

        return this;
    }

    public Vector2 Limit(Vector2 mask)
    {
        mask.Abs();
        
        x = Math.Min(x, mask.x);
        y = Math.Min(y, mask.y);
        
        x = Math.Max(x, -mask.x);
        y = Math.Max(x, -mask.y);
        
        return this;
    }
    
    public bool Equals(Vector2 b)
    {
        return Equals(this, b);
    }

    public bool Equals(int x1, int y1)
    {
        return x == x1 && y == y1;
    }

    public int this[int i] => (i == 0) ? x : y;
    
    public int X => x;
    public int Y => y;
    
    public static bool Equals(Vector2 a, Vector2 b)
    {
        return a.x == b.x && a.y == b.y;
    }
    
    public static double GetDistance(Vector2 a, Vector2 b)
    {
        return Subtract(a, b).GetLength();
    }

    public static Vector2 Sum(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }
    
    public static Vector2 Subtract(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y);
    }
    
    public static Vector2 Multiply(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }
    
    public static Vector2 Divide(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x / b.x, a.y / b.y);
    }

    public override string ToString()
    {
        return $"x={x}, y={y}";
    }
}