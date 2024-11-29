namespace Rogue_Warrior;

public class Game
{
    public Map Map;
    private static Random _random = new Random();

    public Game()
    {
        Map = new Map();
        SetDefault();
    }

    private void SetDefault()
    {
        Character[] characters = new Character[17];

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = Character.GetRandomCharacter();
        }
        
        characters[0].Set([0, 0], Character.Team.Red);
        characters[1].Set([1, 0], Character.Team.Red);
        characters[2].Set([0, 1], Character.Team.Red);
        characters[3].Set([1, 1], Character.Team.Red);
        characters[4].Set([1, 2], Character.Team.Red);
        characters[5].Set([2, 1], Character.Team.Red);
        characters[6].Set([2, 2], Character.Team.Red);
        characters[7].Set([0, 2], Character.Team.Red);
        characters[8].Set([2, 0], Character.Team.Red);
        
        characters[9].Set([Config.MapSize[0] - 1, Config.MapSize[1] - 1], Character.Team.Blue);
        characters[10].Set([Config.MapSize[0] - 1, Config.MapSize[1] - 2], Character.Team.Blue);
        characters[11].Set([Config.MapSize[0] - 2, Config.MapSize[1] - 1], Character.Team.Blue);
        characters[12].Set([Config.MapSize[0] - 2, Config.MapSize[1] - 3], Character.Team.Blue);
        characters[13].Set([Config.MapSize[0] - 3, Config.MapSize[1] - 2], Character.Team.Blue);
        characters[14].Set([Config.MapSize[0] - 2, Config.MapSize[1] - 2], Character.Team.Blue);
        characters[15].Set([Config.MapSize[0] - 3, Config.MapSize[1] - 1], Character.Team.Blue);
        characters[16].Set([Config.MapSize[0] - 1, Config.MapSize[1] - 3], Character.Team.Blue);
        
        Map.SetMap(characters, null, Config.MapSize);
    }

    public void SetCustom()
    {
        Character[] characters = new Character[40];
        
        double archerProbability = 0.8;
        
        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            characters[i] = _random.NextDouble() < archerProbability ? new Archer() : new Mage();
            characters[i].Set([i, 0], Character.Team.Red);
        }
        
        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            characters[i + 10] = Character.GetRandomCharacter();
            characters[i + 10].Set([i, 1], Character.Team.Red);
        }
        
        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            characters[i + 20] = _random.NextDouble() < archerProbability ? new Archer() : new Mage();
            characters[i + 20].Set([i, Config.MapSize[1] - 1], Character.Team.Blue);
        }
        
        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            characters[i + 30] = Character.GetRandomCharacter();
            characters[i + 30].Set([i, Config.MapSize[1] - 2], Character.Team.Blue);
        }

        Map.SetMap(characters, [], Config.MapSize);
    }

    public void SetCustom2()
    {
        int columnsAmount = 4;
     
        Character[] characters = new Character[1 + columnsAmount * 10];

        characters[0] = new God();
        characters[0].Set([Config.MapSize[0]/2, Config.MapSize[1]-1], Character.Team.Blue);
        
        for (int j = 0; j < columnsAmount; j++)
        {
            for (int i = 0; i < Config.MapSize[0]; i++)
            {
                characters[1+i+10*j] = Character.GetRandomCharacter();
                characters[1+i+10*j].Set([i, j], Character.Team.Red);
            }
        }
        
        Map.SetMap(characters, [], Config.MapSize);
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