using Rogue_Warrior.MapObjects;

namespace Rogue_Warrior;

public class Map
{
    private Vector2 _size;
    private List<MapObject> _objects;


    public void SetMap(List<MapObject> objects, Vector2 mapSize)
    {
        _objects = objects;
        _size = mapSize;
    }

    public void SetMap(Character[]? characters, Obstacle[]? obstacles, Vector2 mapSize)
    {
        _objects = new List<MapObject>();
        
        _size = mapSize;

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
        _objects.Add(obj);
    }

    public Vector2 GetSize()
    {
        return _size;
    }
    
    public MapObject? Get(Vector2 position)
    {
        foreach (MapObject obj in _objects)
        {
            if (obj.GetPosition().Equals(position)) return obj;
        }
        
        return null;
    }
    
    public MapObject? Get(int x, int y)
    {
        return Get(new Vector2(x, y));
    }
    
    public Character[] GetCharacters()
    {
        List<Character> characters = new List<Character>();

        for (int i = 0; i < _size[0]; i++)
        {
            for (int j = 0; j < _size[1]; j++)
            {
                if (Get(i, j) is not Character character) continue;

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

        for (int i = 0; i < _size[0]; i++)
        {
            for (int j = 0; j < _size[1]; j++)
            {
                if (Get(i, j) is not MapObject obj) continue;

                objects.Add(obj);
            }
        }

        return objects.ToArray();
    }

    public bool IsCellFree(Vector2 position)
    {
        return Get(position) is not null;
    }
}