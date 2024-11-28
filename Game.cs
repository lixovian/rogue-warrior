namespace Rogue_Warrior;

public class Game
{
    public Character[] Characters;
    private static Random _random = new Random();

    public Game()
    {
        SetDefault();
    }

    private void SetDefault()
    {
        Characters = new Character[17];

        // TODO: XD

        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i] = Character.GetRandomCharacter();
        }
        
        Characters[0].Set([0, 0], Character.Team.Red);
        Characters[1].Set([1, 0], Character.Team.Red);
        Characters[2].Set([0, 1], Character.Team.Red);
        Characters[3].Set([1, 1], Character.Team.Red);
        Characters[4].Set([1, 2], Character.Team.Red);
        Characters[5].Set([2, 1], Character.Team.Red);
        Characters[6].Set([2, 2], Character.Team.Red);
        Characters[7].Set([0, 2], Character.Team.Red);
        Characters[8].Set([2, 0], Character.Team.Red);
        
        Characters[9].Set([Config.MapSize[0] - 1, Config.MapSize[1] - 1], Character.Team.Blue);
        Characters[10].Set([Config.MapSize[0] - 1, Config.MapSize[1] - 2], Character.Team.Blue);
        Characters[11].Set([Config.MapSize[0] - 2, Config.MapSize[1] - 1], Character.Team.Blue);
        Characters[12].Set([Config.MapSize[0] - 2, Config.MapSize[1] - 3], Character.Team.Blue);
        Characters[13].Set([Config.MapSize[0] - 3, Config.MapSize[1] - 2], Character.Team.Blue);
        Characters[14].Set([Config.MapSize[0] - 2, Config.MapSize[1] - 2], Character.Team.Blue);
        Characters[15].Set([Config.MapSize[0] - 3, Config.MapSize[1] - 1], Character.Team.Blue);
        Characters[16].Set([Config.MapSize[0] - 1, Config.MapSize[1] - 3], Character.Team.Blue);
    }

    public void SetCustom()
    {
        double archerProbability = 0.8;
        
        Characters = new Character[40];

        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            Characters[i] = _random.NextDouble() < archerProbability ? new Archer() : new Mage();
            Characters[i].Set([i, 0], Character.Team.Red);
        }
        
        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            Characters[i + 10] = Character.GetRandomCharacter();
            Characters[i + 10].Set([i, 1], Character.Team.Red);
        }
        
        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            Characters[i + 20] = _random.NextDouble() < archerProbability ? new Archer() : new Mage();
            Characters[i + 20].Set([i, Config.MapSize[1] - 1], Character.Team.Blue);
        }
        
        for (int i = 0; i < Config.MapSize[0]; i++)
        {
            Characters[i + 30] = Character.GetRandomCharacter();
            Characters[i + 30].Set([i, Config.MapSize[1] - 2], Character.Team.Blue);
        }
    }

    public void SetCustom2()
    {
        int columnsAmount = 4;
        
        Characters = new Character[1 + columnsAmount * 10];

        Characters[0] = new God();
        Characters[0].Set([Config.MapSize[0]/2, Config.MapSize[1]-1], Character.Team.Blue);
        
        for (int j = 0; j < columnsAmount; j++)
        {
            for (int i = 0; i < Config.MapSize[0]; i++)
            {
                Characters[1+i+10*j] = Character.GetRandomCharacter();
                Characters[1+i+10*j].Set([i, j], Character.Team.Red);
            }
        }
    }
    
    public void MoveCharacters()
    {
        foreach (Character character in Characters)
        {
            character.OnTurn(Characters);
        }
    }
}