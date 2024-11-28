namespace Rogue_Warrior;

public class BaseMovementAi : Ai
{
    public override Character? CharacterCalculateAttack(Character[] characters)
    {
        throw new NotImplementedException();
    }

    // волновой алгоритм
    public override Vector2 CharacterCalculateMovement(Character[] characters)
    {
        return new Vector2(0, 0);
    }
}