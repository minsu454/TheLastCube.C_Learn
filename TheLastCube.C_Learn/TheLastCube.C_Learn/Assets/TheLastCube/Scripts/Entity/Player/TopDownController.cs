using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent; //플레이어가 움직일 때 발생시킬 event들
    public event Action<Vector2> OnSpecialMoveEvent; //노란색 스킬이 켜졌을 때 발생시킬 event들(사실상 빠른 이동이고 이후에 기능들이 추가 될 수 있기에 나눔)
    public event Action<bool> OnSkillEvent; // 플레이어가 스킬(e) 를 눌렀을 때 발생시킬 event

    protected bool isPressing = false;

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }
    public void CallSpecialMoveEvent(Vector2 direction)
    {
        OnSpecialMoveEvent?.Invoke(direction);
    }
    public void CallSkillEvent(bool active)
    {
        OnSkillEvent?.Invoke(active);
    }
}
