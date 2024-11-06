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
                if (playerController.skillActive && playerController.yellowSkill)
                {
                    Managers.Sound.PlaySFX(SfxType.BreakBlock);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }
}
