using System.Diagnostics;
using Rogue_Warrior.MapObjects;

namespace Rogue_Warrior;

public abstract class Character : MapObject
{
    public enum Team
    {
        Red,
        Blue,
        Green,
        Magenta,
        Neutral
    }

    private static Random _random = new();

    protected static readonly char DisplayChar = 'C';

    private static readonly int MaxHealth = 15;

    private static readonly double AttackRange = 1;
    
    private static readonly double Speed = 1.5;

    protected Ai _ai;

    public string DebugData = "";

    private static readonly int Strength = 1;
    private int Health;

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

    public void Set(int[] a, Team team)
    {
        CharacterTeam = team;
        Position = new Vector2(a);
    }

    public void SetTeam(Team team)
    {
        CharacterTeam = team;
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public void Damage(int damage)
    {
        Health -= damage;

        if (IsActive())
        {
            Die();
        }
    }

    private void Die()
    {
    }

    public override bool IsActive()
    {
        return Health > 0;
    }

    public virtual double GetSpeed()
    {
        return Speed;
    }
    
    public virtual double GetAttackRange()
    {
        return AttackRange;
    }

    public override char GetDisplay()
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

    public override Vector2 GetPosition()
    {
        return Position;
    }

    public void OnTurn(Map map)
    {
        if (!IsActive())
        {
            return;
        }

        DebugData = "   /debug/";

        Vector2 movementOffset = _ai.CharacterCalculateMovement(map);
        if (IsMoveValid(movementOffset, map)) Position.Add(movementOffset);

        Character? toAttack = _ai.CharacterCalculateAttack(map);
        if (toAttack == null) return;

        Character[] characters = map.GetCharacters();
        
        foreach (Character ch in characters)
        {
            if (ch != toAttack)
            {
                continue;
            }

            DebugData += " ta:" + ch;

            ch.Damage(GetStrength());

            return;
        }
    }

    private bool IsMoveValid(Vector2 movement, Map map)
    {
        Vector2 newPosition = Vector2.Sum(GetPosition(), movement);

        MapObject?[] objects = map.GetObjects();

        foreach (MapObject? obj in objects)
        {
            if (obj != null && obj.IsActive() && obj.GetPosition().Equals(newPosition)) return false;
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