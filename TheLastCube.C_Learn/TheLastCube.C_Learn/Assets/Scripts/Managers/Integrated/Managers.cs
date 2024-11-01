using Unity.VisualScripting;
using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    private static Managers instance;

    public static UIManager UI
    { get {return instance.uiManager;} }

    private UIManager uiManager;
            

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //위 생성자보다 먼저 실행하게 됨 생명주기에서 awake보다 우선 실행
    private static void Init()
    {
        Screen.SetResolution(1920, 1080, false);

        if (instance == null)
            //처음 게임 scene이 로드되기 전 instance가 null이기 때문에 해당 코드를 실행
        {
            GameObject gameObject = new GameObject("Managers");
            instance = gameObject.AddComponent<Managers>();
            //Managers라는 빈 게임옵젝 만들고 Managers 컴포넌트를 붙임.
            DontDestroyOnLoad(gameObject);
            
            
            //GameObject uiGameobject = new GameObject("UIManager");
            //instance.uiManager = uiGameobject.AddComponent<UIManager>();
            //uiGameobject.transform.parent = gameObject.transform; // Managers의 자식으로 설정

            //위의 코드는 아래 코드와 같음

            instance.uiManager = CreateManager<UIManager>(gameObject.transform);

        }

        else
        {
            throw new System.Exception("Managers Error");
        }
    }


    // Hierarchy창에 Manager만들어주는 함수
    private static T CreateManager<T>(Transform parent) where T : Component, IManager
    {
        GameObject gameObject = new GameObject(typeof(T).Name);
        T generic = gameObject.AddComponent<T>();
        gameObject.transform.parent = parent;

        generic.Init();

        return generic;
    }
}
