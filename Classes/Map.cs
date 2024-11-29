using Rogue_Warrior.MapObjects;

namespace Rogue_Warrior;

public class Map
{
    private MapObject?[,] _map;


    public static MapObject?[,] GetEmpty(Vector2 size)
    {
        MapObject?[,] map = new MapObject?[size[0], size[1]];

        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                map[i, j] = null;
            }
        }
        
        return map;
    }

    public void SetMap(MapObject?[,] map)
    {
        _map = map;
    }

    public void SetMap(Character[]? characters, Obstacle[]? obstacles, Vector2 mapSize)
    {
        _map = new MapObject?[mapSize.X, mapSize.Y];

        for (int i = 0; i < mapSize.X; i++)
        {
            for (int j = 0; j < mapSize.Y; j++)
            {
                _map[i, j] = null;
            }
        }

        if (characters != null)
        {
            foreach (Character character in characters)
            {
                Set(character);
            }
        }

        if (obstacles != null)
        {
            foreach (Obstacle obstacle in obstacles)
            {
                Set(obstacle);
            }
        }
    }

    public void Set(MapObject obj)
    {
        _map[obj.GetPosition().X, obj.GetPosition().Y] = obj;
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
    
    public Character[] GetSortedCharacters()
    {
        Character[] characters = GetCharacters();
        Array.Sort(characters, (x, y) => x.CharacterTeam - y.CharacterTeam);
        
        return characters;
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