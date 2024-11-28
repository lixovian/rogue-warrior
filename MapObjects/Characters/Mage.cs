namespace Rogue_Warrior;

public class Mage : Character
{
    private static readonly char DisplayChar = 'M';
    
    private static readonly int MaxHealth = 3;

    private static readonly int Strength = 3;
    private static readonly double AttackRange = Math.Sqrt(18);
    
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

    protected override double GetAttackRange()
    {
        return AttackRange;
    }
}