using Rogue_Warrior.MapObjects;

namespace Rogue_Warrior;

public class Map
{
    private MapObject[,] _map;

    public void SetMap(MapObject[,] map)
    {
        _map = map;
    }

    public void SetObject<T>(T obj, Vector2 position)
    {
        if (obj is not MapObject mapObject)
        {
            return;
        }
        
        _map[position[0], position[1]] = mapObject;
    }
    
    public Character[] GetCharacters()
    {
        return [];
    }

    public MapObject[] GetObjects()
    {
        return [];
    }
}