namespace Rogue_Warrior;

public class Warrior : Character
{
    private static readonly char DisplayChar = 'W';
    
    private static readonly int MaxHealth = 5;

    private static readonly int Strength = 2;
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

    protected override double  GetAttackRange()
    {
        return AttackRange;
    }
}