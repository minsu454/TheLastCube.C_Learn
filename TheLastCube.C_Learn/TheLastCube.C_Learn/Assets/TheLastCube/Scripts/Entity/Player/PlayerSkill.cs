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

    // Start is called before the first frame update
    void Start()
    {
        cubeController.OnSkillEvent += skill2;
    }

    public void CheckSkiilType()
    {

    }

    private void skill1(bool active)
    {
        if (active)
        {
            Debug.Log("Position Save");
            skill1Count = skill1MaxCount;
            pastPosition = gameObject.transform.position;
            pastRotation = gameObject.transform.rotation;
            return;
        }
        else
        {
            Debug.Log("Load Past Position");
            skill1Count = -1;
            this.gameObject.transform.position = pastPosition;
            this.gameObject.transform.rotation = pastRotation;
            pastPosition = Vector3.zero;
            pastRotation = Quaternion.identity;
            return;
        }
    }

    private void skill2(bool active)
    {
        if (active)
        {
            int upIndex = cubeController.playerQuadController.CheckUP();
            if (cubeController.playerQuadController.quads[upIndex].playerQuadType == BlockInteractionType.KeyRed)
            {
                Debug.Log("Yellow Skill On");
                cubeController.yellowCheck = true;
            }
            else
            {
                Debug.Log("Not right Key");
                cubeController.skillActive = false;
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
