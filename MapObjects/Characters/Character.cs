using System.Diagnostics;

namespace Rogue_Warrior;

public abstract class Character
{
    public enum Team
    {
        Red,
        Blue,
        Neutral
    }

    private static Random _random = new();

    private static readonly char DisplayChar = 'C';

    private static readonly int MaxHealth = 15;

    private static readonly double AttackRange = 1;
    
    private static readonly int Speed = 1;

    public string DebugData = "";

    private static readonly int Strength = 1;
    private int Health;
    private Vector2 Position;

    public Team CharacterTeam = Team.Neutral;

    protected Character(Vector2? position = null, Team team = Team.Red)
    {
        Health = GetMaxHealth();

        Set(position ?? new Vector2(), team);
    }

    public void Set(Vector2 position, Team team)
    {
        CharacterTeam = team;
        Position = position;
    }

    public void Set(int a, Team team)
    {
        CharacterTeam = team;
        Position = new Vector2(a);
    }

    public void Set(int[] a, Team team)
    {
        CharacterTeam = team;
        Position = new Vector2(a);
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public void Damage(int damage)
    {
        Health -= damage;

        if (IsDead())
        {
            Die();
        }
    }

    private void Die()
    {
    }

    public bool IsDead()
    {
        return Health <= 0;
    }

    public virtual int GetSpeed()
    {
        return Speed;
    }
    
    protected virtual double GetAttackRange()
    {
        return AttackRange;
    }

    public virtual char GetDisplay()
    {
        return DisplayChar;
    }

    public virtual int GetStrength()
    {
        return Strength;
    }

    public int GetHealth()
    {
        return Health;
    }

    public virtual int GetMaxHealth()
    {
        return MaxHealth;
    }

    public Vector2 GetPosition()
    {
        return Position;
    }

    public void OnTurn(Character[] characters)
    {
        if (IsDead())
        {
            return;
        }

        DebugData = "   ";

        Vector2 movementOffset = AiCalculateMovement(characters);

        if (IsMoveValid(movementOffset, characters)) Position.Add(movementOffset);

        Character? toAttack = AiCalculateAttack(characters);
        
        if (toAttack == null) return;

        foreach (Character ch in characters)
        {
            if (ch != toAttack)
            {
                continue;
            }

            DebugData += " ta:" + ch;
            DebugData += " hb:" + ch.GetHealth();

            ch.Damage(GetStrength());

            DebugData += " ha:" + ch.GetHealth();

            return;
        }
    }

    private bool IsMoveValid(Vector2 movement, Character[] characters)
    {
        Vector2 newPosition = Vector2.Sum(GetPosition(), movement);

        foreach (Character character in characters)
        {
            if (!character.IsDead() && character.GetPosition().Equals(newPosition)) return false;
        }

        return true;
    }

    private Character? AiCalculateAttack(Character[] characters)
    {
        Character[] inRangeEnemies = GetInRangeEnemies(characters);

        if (characters.Length == 0) return null;

        Character toAttack = null;
        foreach (Character character in inRangeEnemies)
        {
            if (toAttack == null || CompareDanger(toAttack, character) < 0)
            {
                toAttack = character;
            }
        }

        return toAttack;
    }

    private int CompareDanger(Character character1, Character character2)
    {
        return GetDanger(character1) - GetDanger(character2);
    }

    private int GetDanger(Character character)
    {
        return -character.GetHealth() + character.GetStrength() * 2 + GetTypeDanger(character);
    }

    private static int GetTypeDanger(Character character)
    {
        return character switch
        {
            Archer => 100,
            Mage => 1000,
            God => 100000,
            _ => 0
        };
    }

    private Vector2 AiCalculateMovement(Character[] characters)
    {
        Vector2? closestEnemyPosition = GetClosestEnemy(characters);

        DebugData += " ce:" + closestEnemyPosition;
        
        if (closestEnemyPosition == null || GetDistance(closestEnemyPosition) <= GetAttackRange()) return new Vector2();
        
        // TODO: make speed work
        return Vector2.Subtract(closestEnemyPosition, GetPosition()).Normalize();
    }

    private Vector2? GetClosestEnemy(Character[] characters)
    {
        Vector2? closestEnemyPosition = null;

        foreach (Character character in characters)
        {
            if (!character.IsDead() && IsEnemy(character) && (closestEnemyPosition == null ||
                                                              GetDistance(closestEnemyPosition) >
                                                              GetDistance(character)))
            {
                closestEnemyPosition = character.GetPosition();
            }
        }

        return closestEnemyPosition;
    }

    private Character[] GetInRangeEnemies(Character[] characters)
    {
        List<Character> inRangeEnemies = [];

        foreach (Character character in characters)
        {
            if (IsEnemy(character) && GetDistance(character) <= GetAttackRange() && !character.IsDead())
            {
                inRangeEnemies.Add(character);
            }
        }

        DebugData += " ia:" + inRangeEnemies.Count;

        return inRangeEnemies.ToArray();
    }

    private double GetDistance(Character character)
    {
        return GetDistance(character.GetPosition());
    }

    private double GetDistance(Vector2 position)
    {
        return Vector2.Subtract(GetPosition(), position).GetLength();
    }

    private bool IsEnemy(Character character)
    {
        return !CharacterTeam.Equals(character.CharacterTeam);
    }

    public static Character GetRandomCharacter()
    {
        int seed = (int)Math.Floor(_random.NextDouble() * 6);

        switch (seed)
        {
            case 0:
                return new Archer();
            case 1:
                return new Rogue();
            case 2:
                return new Warrior();
            case 3:
                return new Mage();
            case 4:
                return new Spearman();
            case 5:
                return new Paladin();
            default:
                return new God();
        }
    }
}