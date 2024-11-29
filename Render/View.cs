namespace Rogue_Warrior;

public abstract class View
{
    public string Id = "none";
    
    public abstract void OnStart();
    public abstract void OnIteration();
    public abstract void OnClose();
}