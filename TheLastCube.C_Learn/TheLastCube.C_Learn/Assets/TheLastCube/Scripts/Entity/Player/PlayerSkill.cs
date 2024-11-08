using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerController cubeController;

    private Vector3 pastPosition;
    private Quaternion pastRotation;

    [SerializeField] private int skill1MaxCount = 6;

    private void Awake()
    {
        cubeController = GetComponent<PlayerController>();        
    }

    public bool CheckSkiilType()
    {
        int upIndex = cubeController.playerQuadController.CheckUP();

        switch (cubeController.playerQuadController.quads[upIndex].playerQuadType)
        {
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
            Managers.Sound.PlaySFX(SfxType.End);
            cubeController.playerQuadController.UseEffect((int)BlockInteractionType.KeyRed - 10);
            cubeController.redSkillCount = skill1MaxCount;
            pastPosition = gameObject.transform.position;
            pastRotation = gameObject.transform.rotation;
            return;
        }
        else
        {
            if (cubeController.playerMovement.CheckGround())
            {
                cubeController.redSkillCount = -1;
                pastPosition = Vector3.zero;
                pastRotation = Quaternion.identity;

                cubeController.OnSkillEvent -= skill1;
                cubeController.playerQuadController.UseEffect((int)BlockInteractionType.KeyRed - 10);
                return;
            }

            cubeController.redSkillCount = -1;
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

        Managers.Sound.PlaySFX(SfxType.End);
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
