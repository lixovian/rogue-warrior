using ConsoleKey = System.ConsoleKey;

namespace Rogue_Warrior.Gui;

public class ListChooser
{
    private const ConsoleColor DefaultSelectedBackgroundColor = ConsoleColor.DarkYellow;
    private const ConsoleColor DefaultSelectedForegroundColor = ConsoleColor.Black;
    
    private string Title;
    private string[] TaskList;

    private int _chosenTask;
    public bool IsChosen;

    public ListChooser(string title, string[] taskList)
    {
        Title = title;
        TaskList = taskList;

        _chosenTask = 0;
    }

    public int GetChosen()
    {
        return _chosenTask;
    } 

    public void Update(bool doClear = true)
    {
        if (doClear) Console.Clear();

        Console.Out.WriteLine(Title);

        for (int i = 0; i < TaskList.Length; i++)
        {
            if (_chosenTask == i)
            {
                Console.ForegroundColor = DefaultSelectedForegroundColor;
                Console.BackgroundColor = DefaultSelectedBackgroundColor;
            }
            Console.Out.WriteLine($"{i+1}. {TaskList[i]}");
            
            Console.ResetColor();
        }
        
        ConsoleKey key = Console.ReadKey().Key;
        Move(key);
    }

    private void Move(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow or ConsoleKey.W:
                _chosenTask = (_chosenTask - 1 + TaskList.Length) % TaskList.Length; 
                break;
            case ConsoleKey.DownArrow or ConsoleKey.S:
                _chosenTask = (_chosenTask + 1) % TaskList.Length;
                break;
            case ConsoleKey.Enter or ConsoleKey.Spacebar:
                IsChosen = true;
                break;
            default:
                return;
        }
    }
}