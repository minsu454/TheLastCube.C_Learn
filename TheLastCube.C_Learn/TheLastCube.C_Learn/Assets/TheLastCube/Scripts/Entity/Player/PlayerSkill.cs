using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerController cubeController;

    private Vector3 pastPosition;//redskill 사용 시 필요
    private Quaternion pastRotation;

    [SerializeField] private int skill1MaxCount = 6;

    private void Awake()
    {
        cubeController = GetComponent<PlayerController>();        
    }

    public bool CheckSkiilType()
    {
        int upIndex = cubeController.playerQuadController.CheckUP();//위의 quad를 체크해서 스킬 발동

        switch (cubeController.playerQuadController.quads[upIndex].playerQuadType)//구린 구조임 (자세히 보면 실력이 하강함) interface 형식으로 바꿔야 할 것 같음
        {//OnSkillEvent에 순간적으로 구독을 해주고 스킬 사용이 끝나면 구독을 취소 시킴 (하나의 스킬 키로 수행하고 싶어서)
            case BlockInteractionType.None: Debug.Log("None"); return false;
            case BlockInteractionType.KeyRed:
                cubeController.OnSkillEvent += skill1;
                return true;
            case BlockInteractionType.KeyBlue:
                return true;
            case BlockInteractionType.KeyYellow:
                cubeController.OnSkillEvent += skill2;
                return true;
            default: return false;
        }
    }

    private void skill1(bool active)
    {
        cubeController.redSkill = active;

        if (active)
        {
            Managers.Sound.PlaySFX(SfxType.Skill);//효과음
            cubeController.playerQuadController.UseEffect((int)BlockInteractionType.KeyRed - 10);//파티클 사용
            cubeController.redSkillCount = skill1MaxCount;
            pastPosition = gameObject.transform.position;
            pastRotation = gameObject.transform.rotation;
            return;
        }
        else
        {
            if (cubeController.playerMovement.CheckGround())//스킬 사용 후 땅에서 발동 시 위치 유지
            {
                cubeController.redSkillCount = -1;
                pastPosition = Vector3.zero;
                pastRotation = Quaternion.identity;

                cubeController.OnSkillEvent -= skill1;
                cubeController.playerQuadController.UseEffect((int)BlockInteractionType.KeyRed - 10);
                return;
            }

            cubeController.redSkillCount = -1;//초기화
            this.gameObject.transform.position = pastPosition;
            this.gameObject.transform.rotation = pastRotation;
            pastPosition = Vector3.zero;
            pastRotation = Quaternion.identity;

            cubeController.OnSkillEvent -= skill1;
            cubeController.playerQuadController.UseEffect((int)BlockInteractionType.KeyRed - 10);
            return;
        }
    }

    private void skill2(bool active)
    {
        cubeController.yellowSkill = active;

        Managers.Sound.PlaySFX(SfxType.Skill);
        cubeController.playerQuadController.UseEffect((int)BlockInteractionType.KeyYellow-10);

        if (active)
        {
            cubeController.yellowSkill = true;

        }
        else
        {
            cubeController.yellowSkill = false;
            return;
        }
    }
}
