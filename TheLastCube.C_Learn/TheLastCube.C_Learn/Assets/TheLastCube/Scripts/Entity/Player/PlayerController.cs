using Common.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : TopDownController
{
    [SerializeField] private int skillCount; //redskillcount
    public PlayerMovement playerMovement; // *아래 3가지 모두 프리팹으로 만들어질 플레이어의 관계를 위해서 
    public PlayerQuadController playerQuadController; // *PlayerController가 버스의 느낌을 해주고 있는 것 
    public PlayerSkill playerSkill; // *다른 스크립트에서 또 다른 스크립트의 정보를 얻기 위해서는 PlayerController의 무언가로 접근하면 된다. 
    public GameObject Effect; //파티클을 다루가 위해서 필요한 정보
    private Vector2 direction = Vector2.zero; //기본값 int = 0 같은 느씸

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
        if (yellowSkill)//yellowSkill이 켜져있으면 일반 이동보다 우선적으로 실행하도록
        {
            CallSpecialMoveEvent(direction);
            return;
        }

        CallMoveEvent(direction);
    }

    public void OnMove(InputValue value)//input system을 이용하여 vector value를 받음
    {
        direction = value.Get<Vector2>();
        if (direction == Vector2.zero)//입력을 안하고 있을때 불필요한 수행 방지
        {
            isPressing = false;
            return;
        }
        if (direction.x * direction.y != 0f)//대각선 이동 방지
        {
            isPressing = false;
            return;
        }
        isPressing = true;
    }

    public void OnSkill(InputValue value)
    {
        if (playerMovement.Moving()) return;//이동 중에 스킬 발동 불가
        if (value.isPressed)
        {
            if (skillActive)// 스킬 다시 발동 시
            {
                skillActive = false;
                CallSkillEvent(skillActive);
            }
            else//처음 능력을 발동 시
            {
                if (!playerSkill.CheckSkiilType()) return; // 구현이 안된 능력이거나 없을 경우
                skillActive = true;

                CallSkillEvent(skillActive);
            }            
        }
    }

    public void OnClick(InputValue value)//마우스 좌클릭시 값을 EventManager에 전달해줌 (역 구독?)
    {
        EventManager.Dispatch(GameEventType.ChangeViewPoint, null);
    }
}
