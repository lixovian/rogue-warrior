namespace Rogue_Warrior;

public class God : Character
{
    protected static readonly char DisplayChar = 'G';

    private static readonly int MaxHealth = 1;

    private static readonly int Strength = 1000;
    private static readonly double AttackRange = 1000;

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

    public override double GetAttackRange()
    {
        return AttackRange;
    }
}