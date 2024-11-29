namespace Rogue_Warrior;

public class Paladin : Character
{
    protected static readonly char DisplayChar = 'P';
    
    private static readonly int MaxHealth = 15;

    private static readonly int Strength = 1;
    private static readonly double AttackRange = Math.Sqrt(2);
    
    public override char GetDisplay()
    {
        return DisplayChar;
    }

    public override int GetStrength()
    {
        return Strength;
    }

    public override int GetMaxHealth()
    {
        return MaxHealth;
    }

    public override double  GetAttackRange()
    {
        return AttackRange;
    }
}