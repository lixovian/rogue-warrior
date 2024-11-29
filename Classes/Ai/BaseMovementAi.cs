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
        GetClosestEnemy(map, out Character? closestEnemy, out int[,] waveMap);
        
        if (closestEnemy == null) return new Vector2(0, 0);
        
        Vector2 goalPosition = _main.GetPosition(), currentPosition = closestEnemy.GetPosition();
        while (Vector2.GetDistance(currentPosition, goalPosition) > Math.Sqrt(2))
        {
            GetNeighboursMin(waveMap, currentPosition.X, currentPosition.Y, out Vector2 direction);
            currentPosition.Minus(direction);
        }
        
        return Vector2.Subtract(currentPosition, goalPosition);
    }

    private void GetClosestEnemy(Map map, out Character? closestEnemy, out int[,] waveMap)
    {
        Vector2 size = map.GetSize();
        waveMap = new int[size[0], size[1]];

        Vector2 startPosition = _main.GetPosition();

        waveMap[startPosition.X, startPosition.Y] = 1;

        bool hasChanged = true;
        
        while (hasChanged)
        {
            hasChanged = false;
            
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    int lowestNeighbour = GetNeighboursMin(waveMap, i, j, out _);
                    if (lowestNeighbour == -1) continue;

                    if (waveMap[i, j] > lowestNeighbour + 1) hasChanged = true;

                    waveMap[i, j] = waveMap[i, j] == 0 ? lowestNeighbour : Math.Min(waveMap[i, j], lowestNeighbour + 1);
                }
            }

            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    if (i == startPosition.X && j == startPosition.Y) continue;

                    if (map.Get(i, j) is Character character && waveMap[i, j] > 0 && character.IsEnemy(_main))
                    {
                        closestEnemy = character;
                        return;
                    }
                }
            }
        }
        
        closestEnemy = null;
    }
    
    private static int GetNeighboursMin(int[,] waveMap, int x, int y, out Vector2 dir)
    {
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
                    if (lowestNeighbour > neighbour) dir = new Vector2(i, j);
                    
                    lowestNeighbour = lowestNeighbour == -1 ? neighbour : Math.Min(lowestNeighbour, neighbour);
                }
            }
        }

        dir = new Vector2(0, 0);
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