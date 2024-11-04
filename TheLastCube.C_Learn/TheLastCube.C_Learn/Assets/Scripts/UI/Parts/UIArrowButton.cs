using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIArrowButton : MonoBehaviour
{
    [SerializeField] private ArrowDirType arrowDirType;
    [SerializeField] private Transform arrow;   //화살 위치
    [SerializeField] private int distance;      

    private Vector3 movePos;
    private bool isOnUI = true;                 //내려가있나 올라와있나 체크 변수

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