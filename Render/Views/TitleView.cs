namespace Rogue_Warrior.Render.Views;

public class TitleView : View
{
    public TitleView()
    {
        Id = "title";
    }

    public override void OnStart()
    {
        Console.Out.WriteLine("Welcome to");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Out.WriteLine("██████╗  ██████╗  ██████╗ ██╗   ██╗███████╗    ██╗    ██╗ █████╗ ██████╗ ██████╗ ██╗ ██████╗ ██████╗");
        Console.Out.WriteLine("██╔══██╗██╔═══██╗██╔════╝ ██║   ██║██╔════╝    ██║    ██║██╔══██╗██╔══██╗██╔══██╗██║██╔═══██╗██╔══██╗");
        Console.Out.WriteLine("██████╔╝██║   ██║██║  ███╗██║   ██║█████╗█████╗██║ █╗ ██║███████║██████╔╝██████╔╝██║██║   ██║██████╔╝");
        Console.Out.WriteLine("██╔══██╗██║   ██║██║   ██║██║   ██║██╔══╝╚════╝██║███╗██║██╔══██║██╔══██╗██╔══██╗██║██║   ██║██╔══██╗");
        Console.Out.WriteLine("██║  ██║╚██████╔╝╚██████╔╝╚██████╔╝███████╗    ╚███╔███╔╝██║  ██║██║  ██║██║  ██║██║╚██████╔╝██║  ██║");
        Console.Out.WriteLine("╚═╝  ╚═╝ ╚═════╝  ╚═════╝  ╚═════╝ ╚══════╝     ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝ ╚═════╝ ╚═╝  ╚═╝");
        Console.ResetColor();
        Console.Out.WriteLine("Version: 0.3.1 (pre-alpha)");
        Console.Out.WriteLine("Press any button to continue...");
    }

    public override void OnIteration()
    {
        Console.ReadKey();
        
        ViewManager.ChangeView("menu");
    }

    public override void OnClose()
    {
    }
}