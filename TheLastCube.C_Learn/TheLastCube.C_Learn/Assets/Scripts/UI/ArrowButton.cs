using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    [SerializeField] private Transform arrow;   //화살 위치
    [SerializeField] private int downPosY;      //내려갈 때 얼마나 내려갈지 정해주는 변수

    private bool isOnUI = true;                 //내려가있나 올라와있나 체크 변수

    /// <summary>
    /// 화살버튼 rotation과 obj를 통째로 내려주는 함수
    /// </summary>
    public void MoveArrowButton()
    {
        if (isOnUI)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - downPosY);
            arrow.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + downPosY);
            arrow.rotation = Quaternion.Euler(0, 0, 0);
        }

        isOnUI = !isOnUI;
    }

}
