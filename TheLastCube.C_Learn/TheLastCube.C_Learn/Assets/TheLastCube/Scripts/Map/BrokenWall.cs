using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private float CrashSpeed; 
    [SerializeField] private LayerMask playerLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayer(collision.collider))
        {
            float collisionSpeed = collision.relativeVelocity.magnitude;
            Debug.Log("충돌이 감지 되었습니다");

        }
    }

    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }

}
