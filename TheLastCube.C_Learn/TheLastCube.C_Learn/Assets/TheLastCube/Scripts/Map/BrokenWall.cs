using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("스킬:"+ playerController.skillActive);
                Debug.Log("충돌이 감지 되었습니다");

                if (playerController.skillActive && playerController.yellowSkill)
                {
                    Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
                    Debug.Log("통과했습니다");
                }
            }
        }
    }

    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }
}

