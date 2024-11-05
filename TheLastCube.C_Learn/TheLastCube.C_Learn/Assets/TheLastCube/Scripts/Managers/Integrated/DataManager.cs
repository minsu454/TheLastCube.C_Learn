using Common.Event;
using UnityEngine;

public sealed class DataManager : IManager
{
    private string fileName = "3Stage";
    //public string[] 
    public string FileName { get { return fileName; } }

    public  string path = $"{Application.streamingAssetsPath}/MapData";

    public void Init()
    {

        EventManager.Subscribe(GameEventType.StageChoice, StageChoiceCompleted);
    }

    public void StageChoiceCompleted(object args)
    {
        fileName = (string)args;
    }
}