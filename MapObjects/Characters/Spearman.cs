namespace Rogue_Warrior;

public class Spearman : Character
{
    private static readonly char DisplayChar = 'S';
    
    private static readonly int MaxHealth = 7;

    private static readonly int Strength = 2;
    private static readonly double AttackRange = Math.Sqrt(8);
    
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