using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlock : MapBlock
{
    [SerializeField] private LayerMask playerLayer;     //플레이어 레이어

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            Managers.UI.CreateUI(UIType.ClearPopup);
        }
    }

    /// <summary>
    /// 충돌한 레이어가 Player레이어인지 체크해주는 함수
    /// </summary>
    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }
}
