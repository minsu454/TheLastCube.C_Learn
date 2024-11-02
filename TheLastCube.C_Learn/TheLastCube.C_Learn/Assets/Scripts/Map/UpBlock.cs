using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpBlock : MonoBehaviour
{
    [Header("UpBlock Value Setting")]
    [SerializeField] private float UpValue;
    [SerializeField] private int Speed;
    [SerializeField] private LayerMask playerLayer;

    private Vector3 targetPosition;
    private bool Move = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("올라옴");
        if (other.CompareTag("Player"))
        {
            targetPosition = new Vector3(transform.position.x, UpValue, transform.position.z);
            Move = true;
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    //private bool IsPlayer(Col other)
    //{
    //    return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    //}

    private void Update()
    {
        if (Move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                Move = false;
            }
        }
    }
}
