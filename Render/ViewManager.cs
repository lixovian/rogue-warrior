using Rogue_Warrior.Render;
using Rogue_Warrior.Views;

namespace Rogue_Warrior;

public class ViewManager
{
    private static View[] _views = [new MenuView(), new GameView(), new ResultView()];
    
    private static int _currentView = 0;
    public void SetTimer()
    {
        while (true)
        {
            
        }
    }
}