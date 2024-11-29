using Rogue_Warrior;
using Rogue_Warrior.MapObjects;
using Rogue_Warrior.Service.FileHandling;

internal class Program
{
    public static void Main(string[] args)
    {
        PathProcessor pathProcessor = new PathProcessor();
        Game game = new Game();
        game.SetCustom();
        // game.SetCustom2();

        FileProcessor.ParseFile(pathProcessor.GetPath(@"Files\Levels\map-2.rog"), out MapObject?[,] map); 
        game.Map.SetMap(map);
        
        Console.ReadKey();
        
        Renderer.Rerender(game.Map);
        Console.ReadKey();
        Console.ReadKey();
        
        while (true)
        {
            game.MoveCharacters();

            Renderer.Rerender(game.Map);
            
            Console.ReadKey();
            // Renderer.Wait();
        }
    }
}