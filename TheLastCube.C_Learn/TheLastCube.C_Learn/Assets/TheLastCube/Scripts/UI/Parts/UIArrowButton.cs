using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIArrowButton : MonoBehaviour
{
    [SerializeField] private ArrowDirType arrowDirType;     //화살 방향타입
    [SerializeField] private Transform arrow;               //화살
    [SerializeField] private int distance;                  //이동 거리

    private Vector3 movePos;                    //움직이는 방향
    private bool isOnUI = true;                 //내려가있나 올라와있나 체크 변수

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        switch (arrowDirType)
        {
            case ArrowDirType.Up:
                movePos = Vector3.up;
                break;
            case ArrowDirType.Down:
                movePos = Vector3.down;
                break;
            case ArrowDirType.Left:
                movePos = Vector3.left;
                break;
            case ArrowDirType.Right:
                movePos = Vector3.right;
                break;
        }

        movePos *= distance;
    }

    /// <summary>
    /// 해당 창 움직이는 함수
    /// </summary>
    public void OnArrowMove()
    {
        MoveArrow();

        isOnUI = !isOnUI;
    }

    /// <summary>
    /// 화살버튼 rotation과 obj를 통째로 내려주는 함수
    /// </summary>
    private void MoveArrow()
    {
        if (isOnUI)
        {
            transform.position += movePos;
        }
        else
        {
            transform.position -= movePos;
        }

        arrow.Rotate(0, 0, 180);
    }
}

public enum ArrowDirType
{
    Up,
    Down,
    Left,
    Right,
}