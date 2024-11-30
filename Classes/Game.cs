namespace Rogue_Warrior;

public class Game
{
    public Map Map;
    private static Random _random = new Random();

    public Game()
    {
        Map = new Map();
    }
    
    public void MoveCharacters()
    {
        Character[] characters = Map.GetCharacters();
        
        foreach (Character character in characters)
        {
            character.OnTurn(Map);
        }
    }
}