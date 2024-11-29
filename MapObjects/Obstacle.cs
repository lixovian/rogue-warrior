namespace Rogue_Warrior.MapObjects;

public class Obstacle : MapObject
{
    private static Random _random = new();
    
    private static readonly char[] DisplayChars = ['%', '+', '&', '@', '#', 'Ё', 'Ш', 'Ы'];
    private char DisplayChar;
    
    public Obstacle()
    {
        DisplayChar = DisplayChars[_random.Next(DisplayChars.Length)];
    }

    public override char GetDisplay()
    {
        return DisplayChar;
    }
}