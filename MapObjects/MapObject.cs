namespace Rogue_Warrior.MapObjects;

public abstract class MapObject
{
    private Vector2 Position = new(0, 0);
    private static readonly char DisplayChar = '?';
    
    public virtual void Set(Vector2 position)
    {
        Position = position;
    }

    public virtual void Set(int a)
    {
        Position = new Vector2(a);
    }

    public virtual void Set(int[] a)
    {
        Position = new Vector2(a);
    }

    public virtual bool IsActive()
    {
        return true;
    }

    public virtual char GetDisplay()
    {
        return DisplayChar;
    }

    public virtual Vector2 GetPosition()
    {
        return Position;
    }
}