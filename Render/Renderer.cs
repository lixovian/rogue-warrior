using Rogue_Warrior.MapObjects;

namespace Rogue_Warrior;

public static class Renderer
{
    public static void Wait()
    {
        Thread.Sleep(Config.SleepTime);
    }

    public static void Rerender(Map map)
    {
        Console.Clear();

        RenderCharacters(map);
        RenderDescription(map);
    }

    private static void RenderCharacters(Map map)
    {
        // Console.WriteLine(new string(Config.BorderChar, Config.MapSize + 2));

        Vector2 mapSize = map.GetSize();

        for (int i = 0; i < mapSize[0]; i++)
        {
            // Console.Write(Config.BorderChar);
            for (int j = 0; j < mapSize[1]; j++)
            {
                MapObject? obj = map.Get(i, j);

                if (obj != null && obj.IsActive())
                {
                    Console.ForegroundColor = GetTextColor(obj);
                    Console.Write(obj.GetDisplay());
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = Config.GroundColor;
                    Console.Write(Config.GroundChar);
                    Console.ResetColor();
                }
            }
            // Console.Write(Config.BorderChar);

            Console.WriteLine();
        }

        // Console.Write(new string(Config.BorderChar, Config.MapSize + 2));
        Console.WriteLine();
    }

    private static void RenderDescription(Map map)
    {
        Character[] characters = map.GetSortedCharacters();

        int displayed = 0;
        for (var i = 0; i < characters.Length; i++)
        {
            if (displayed > 35) return;

            var character = characters[i];
            if (!character.IsActive())
            {
                continue;
            }

            displayed++;

            Console.ForegroundColor = GetTextColor(character);
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

            Console.ResetColor();
            Console.Out.WriteLine();
        }
    }

    public static ConsoleColor GetTextColor(MapObject obj)
    {
        if (obj is Obstacle)
        {
            return ConsoleColor.DarkYellow;
        }

        if (obj is Character character)
        {
            return GetTeamColor(character.CharacterTeam);
        }

        return ConsoleColor.White;
    }

    public static string GetTeamName(Character.Team team)
    {
        switch (team)
        {
            case Character.Team.Blue:
                return "Blue";
            case Character.Team.Red:
                return "Red";
            case Character.Team.Green:
                return "Green";
            case Character.Team.Magenta:
                return "Magenta";
            default:
                return "Error";
        }
    }
    
    public static ConsoleColor GetTeamColor(Character.Team team)
    {
        switch (team)
        {
            case Character.Team.Blue:
                return ConsoleColor.Blue;
            case Character.Team.Red:
                return ConsoleColor.Red;
            case Character.Team.Green:
                return ConsoleColor.Green;
            case Character.Team.Magenta:
                return ConsoleColor.Magenta;
            default:
                return ConsoleColor.Gray;
        }
    }
}