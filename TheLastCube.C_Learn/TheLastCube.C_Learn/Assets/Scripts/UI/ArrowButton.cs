using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    [SerializeField] private ArrowDirType arrowDirType;
    [SerializeField] private Transform arrow;   //화살 위치
    [SerializeField] private int downPos;      //내려갈 때 얼마나 내려갈지 정해주는 변수

    private bool isOnUI = true;                 //내려가있나 올라와있나 체크 변수

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
        int x = 0;
        int y = 0;

        switch (arrowDirType)
        {
            case ArrowDirType.Up:
            case ArrowDirType.Down:
                y = downPos;
                break;
            case ArrowDirType.Left:
            case ArrowDirType.Right:
                x = downPos;
                break;
        }

        if (isOnUI)
        {
            transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - x, transform.position.y - y);
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