using Rogue_Warrior;

internal class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.SetCustom();
        // game.SetCustom2();
        
        Renderer.Rerender(game.Characters);
        Console.ReadKey();
        Console.ReadKey();
        
        while (true)
        {
            game.MoveCharacters();

            Renderer.Rerender(game.Characters);
            
            Console.ReadKey();
            // Renderer.Wait();
        }
    }
}