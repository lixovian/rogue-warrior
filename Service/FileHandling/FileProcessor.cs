using System.Security;
using System.Text.RegularExpressions;
using Rogue_Warrior.MapObjects;

namespace Rogue_Warrior.Service.FileHandling
{
    public static class FileProcessor
    {
        /// <summary>
        /// Считать данные файла в массив данных о карте.
        /// </summary>
        /// <returns>True - если данные считаны успешно, false - если произошла ошибка при считывании.</returns>
        public static bool ParseFile(string inputPath, out List<MapObject> map, out Vector2 size)
        {
            map = new List<MapObject>();

            try
            {
                string[] lines = File.ReadAllLines(inputPath);

                if (lines.Length == 0)
                {
                    size = new Vector2(0, 0);
                    return false;
                }

                size = new Vector2(lines.Length, SplitLine(lines[0]).Length);
                
                for (var i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    string[] parameters = SplitLine(line);

                    for (int j = 0; j < parameters.Length; j++)
                    {
                        MapObject obj = GetObject(parameters[j].ElementAtOrDefault(0));
                        
                        if (obj == null) continue;
                        
                        obj.SetPosition(new Vector2(i, j));
                        map.Add(obj);
                    }
                }

                // for (int i = 0; i < map.GetLength(0); i++)
                // {
                    // for (int j = 0; j < map.GetLength(1); j++)
                    // {
                        // Console.Out.Write(map[i, j] == null ? '.' : map[i, j].GetDisplay());
                    // }

                    // Console.Out.WriteLine("");
                // }

                return true;
            }
            catch (PathTooLongException)
            {
                Console.Out.WriteLine("Слишком длинный путь.");
            }
            catch (ArgumentException)
            {
                Console.Out.WriteLine("Указанный файл не найден.");
            }
            catch (Exception e) when (e is UnauthorizedAccessException or SecurityException or IOException
                                          or NotSupportedException)
            {
                Console.Out.WriteLine("Ошибка доступа к файлу.");
            }
            catch (Exception e) when (e is DirectoryNotFoundException or FileNotFoundException)
            {
                Console.Out.WriteLine("Указанный путь не найден.");
            }

            size = new Vector2(0, 0);
            return false;
        }

        private static string[] SplitLine(string line)
        {
            return line.Trim().Split(" ");
        }

        private static MapObject? GetObject(char c1, char c2 = ' ')
        {
            switch (c1)
            {
                case 'R':
                    return SpecifyType(Character.Team.Red, c2);
                case 'B':
                    return SpecifyType(Character.Team.Blue, c2);
                case 'G':
                    return SpecifyType(Character.Team.Green, c2);
                case 'M':
                    return SpecifyType(Character.Team.Magenta, c2);
                case 'O':
                    return new Obstacle();
            }
            
            return null;
        }

        private static Character? SpecifyType(Character.Team team, char c)
        {
            switch (c)
            {
                case '?': 
                    return Character.GetRandomCharacter().SetTeam(team);
                case 'r': 
                    return Character.GetRandomRanger().SetTeam(team);
                case 'm': 
                    return Character.GetRandomMelee().SetTeam(team);;
                case 'A':
                    return new Archer().SetTeam(team);
                case 'M':
                    return new Mage().SetTeam(team);
                case 'W':
                    return new Warrior().SetTeam(team);
                case 'S':
                    return new Spearman().SetTeam(team);
                case 'P':
                    return new Paladin().SetTeam(team);
                case 'R':
                    return new Rogue().SetTeam(team);
                case 'G':
                    return new God().SetTeam(team);
            }

            return null;
        }
    }
}