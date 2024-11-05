using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private float PassingSpeed; 
    [SerializeField] private LayerMask playerLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayer(collision.collider))
        {
            float PlayerSpeed = collision.relativeVelocity.magnitude;
            Debug.Log("충돌이 감지 되었습니다");

            if (PlayerSpeed > PassingSpeed)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>()); 
                Debug.Log("통과했습니다");
            }
        }
    }

    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }

}
