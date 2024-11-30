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
        Console.ReadKey();
    }

    public override void OnIteration()
    {
        _game.MoveCharacters();
        Renderer.Rerender(_game.Map);

        ConsoleKey key = Console.ReadKey().Key;

        if (key == ConsoleKey.Escape)
        {
            ViewManager.ChangeView("title");
        }
    }

    public override void OnClose()
    {
    }
}