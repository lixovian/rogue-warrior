using Rogue_Warrior;
using Rogue_Warrior.MapObjects;
using Rogue_Warrior.Render;
using Rogue_Warrior.Service.FileHandling;

internal class Program
{
    public static void Main(string[] args)
    {
        // ViewManager.ChangeView("title");
        ViewManager.ChangeView("game");
        ViewManager.SetView();
    }
}