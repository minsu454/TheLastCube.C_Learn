using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MapBlock
{
    [SerializeField] private LayerMask playerLayer;
    
    void Start()
    {
        GroundRenderer.material = Managers.Material.Return(data.MoveType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("스킬:" + playerController.skillActive);
                Debug.Log("충돌이 감지 되었습니다");

                if (playerController.skillActive && playerController.yellowSkill)
                {
                    gameObject.SetActive(false);
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
