using Common.Event;

public sealed class DataManager : IManager
{
    private string fileName;    //맵이름
    public string FileName { get { return fileName; } }

    public void Init()
    {
        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
    }

    /// <summary>
    /// 스테이지 고를 때 맵이름 저장해주는 함수
    /// </summary>
    public void StageChoiceCompleted(object args)
    {
        fileName = (string)args;
    }

    public void Delete()
    {
        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
    }
}