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

    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }
}
