using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private readonly Dictionary<UIType, GameObject> UIContainer = new Dictionary<UIType, GameObject>();
    //키값 : UIType Enum / Value값 : GameObject(생성할 옵젝)
    //UIContainer = Dictionary 사전 클래스의 변수에 키값과 벨류값을 넣어줌.
    //readonly(키워드) : 필드 또는 생성자에서 처음 값을 할당하고 나면 더이상 값을 못바꾸게 하는 키워드
    private readonly Stack<GameObject> _stack = new Stack<GameObject>();

    public void Init() //생성 초기값
    {
        foreach(UIType type in Enum.GetValues(typeof(UIType)))
        //foreach 반복문 Enum UIType의 type 변수가 
        //UIType이라는 열거형(enum)에 정의된 모든 값을 가져옴.
        //UIType라는 enum의 모든 값을 UIType 열거형의 type라는 변수에 저장함.
        {
            GameObject gameObject = Resources.Load<GameObject>(/*{type 앞에 불러올 프리팹의 Resources 주소를 작성. ex) Prefabs/cube*/$"{type.ToString()}");
            //gameObject(유니티에서 GameObject) 변수 = Resources 폴더에서 GameObject를 로드할 것인데 어떤 GameObject를 로드할 것인가?
            //string.Format >> 




            UIContainer.Add(type, gameObject);
        }
    }


    public void CreateUI(UIType type)
    {
        if(UIContainer.TryGetValue(type, out GameObject gameObject))
        {
            GameObject createdGameObject = Instantiate(gameObject);
        }
    }


    public void CloseUI()
    {

    }
}
