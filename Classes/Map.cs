using Rogue_Warrior.MapObjects;

namespace Rogue_Warrior;

public class Map
{
    private MapObject?[,] _map;

    public void SetMap(MapObject[,] map)
    {
        _map = map;
    }

    public Vector2 GetSize()
    {
        return new Vector2(_map.GetLength(0), _map.GetLength(1));
    }
    
    public MapObject? Get(Vector2 position)
    {
        return _map[position.X, position.Y];
    }
    
    public MapObject? Get(int x, int y)
    {
        return _map[x, y];
    }

    public void SetObject<T>(T obj, Vector2 position)
    {
        if (obj is not MapObject mapObject)
        {
            return;
        }

        _map[position.X, position.Y] = mapObject;
    }

    public Character[] GetCharacters()
    {
        List<Character> characters = new List<Character>();

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j] is not Character character) continue;

                characters.Add(character);
            }
        }

        return characters.ToArray();
    }

    public MapObject[] GetObjects()
    {
        List<MapObject> objects = new List<MapObject>();

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i, j] is not MapObject obj) continue;

                objects.Add(obj);
            }
        }

        return objects.ToArray();
    }

    public bool IsCellFree(Vector2 position)
    {
        return _map[position[0], position[1]] is not null;
    }
}