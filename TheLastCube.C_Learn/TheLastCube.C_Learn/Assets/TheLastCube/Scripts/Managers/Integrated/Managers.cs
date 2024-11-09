using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    private static Managers instance;

    public static DataManager Data { get { return instance.dataManager; } }
    public static UIManager UI { get { return instance.uiManager; } }
    public static MaterialContainer Material { get { return instance.materialContainer; } }
    public static SoundManager Sound { get { return instance.soundManager; } }

    private DataManager dataManager = new DataManager();
    private UIManager uiManager;
    private MaterialContainer materialContainer;
    private SoundManager soundManager;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //위 생성자보다 먼저 실행하게 됨 생명주기에서 awake보다 우선 실행
    private static void Init()
    {
        Screen.SetResolution(1920, 1080, false);


        //처음 게임 scene이 로드되기 전 instance가 null이기 때문에 해당 코드를 실행
        GameObject gameObject = new GameObject("Managers");
        instance = gameObject.AddComponent<Managers>();
        //Managers라는 빈 게임옵젝 만들고 Managers 컴포넌트를 붙임.
        DontDestroyOnLoad(gameObject);

        instance.dataManager.Init();

        instance.uiManager = CreateManager<UIManager>(gameObject.transform);
        instance.materialContainer = CreateManager<MaterialContainer>(gameObject.transform);
        instance.soundManager = CreateManager<SoundManager>(gameObject.transform);
    }


    /// <summary>
    /// Hierarchy창에 Manager만들어주는 함수
    /// </summary>
    private static T CreateManager<T>(Transform parent) where T : Component, IManager
    {
        GameObject gameObject = new GameObject(typeof(T).Name);
        T generic = gameObject.AddComponent<T>();
        gameObject.transform.parent = parent;

        generic.Init();

        return generic;
    }
}
