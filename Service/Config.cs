namespace Rogue_Warrior;

public class Config
{
    public static readonly Vector2 MapSize = new(10, 40);
    public const int TeamSize = 3;
    public const int TeamAmount = 2;
    public const int SleepTime = 2500;
    
    public const char GroundChar = '.';
    public const ConsoleColor HealthColor = ConsoleColor.Green;
    public const ConsoleColor FontColor = ConsoleColor.White;
}