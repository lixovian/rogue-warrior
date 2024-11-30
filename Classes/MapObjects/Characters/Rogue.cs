namespace Rogue_Warrior;

public class Rogue : Character
{
    protected static readonly char DisplayChar = 'R';
    
    private static readonly int MaxHealth = 5;

    private static readonly double Speed = Math.Sqrt(8);
    
    private static readonly int Strength = 4;
    private static readonly double AttackRange = Math.Sqrt(2);
    
    public override char GetDisplay()
    {
        return DisplayChar;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override int GetStrength()
    {
        return Strength;
    }

    public override int GetMaxHealth()
    {
        return MaxHealth;
    }

    public override double GetAttackRange()
    {
        return AttackRange;
    }
}