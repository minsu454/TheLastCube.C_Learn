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
    [SerializeField] private int skill2MaxCount = 5;

    public int skill1Count = 1;

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
            skill1Count = skill1MaxCount;
            pastPosition = gameObject.transform.position;
            pastRotation = gameObject.transform.rotation;
            return;
        }
        else
        {
            skill1Count = -1;
            this.gameObject.transform.position = pastPosition;
            this.gameObject.transform.rotation = pastRotation;
            pastPosition = Vector3.zero;
            pastRotation = Quaternion.identity;

            cubeController.OnSkillEvent -= skill1;
            return;
        }
    }

    private void skill2(bool active)
    {
        cubeController.yellowSkill = active;

        if (active)
        {
            Debug.Log("Yellow Skill On");
            cubeController.yellowSkill = true;

        }
        else
        {
            Debug.Log("Skill2 Off");
            cubeController.yellowSkill = false;
            return;
        }
    }
}
