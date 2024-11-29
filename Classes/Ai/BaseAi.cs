namespace Rogue_Warrior;

public class BaseAi(Character main) : BaseMovementAi(main)
{
    // TODO: Fix kostyl
    public override Character? CharacterCalculateAttack(Map map)
    {
        Character[] characters = map.GetCharacters();
        
        Character[] inRangeEnemies = CharacterGetInRangeEnemies(characters);

        if (characters.Length == 0) return null;

        Character toAttack = null;
        foreach (Character character in inRangeEnemies)
        {
            if (toAttack == null || CharactersCompareDanger(toAttack, character) < 0)
            {
                toAttack = character;
            }
        }

        return toAttack;
    }

    
    // TODO: Fix kostyl
    public override Vector2 CharacterCalculateMovement(Map map)
    {
        Character[] characters = map.GetCharacters();
        
        Character? closestEnemy = GetClosestEnemy(characters);

        if (closestEnemy == null || _main.GetDistance(closestEnemy) <= _main.GetAttackRange())
            return new Vector2();

        // TODO: make speed work properly
        Vector2 offset = Vector2.Subtract(closestEnemy.GetPosition(), _main.GetPosition()).Limit(new Vector2(_main.GetSpeed()));

        return offset;
    }

    private int CharactersCompareDanger(Character character1, Character character2)
    {
        return CharacterGetDanger(character1) - CharacterGetDanger(character2);
    }

    private int CharacterGetDanger(Character character)
    {
        return -character.GetHealth() + character.GetStrength() * 2 + CharacterGetTypeDanger(character);
    }

    private int CharacterGetTypeDanger(Character character)
    {
        return character switch
        {
            Archer => 100,
            Mage => 1000,
            God => 100000,
            _ => 0
        };
    }

    private Character? GetClosestEnemy(Character[] characters)
    {
        Character? closestEnemy = null;

        foreach (Character character in characters)
        {
            if (!character.IsDead() && _main.IsEnemy(character) && (closestEnemy == null ||
                                                                    _main.GetDistance(closestEnemy) >
                                                                    _main.GetDistance(character)))
            {
                closestEnemy = character;
            }
        }

        return closestEnemy;
    }

    private Character[] CharacterGetInRangeEnemies(Character[] characters)
    {
        List<Character> inRangeEnemies = [];

        foreach (Character character in characters)
        {
            if (_main.IsEnemy(character) && _main.GetDistance(character) <= _main.GetAttackRange() && !character.IsDead())
            {
                inRangeEnemies.Add(character);
            }
        }

        return inRangeEnemies.ToArray();
    }
}