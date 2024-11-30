namespace Rogue_Warrior.MapObjects;

public abstract class MapObject
{
    protected Vector2 Position = new(0, 0);
    protected static readonly char DisplayChar = '?';
    
    public virtual bool IsActive()
    {
        return true;
    }

    public virtual char GetDisplay()
    {
        return DisplayChar;
    }
    
    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public virtual Vector2 GetPosition()
    {
        return Position;
    }
}