using Common.Event;

public sealed class DataManager : IManager
{
    private string fileName;
    public string FileName { get { return fileName; } }

    public void Init()
    {
        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
    }

    public void StageChoiceCompleted(object args)
    {
        fileName = (string)args;
    }

    public void Delete()
    {
        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
    }
}