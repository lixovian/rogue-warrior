namespace Rogue_Warrior;

public abstract class Ai
{
    public abstract Character? CharacterCalculateAttack(Character[] characters);
    public abstract Vector2 CharacterCalculateMovement(Character[] characters);
}