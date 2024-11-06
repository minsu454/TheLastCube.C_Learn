using Common.Event;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpBlock : MapBlock
{
    [Header("UpBlock Value Setting")]
    [SerializeField] private int Speed;
    [SerializeField] private LayerMask playerLayer;

    private Vector3 targetPosition;
    private Vector3 originalPosition;
    private bool Move = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("올라옴");
        if (IsPlayer(other))
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y + data.upCount, transform.position.z);
            Move = true;
            other.transform.SetParent(transform); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            other.transform.SetParent(null);
            targetPosition = originalPosition;
            Move = false;
        }
    }

    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }

    private void Update()
    {
        if (Move == true)
        {
            EventManager.Dispatch(GameEventType.LockPlayerMove, Move);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                Move = false;
            }
            EventManager.Dispatch(GameEventType.LockPlayerMove, Move);
        }
    }
}
