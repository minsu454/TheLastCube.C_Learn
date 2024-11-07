using Common.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : TopDownController
{
    [SerializeField] private int skillCount;
    public PlayerMovement playerMovement;
    public PlayerQuadController playerQuadController;
    public PlayerSkill playerSkill;
    public GameObject Effect;
    private Vector2 direction = Vector2.zero;

    public bool skillActive = false;
    public bool redSkill = false;
    public bool yellowSkill = false;

    public int redSkillMaxCount;
    public int redSkillCount;

    private void FixedUpdate()
    {
        if (!isPressing)
        {
            return;            
        }
        if (yellowSkill)
        {
            CallSpecialMoveEvent(direction);
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
        if (playerMovement.Moving()) return;
        if (value.isPressed)
        {
            if (skillActive)
            {
                skillActive = false;
                CallSkillEvent(skillActive);
            }
            else
            {
                if (!playerSkill.CheckSkiilType()) return;
                skillActive = true;

                CallSkillEvent(skillActive);
            }            
        }
    }

    public void OnClick(InputValue value)
    {
        EventManager.Dispatch(GameEventType.ChangeViewPoint, null);
    }
}
