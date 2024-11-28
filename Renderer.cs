namespace Rogue_Warrior;

public static class Renderer
{
    public static void Wait()
    {
        Thread.Sleep(Config.SleepTime);
    }

    public static void Rerender(Character[] characters)
    {
        Console.Clear();

        RenderCharacters(characters);
        RenderDescription(characters);
    }

    private static void RenderCharacters(Character[] characters)
    {
        // Console.WriteLine(new string(Config.BorderChar, Config.MapSize + 2));
        
        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            // Console.Write(Config.BorderChar);
            for (int j = 0; j < Config.MapSize[1]; j++)
            {
                Character? character = GetCharacter(i, j,  characters);

                if (character != null && !character.IsDead())
                {
                    Console.ForegroundColor = GetTextColor(character.CharacterTeam);
                    Console.Write(character.GetDisplay());
                    Console.ForegroundColor = GetTextColor(Character.Team.Neutral);
                }
                else
                {
                    Console.Write(Config.GroundChar);
                }
            }
            // Console.Write(Config.BorderChar);
            
            Console.WriteLine();
        }
        // Console.Write(new string(Config.BorderChar, Config.MapSize + 2));
        Console.WriteLine();
    }

    private static Character? GetCharacter(int m, int n, Character[] characters)
    {
        foreach (Character character in characters)
        {
            if (character.GetPosition().Equals(m, n))
            {
                return character;
            }
        }
        
        return null;
    }

    private static void RenderDescription(Character[] characters)
    {
        int displayed = 0;
        for (var i = 0; i < characters.Length; i++)
        {
            if (displayed > 35) return;
            
            var character = characters[i];
            if (character.IsDead())
            {
                continue;
            }

            displayed++;

            Console.ForegroundColor = GetTextColor(character.CharacterTeam);
            Console.Out.Write(character.GetDisplay() + ": ");

            Console.ForegroundColor = Config.HealthColor;

            Console.Out.Write($"{character.GetHealth()}/{character.GetMaxHealth()} ");

            for (int j = 0; j < character.GetMaxHealth(); j++)
            {
                Console.Out.Write(j < character.GetHealth() ? "\u25a0" : ".");
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Out.Write("   ");
            Console.Out.Write(character.GetPosition());
            Console.Out.Write("   ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Out.Write(character.DebugData);

            Console.ForegroundColor = GetTextColor(Character.Team.Neutral);
            Console.Out.WriteLine();
        }
    }

    private static ConsoleColor GetTextColor(Character.Team team)
    {
        switch (team)
        {
            case Character.Team.Blue:
                return ConsoleColor.Blue;
            case Character.Team.Red:
                return ConsoleColor.Red;
            case Character.Team.Neutral:
                return ConsoleColor.White; // TODO: Kostyl
            default:
                return ConsoleColor.Gray;
        }
    }
}