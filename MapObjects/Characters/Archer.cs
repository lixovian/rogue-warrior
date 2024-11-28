namespace Rogue_Warrior;

public class Archer : Character
{
    private static readonly char DisplayChar = 'A';
    
    private static readonly int MaxHealth = 4;

    private static readonly int Strength = 1;
    private static readonly double AttackRange = Math.Sqrt(30);
    
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