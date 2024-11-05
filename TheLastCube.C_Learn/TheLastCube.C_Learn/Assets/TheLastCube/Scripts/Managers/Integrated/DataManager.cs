using Common.Event;

public sealed class DataManager : IManager
{
    private string fileName = "3Stage";
    public string FileName { get { return fileName; } }

    public void Init()
    {
        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
    }

    public void StageChoiceCompleted(object args)
    {
        fileName = (string)args;
    }
}