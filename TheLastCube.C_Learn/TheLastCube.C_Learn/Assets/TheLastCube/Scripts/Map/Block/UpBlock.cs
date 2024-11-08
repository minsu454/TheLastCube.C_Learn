using Common.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpBlock : MapBlock
{
    [Header("UpBlock Value Setting")]
    [SerializeField] private int Speed;                 //블록 속도
    [SerializeField] private LayerMask playerLayer;     //플레이어 감지 레이어

    private Vector3 targetPosition;                     //타겟 포지션    
    private Vector3 originalPosition;                   //기존 포지션
    private bool Move = false;                          //움직이는 중인지 체크 변수
    private Action dispatchEvent;                       

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y + data.upCount, transform.position.z);
            Move = true;
            dispatchEvent += OnDispatch;
            other.transform.SetParent(transform); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            other.transform.SetParent(null);
            targetPosition = originalPosition;
            Move = true;
            dispatchEvent -= OnDispatch;
        }
    }

    /// <summary>
    /// 충돌한 레이어가 Player레이어인지 체크해주는 함수
    /// </summary>
    private bool IsPlayer(Collider other)
    {
        return playerLayer == (playerLayer | (1 << other.gameObject.layer));
    }

    /// <summary>
    /// 플레이어 움직임을 잠궈주는 함수
    /// </summary>
    private void OnDispatch()
    {
        EventManager.Dispatch(GameEventType.LockPlayerMove, Move);
    }

    private void Update()
    {
        if (Move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                Move = false;
            }
            dispatchEvent?.Invoke();
        }
    }
}
