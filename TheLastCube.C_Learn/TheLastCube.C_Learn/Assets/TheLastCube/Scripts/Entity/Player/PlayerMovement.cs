using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveAfterPosition;

    private PlayerController cubeController;
    public LayerMask groundlayerMask;
    private bool isMoving = false;

    [SerializeField]private float rotateSpeed;
    private float rotateRate;
    private int rollBackInt=10;

    [SerializeField] private int skillCount;

    private void Awake()
    {
        cubeController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        rotateRate = 90 / rotateSpeed;
        moveAfterPosition = transform.position;//보류

        cubeController.OnMoveEvent += Move;
    }


    private void Move(Vector2 direction)
    {
        if (isMoving)
        {
            //Debug.Log("Err Moving!");
            return;
        }
        //Debug.Log(direction);
        if (CheckWall(direction))
        {
            return;
        }

        Vector3 ancher = transform.position + (new Vector3(direction.x, -1, direction.y)) * 0.5f;//회전시킬 위치
        Vector3 axis = Vector3.Cross(Vector3.up, new Vector3(direction.x, 0, direction.y));//두선과 수직인 회전시킬 축을 찾기
        //Debug.Log($"axis :  {axis}");

        if (!CheckNextGround(direction))
        {
            if (cubeController.skillActive)
            {
                StartCoroutine(RollDown(ancher, axis));
                return;
            }
            StartCoroutine(RollBack(ancher, axis));
            return;
        }

        StartCoroutine(Roll(ancher, axis));
        
    }

    private bool CheckWall(Vector2 direction)
    {
        Vector3 dir = new Vector3(direction.x, 0, direction.y);
        Ray ray = new Ray(transform.position, dir);
        Debug.DrawRay(transform.position, dir, Color.red);

        if (Physics.Raycast(ray, 1.5f, groundlayerMask))
        {
            return true;
        }

        return false;
    }

    private bool CheckNextGround(Vector2 direction)
    {
        Ray ray = new Ray(transform.position + (new Vector3(direction.x, 0, direction.y)), Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (Physics.Raycast(ray, 0.6f, groundlayerMask))
        {
            //cubeController.playerQuadController.BlockInteract(BlockInteractionType.None);
            return true;
        }

        return false;
    }

    IEnumerator Roll(Vector3 ancher, Vector3 axis)
    {
        isMoving = true;

        for (int i = 0; i < rotateRate; i++)
        {
            transform.RotateAround(ancher, axis, rotateSpeed);//지정한 점을 통과하는 벡터를 중심으로 회전 
            yield return YieldCache.WaitForSeconds(0.01f);
        }

        isMoving = false;
    }

    IEnumerator RollBack(Vector3 ancher, Vector3 axis)
    {
        isMoving = true;

        for (int i = 0; i < rollBackInt; i++)//회전이후 축이 틀어지지 않도록 고정된 int값을 사용.
        {
            transform.RotateAround(ancher, axis, rotateSpeed);
            yield return YieldCache.WaitForSeconds(0.01f);
        }
        for (int i = 0; i < rollBackInt; i++)
        {
            transform.RotateAround(ancher, axis, -rotateSpeed);
            yield return YieldCache.WaitForSeconds(0.01f);
        }

        isMoving = false;
    }

    IEnumerator RollDown(Vector3 ancher, Vector3 axis)
    {
        isMoving = true;

        Vector3 downVec = ((transform.position -  ancher) * 2) / rotateRate;
        moveAfterPosition = transform.position + ancher * 2;

        for (int i = 0; i < rotateRate; i++)
        {
            transform.RotateAround(transform.position, axis, rotateSpeed);//지정한 점을 통과하는 벡터를 중심으로 회전 
            transform.position -= downVec;
            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
    }
}
