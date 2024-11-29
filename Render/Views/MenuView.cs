using Rogue_Warrior.Gui;
using Rogue_Warrior.Service.FileHandling;

namespace Rogue_Warrior.Render.Views;

public class MenuView : View
{
    private const string DefaultTitle = "Choose level:";
    private string[] _files;

    private PathProcessor PathProcessor = new PathProcessor();
    public ListChooser FileChooser;
    public MenuView()
    {
        Id = "menu";
    }

    public override void OnStart()
    {
        _files = PathProcessor.GetLevels();
        
        string[] toDisplay = _files.Select(x => x.Substring(x.LastIndexOf('\\') + 1).Split(".")[0]).ToArray();

        FileChooser = new ListChooser(DefaultTitle, toDisplay);
    }

    public override void OnIteration()
    {
        FileChooser.Update();

        if (FileChooser.IsChosen)
        {
            Config.CurrentFile = _files[FileChooser.GetChosen()];
            ViewManager.ChangeView("game");
        }
    }

    public override void OnClose()
    {
        
    }
}