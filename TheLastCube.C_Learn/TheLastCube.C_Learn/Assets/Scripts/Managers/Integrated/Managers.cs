using Unity.VisualScripting;
using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    private static Managers instance;

    public static Managers Instance
        //외부에서 접근 가능하게 하는 인스턴스
    {
        get
        {
            //외부에서 매니저의 인스턴스 호출 시 null일 경우 에러코드 출력하는 방어코드
            if (instance == null)
            {
                Debug.LogError("Managers instance is null.");
            }
            return instance;
        }
    }

    public static UIManager UI;
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
            
            // CreateManager 오브젝트 생성후 CreateManager 스크립트 추가
            GameObject uiGameobject = new GameObject("UIManager");
            UI = uiGameobject.AddComponent<UIManager>();
            uiGameobject.transform.parent = gameObject.transform; // Managers의 자식으로 설정
        }

        else
        {     
            //만약 이미 인스턴스가 있다면 생성되려는 인스턴스의 게임오브젝트 파괴
            Destroy(instance.gameObject);
        }
    }


    // Hierarchy창에 Manager만들어주는 함수
    private static T CreateManager<T>(GameObject parent) where T : Component, IManager
    {
        GameObject gameObject = new GameObject(typeof(T).Name);
        T generic = gameObject.AddComponent<T>();
        gameObject.transform.parent = parent.transform;

        generic.Init();

        return generic;
    }
}
