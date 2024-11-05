using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerController cubeController;

    private Vector3 pastPosition;

    private void Awake()
    {
        cubeController = GetComponent<PlayerController>();        
    }

    // Start is called before the first frame update
    void Start()
    {
        cubeController.OnSkillEvent += skill1;
    }

    private void skill1(bool active)
    {
        if (active)
        {
            Debug.Log("Position Save");
            pastPosition = gameObject.transform.position;
            return;
        }
        else
        {
            Debug.Log("Load Past Position");
            this.gameObject.transform.position = pastPosition;
            pastPosition = Vector3.zero;
            return;
        }
    }

    private void skill2(bool active)
    {
        if (active)
        {
            int upIndex = cubeController.playerQuadController.CheckUP();
            if (cubeController.playerQuadController.quads[upIndex].playerQuadType == BlockInteractionType.KeyYellow)
            {
                Debug.Log("Yellow Skill On");
                cubeController.yellowCheck = true;
            }
        }
        else
        {
            Debug.Log("Skill2 Off");
            cubeController.yellowCheck = false;
            return;
        }
    }
}
