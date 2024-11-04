using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : TopDownController
{
    [SerializeField] private int skillCount;
    private Vector2 direction = Vector2.zero;
    public bool skillActive = false;


    private void FixedUpdate()
    {
        if (!isPressing)
        {
            return;            
        }

        CallMoveEvent(direction);
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        if (direction == Vector2.zero)
        {
            isPressing = false;
            return;
        }
        if (direction.x * direction.y != 0f)
        {
            isPressing = false;
            return;
        }
        isPressing = true;
    }

    public void OnSkill(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Skill Active");
            skillActive = !skillActive;
            CallSkillEvent(skillActive);
        }
    }
}
