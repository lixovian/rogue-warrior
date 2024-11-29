using Rogue_Warrior.Render.Views;

namespace Rogue_Warrior.Render;

public static class ViewManager
{
    private static View[] _views = [new TitleView(), new MenuView(), new GameView(), new ResultView()];
    
    private static int _currentView = 0;
    private static bool IsInterrupted = false;
    public static void SetView()
    {
        View currentView = _views[_currentView];
        
        Console.Clear();
        
        currentView.OnStart();

        IsInterrupted = false;
        while (!IsInterrupted)
        {
            currentView.OnIteration();
        }
        
        currentView.OnClose();
        
        SetView();
    }

    public static View? GetView(string id)
    {
        for (var i = 0; i < _views.Length; i++)
        {
            var view = _views[i];
            if (view.Id != id) continue;
            
            return view;
        }

        return null;
    }

    public static void ChangeView(int n)
    {
        _currentView = n;
        IsInterrupted = true;
    }

    public static void ChangeView(string id)
    {
        for (var i = 0; i < _views.Length; i++)
        {
            var view = _views[i];
            if (view.Id != id) continue;
            
            ChangeView(i);
            return;
        }
    }
}