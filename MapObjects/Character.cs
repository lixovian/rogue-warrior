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

    private Ai _ai;

    public string DebugData = "";

    private static readonly int Strength = 1;
    private int Health;
    private Vector2 Position;

    public Team CharacterTeam = Team.Neutral;

    protected Character(Vector2? position = null, Team team = Team.Red)
    {
        Health = GetMaxHealth();
        Set(position ?? new Vector2(), team);

        _ai = new BaseAi(this);
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
    
    public virtual double GetAttackRange()
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

        Vector2 movementOffset = _ai.CharacterCalculateMovement(characters);

        if (IsMoveValid(movementOffset, characters)) Position.Add(movementOffset);

        Character? toAttack = _ai.CharacterCalculateAttack(characters);
        
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

    public double GetDistance(Character character)
    {
        return Vector2.GetDistance(GetPosition(), character.GetPosition());
    }

    public bool IsEnemy(Character character)
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