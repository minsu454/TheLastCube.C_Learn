using Common.Event;
using Common.Yield;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 moveAfterPosition;
    private GameObject bottomGround;

    private PlayerController cubeController;
    public LayerMask groundlayerMask;
    private bool isMoving = false;        

    [SerializeField]private float rotateSpeed;
    [SerializeField]private int maxCheckDistance = 5;

    private float rotateRate;
    private int rollBackInt=10;

    private void Awake()
    {
        cubeController = GetComponent<PlayerController>();
        EventManager.Subscribe(GameEventType.LockPlayerMove, LockMove);
    }
    private void Start()
    {
        rotateRate = 90 / rotateSpeed;

        cubeController.OnMoveEvent += Move;
        cubeController.OnSpecialMoveEvent += SpecialMove;
    }

    private void LockMove(object args)
    {
        isMoving = (bool)args;
    }

    private void Move(Vector2 direction)
    {
        bool redskillActive = cubeController.redSkill;

        if (isMoving)
        {
            return;
        }

        if (CheckWall(direction))
        {
            return;
        }

        Vector3 ancher = transform.position + (new Vector3(direction.x, -1, direction.y)) * 0.5f;//회전시킬 위치
        Vector3 axis = Vector3.Cross(Vector3.up, new Vector3(direction.x, 0, direction.y));//두선과 수직인 회전시킬 축을 찾기

        if (!CheckNextGround(direction))
        {
            if (redskillActive)
            {
                StartCoroutine(RollDown(ancher, axis));
                return;
            }

            if (ReturnMapBlock() == null)
            {
                return;
            }

            BlockMoveType blockMoveType = ReturnMapBlock().data.MoveType;

            if (CheckNextGround2(direction))
            {
                StartCoroutine(Roll(ancher, axis, 1));
            }
            else if (blockMoveType != BlockMoveType.Up)
            {
                StartCoroutine(RollBack(ancher, axis));
            }
            return;
        }

        if (redskillActive)
        {
            return;
        }

        StartCoroutine(Roll(ancher, axis));
    }

    private void SpecialMove(Vector2 direction)
    {
        if (isMoving)
        {
            return;
        }

        StartCoroutine(CheckRoad(direction));
    }

    private bool CheckWall(Vector2 direction)
    {
        Vector3 dir = new Vector3(direction.x, 0, direction.y);
        Ray ray = new Ray(transform.position, dir);
        //Debug.DrawRay(transform.position, dir, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, 1.4f, groundlayerMask))
        {
            return true;
        }

        return false;
    }

    private bool CheckWall2(Vector2 direction)
    {
        Vector3 dir = new Vector3(direction.x, 0, direction.y);
        Ray ray = new Ray(transform.position, dir);
        //Debug.DrawRay(transform.position, dir, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.4f))
        {
            BreakBlock wall = hit.collider.gameObject.GetComponent<BreakBlock>();

            if (wall.data.MoveType == BlockMoveType.Break)
            {
                wall.Broken();
            }
        }

        if (Physics.Raycast(ray, 1.4f, groundlayerMask))
        {
            return true;
        }

        return false;
    }

    private bool CheckNextGround(Vector2 direction)
    {
        Ray ray = new Ray(transform.position + (new Vector3(direction.x, 0, direction.y)), Vector3.down);
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (Physics.Raycast(ray, 0.6f, groundlayerMask))
        {
            //cubeController.playerQuadController.BlockInteract(BlockInteractionType.None);
            return true;
        }

        return false;
    }
    private bool CheckNextGround2(Vector2 direction)
    {
        Ray ray = new Ray(transform.position + (new Vector3(direction.x, 0, direction.y)), Vector3.down);
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (Physics.Raycast(ray, 1.6f, groundlayerMask))
        {
            //cubeController.playerQuadController.BlockInteract(BlockInteractionType.None);
            return true;
        }

        return false;
    }

    public bool CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 0.6f, groundlayerMask))
        {
            return true;
        }
        return false;
    }

    private MapBlock ReturnMapBlock()
    {
        RaycastHit hit;
        MapBlock mapBlock;

        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 0.6f))
        {
            bottomGround = hit.collider.gameObject;
            bottomGround.TryGetComponent<MapBlock>(out mapBlock);
            return mapBlock;
        }

        return null;
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
        
        cubeController.playerQuadController.BlockInteract(ReturnMapBlock());
    }

    IEnumerator Roll(Vector3 ancher, Vector3 axis, int ver)
    {
        isMoving = true;

        for (int i = 0; i < rotateRate; i++)
        {
            transform.RotateAround(ancher, axis, rotateSpeed);//지정한 점을 통과하는 벡터를 중심으로 회전 
            yield return YieldCache.WaitForSeconds(0.01f);
        }
        transform.DOMoveY(transform.position.y -1, 0.5f);

        isMoving = false;

        cubeController.playerQuadController.BlockInteract(ReturnMapBlock());
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
        if (cubeController.redSkillCount <= 0) yield break;
        isMoving = true;

        Vector3 downVec = ((transform.position -  ancher - new Vector3(0,0.25f,0)) * 2) / rotateRate;

        //transform.DOMove(transform.position - (transform.position - ancher - new Vector3(0, 0.25f, 0)) * 2, 0.5f).SetEase(Ease.InOutQuart);

        for (int i = 0; i < rotateRate; i++)
        {
            transform.RotateAround(transform.position, axis, rotateSpeed);//지정한 점을 통과하는 벡터를 중심으로 회전 
            transform.position -= downVec;
            yield return new WaitForFixedUpdate();
        }
        cubeController.redSkillCount -= 1;

        isMoving = false;
    }

    IEnumerator CheckRoad(Vector2 direction)
    {
        isMoving = true;
        for (int i = 0; i < maxCheckDistance; i++)
        {

            if (CheckNextGround(direction) && !CheckWall2(direction))
            {
                transform.position += new Vector3(direction.x, 0, direction.y);

                cubeController.playerQuadController.BlockInteract(ReturnMapBlock());
                yield return YieldCache.WaitForSeconds(0.01f);
            }
            else
            {
                break;
            }
        }
        isMoving = false;
        cubeController.skillActive = false; //노란 큐브의 능력은 사용 시 바로 해제
        cubeController.yellowSkill = false;
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(GameEventType.LockPlayerMove, LockMove);
    }

    public bool Moving()
    {
        return isMoving;
    }
}
