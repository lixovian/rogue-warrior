using System.Reflection.Metadata;
using Rogue_Warrior.MapObjects;

namespace Rogue_Warrior;

public class BaseMovementAi(Character main) : Ai(main)
{
    public override Character? CharacterCalculateAttack(Map map)
    {
        throw new NotImplementedException();
    }

    // волновой алгоритм
    public override Vector2 CharacterCalculateMovement(Map map)
    {
        if (CharacterGetInRangeEnemies(map.GetCharacters()).Length > 0) return new Vector2(0, 0);

        GetClosestEnemy(map, out Character? closestEnemy, out int[,] waveMap);

        if (closestEnemy == null) return new Vector2(0, 0);

        Vector2 goalPosition = _main.GetPosition().Copy();
        Vector2 currentPosition = closestEnemy.GetPosition().Copy();

        List<Vector2> path = new List<Vector2>();
        // path.Add(currentPosition.Copy());

        while (Vector2.GetDistance(currentPosition, goalPosition) > Math.Sqrt(2))
        {
            GetNeighboursMin(waveMap, currentPosition.X, currentPosition.Y, out Vector2 direction);
            currentPosition.Add(direction);

            // Console.Out.WriteLine(currentPosition);

            path.Add(currentPosition.Copy());
        }

        // Console.Out.WriteLine();
        // foreach (Vector2 p in path)
        // {
        // Console.Out.Write(p);
        // Console.Out.Write("; ");
        // }
        // Console.Out.WriteLine();
        // Console.Out.WriteLine();
        
        // === debug

        // for (int i = 0; i < map.GetSize()[0]; i++)
        // {
        // for (int j = 0; j < map.GetSize()[1]; j++)
        // {
        // Console.Out.Write($"{waveMap[i, j]:d2},");
        // }
        
        // Console.Out.WriteLine("");
        // }

        // ===
        
        // Console.Out.WriteLine();
        // Console.Out.WriteLine();
        
        // Console.Out.WriteLine("===");
        // Console.Out.WriteLine(new Vector2(0, 1).GetLength());
        // Console.Out.WriteLine(new Vector2(-1, 1).GetLength());
        // Console.Out.WriteLine(new Vector2(1, 1).GetLength() < new Vector2(0, 1).GetLength());
        // Console.Out.WriteLine(new Vector2(-1, 0).GetLength());
        // Console.Out.WriteLine(new Vector2(0, 1).GetLength());
        // Console.Out.WriteLine("===");
        
        
        // Console.Out.WriteLine();
        // Console.Out.WriteLine();
        // Console.Out.WriteLine("&&&" + string.Join(", ",
        // path.FindAll(v => Vector2.GetDistance(v, goalPosition) <= _main.GetSpeed())));

        // Console.Out.WriteLine("..." + _main.GetPosition());

        path = path.FindAll(v => Vector2.GetDistance(v, goalPosition) <= _main.GetSpeed());
        path.Sort((v1, v2) => waveMap[v1[0], v1[1]] - waveMap[v2[0], v2[1]]);

        // Console.Out.WriteLine();
        // foreach (Vector2 p in path)
        // {
        // Console.Out.Write(p);
        // Console.Out.Write("; ");
        // }


        if (path.Count == 0) return new Vector2(0, 0);

        return Vector2.Subtract(path[0], goalPosition);
    }

    protected Character[] CharacterGetInRangeEnemies(Character[] characters)
    {
        List<Character> inRangeEnemies = [];

        foreach (Character character in characters)
        {
            if (_main.IsEnemy(character) && _main.GetDistance(character) <= _main.GetAttackRange() &&
                character.IsActive())
            {
                inRangeEnemies.Add(character);
            }
        }

        return inRangeEnemies.ToArray();
    }

    private void GetClosestEnemy(Map map, out Character? closestEnemy, out int[,] waveMap)
    {
        Vector2 size = map.GetSize();
        waveMap = new int[size[0], size[1]];

        Vector2 startPosition = _main.GetPosition();


        // optimize i know how  wtf is that
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                if (map.Get(i, j) is { } obj && obj.IsActive()) waveMap[i, j] = -1;
            }
        }

        waveMap[startPosition.X, startPosition.Y] = 1;

        bool hasChanged = true;

        // int iterations = 0;

        while (hasChanged)
        {
            hasChanged = false;

            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    int lowestNeighbour = GetNeighboursMin(waveMap, i, j, out _);
                    if (lowestNeighbour == -1) continue;

                    if (waveMap[i, j] > lowestNeighbour + 1 || waveMap[i, j] == 0) hasChanged = true;

                    waveMap[i, j] = waveMap[i, j] == 0
                        ? lowestNeighbour + 1
                        : Math.Min(waveMap[i, j], lowestNeighbour + 1);
                }
            }

            // === debug

            // Console.Clear();
            // for (int i = 0; i < size[0]; i++)
            // {
            //     for (int j = 0; j < size[1]; j++)
            //     {
            //         Console.Out.Write($"{waveMap[i, j]:d3} ");
            //     }
            //
            //     Console.Out.WriteLine("");
            // }
            //
            // Console.Out.WriteLine(iterations);
            // Console.ReadKey();
            // iterations++;

            // ===
        }

        Character? currentClosest = null;
        int moveValue = -1;
        
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                if (i == startPosition.X && j == startPosition.Y) continue;

                if (map.Get(i, j) is Character character && character.IsEnemy(_main))
                {
                    int lowestNeighbour = GetNeighboursMin(waveMap, i, j, out _);
                    if (lowestNeighbour == -1) continue;

                    if (currentClosest == null || moveValue > lowestNeighbour)
                    {
                        currentClosest = character;
                        moveValue = lowestNeighbour;
                    }
                }
            }
        }

        closestEnemy = currentClosest;
    }

    private static int GetNeighboursMin(int[,] waveMap, int x, int y, out Vector2 dir)
    {
        dir = new Vector2(0, 0);

        Vector2 size = new Vector2(waveMap.GetLength(0), waveMap.GetLength(1));

        //kostyl kinda
        int lowestNeighbour = -1;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int cx = x + i, cy = y + j;

                if (CheckSingleTile(waveMap, cx, cy, out int neighbour) && neighbour > 0)
                {
                    Vector2 newDir = new Vector2(i, j);
                    bool isCloser = newDir.GetLength() < dir.GetLength();

                    if (lowestNeighbour > neighbour || lowestNeighbour == -1 || 
                        (lowestNeighbour == neighbour && isCloser)) dir = newDir;

                    lowestNeighbour = lowestNeighbour == -1 ? neighbour : Math.Min(lowestNeighbour, neighbour);
                }
            }
        }

        return lowestNeighbour;
    }

    private static bool CheckSingleTile(int[,] waveMap, int x, int y, out int output)
    {
        Vector2 size = new Vector2(waveMap.GetLength(0), waveMap.GetLength(1));

        if (x < 0 || y < 0 || x >= size[0] || y >= size[1])
        {
            output = 0;
            return false;
        }

        if (waveMap[x, y] == -1)
        {
            output = 0;
            return false;
        }

        output = waveMap[x, y];

        return true;
    }
}