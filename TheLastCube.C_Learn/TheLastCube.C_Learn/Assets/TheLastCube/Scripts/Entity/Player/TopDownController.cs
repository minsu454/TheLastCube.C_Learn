using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<bool> OnSkillEvent;

    protected bool isMoving = false;
    protected bool isPressing = false;

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallSkillEvent(bool active)
    {
        OnSkillEvent?.Invoke(active);
    }
}