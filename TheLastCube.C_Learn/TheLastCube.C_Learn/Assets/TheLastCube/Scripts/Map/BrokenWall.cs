using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private float PassingSpeed; 
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                float PlayerSpeed = rb.velocity.magnitude;
                Debug.Log("Rigidbody velocity: " + rb.velocity);
                Debug.Log("PlayerSpeed: " + PlayerSpeed);
                Debug.Log("충돌이 감지 되었습니다");

                if (PlayerSpeed > PassingSpeed)
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
