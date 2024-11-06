using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlock : MapBlock
{
    [SerializeField] private LayerMask playerLayer;



    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            Managers.UI.CreateUI(UIType.ClearPopup);
        }
    }

    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }
}
