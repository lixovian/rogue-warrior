namespace Rogue_Warrior;

public abstract class Ai(Character main)
{
    protected Character _main = main;
    
    public abstract Character? CharacterCalculateAttack(Map map);
    public abstract Vector2 CharacterCalculateMovement(Map map);
}