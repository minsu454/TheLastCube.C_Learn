using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    private static Managers instance;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Screen.SetResolution(1920, 1080, false);

        GameObject go = new GameObject("Managers");
        instance = go.AddComponent<Managers>();

        DontDestroyOnLoad(go);

    }

    /// <summary>
    /// Hierarchy창에 Manager만들어주는 함수
    /// </summary>
    private static T CreateManager<T>(Transform parent) where T : Component, IManager
    {
        GameObject go = new GameObject(typeof(T).Name);
        T t = go.AddComponent<T>();
        go.transform.parent = parent;

        t.Init();

        return t;
    }
}
