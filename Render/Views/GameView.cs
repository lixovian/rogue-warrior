using Rogue_Warrior.MapObjects;
using Rogue_Warrior.Service.FileHandling;

namespace Rogue_Warrior.Render.Views;

public class GameView : View
{
    private Game _game = new Game();

    public GameView()
    {
        Id = "game";
    }

    public override void OnStart()
    {
        if (FileProcessor.ParseFile(Config.CurrentFile, out List<MapObject> map, out Vector2 size))
        {
            Console.Out.WriteLine("Map loaded, press any button to continue...");
        }

        _game.Map.SetMap(map, size);

        Console.ReadKey();

        Renderer.Rerender(_game.Map);
    }

    public override void OnIteration()
    {
        ConsoleKey key = Console.ReadKey().Key;

        if (key == ConsoleKey.Escape)
        {
            ViewManager.ChangeView("title");
        }
        
        _game.MoveCharacters();
        Renderer.Rerender(_game.Map);
        
        if (CheckWinner(out Character.Team winner))
        {
            Console.Out.WriteLine("Game over!");

            Console.Out.Write("Winner: ");
            Console.ForegroundColor = Renderer.GetTeamColor(winner);
            Console.Out.Write($"{Renderer.GetTeamName(winner)}");
            Console.ResetColor();

            Console.ReadKey();

            ViewManager.ChangeView("menu");
        }
    }

    public bool CheckWinner(out Character.Team winner)
    {
        winner = Character.Team.Neutral;

        Character[] characters = _game.Map.GetSortedCharacters();

        if (characters.Length == 0)
        {
            return true;
        }

        characters = Array.FindAll(characters, character => character.IsActive());

        // Console.Out.WriteLine(string.Join(", ", characters.Select(ch => ch.CharacterTeam)));
        if (characters[0].CharacterTeam == characters[^1].CharacterTeam)
        {
            winner = characters[0].CharacterTeam;
            return true;
        }

        return false;
    }

    public override void OnClose()
    {
    }
}