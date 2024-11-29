namespace Rogue_Warrior.Render.Views;

public class TitleView : View
{
    public void onStart()
    {
        Console.Out.WriteLine("Welcome to");
        
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Out.WriteLine("██████╗  ██████╗  ██████╗ ██╗   ██╗███████╗    ██╗    ██╗ █████╗ ██████╗ ██████╗ ██╗ ██████╗ ██████╗");
        Console.Out.WriteLine("██╔══██╗██╔═══██╗██╔════╝ ██║   ██║██╔════╝    ██║    ██║██╔══██╗██╔══██╗██╔══██╗██║██╔═══██╗██╔══██╗");
        Console.Out.WriteLine("██████╔╝██║   ██║██║  ███╗██║   ██║█████╗█████╗██║ █╗ ██║███████║██████╔╝██████╔╝██║██║   ██║██████╔╝");
        Console.Out.WriteLine("██╔══██╗██║   ██║██║   ██║██║   ██║██╔══╝╚════╝██║███╗██║██╔══██║██╔══██╗██╔══██╗██║██║   ██║██╔══██╗");
        Console.Out.WriteLine("██║  ██║╚██████╔╝╚██████╔╝╚██████╔╝███████╗    ╚███╔███╔╝██║  ██║██║  ██║██║  ██║██║╚██████╔╝██║  ██║");
        Console.Out.WriteLine("╚═╝  ╚═╝ ╚═════╝  ╚═════╝  ╚═════╝ ╚══════╝     ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝ ╚═════╝ ╚═╝  ╚═╝");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Out.WriteLine("Version: 0.1.4 (pre-alpha)");
        Console.Out.WriteLine("Press any button to continue...");
    }

    public void onIteration()
    {
        Console.ReadKey();
        
        
    }

    public void onClose()
    {
    }
}