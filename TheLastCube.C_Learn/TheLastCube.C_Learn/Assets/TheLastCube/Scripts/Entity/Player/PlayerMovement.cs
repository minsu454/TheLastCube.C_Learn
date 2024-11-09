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
    private Vector3 moveAfterPosition; //이동 이후 보정값을 넣으려고 했지만 불필요 할 것 같아서 사용X 하지만 나중에 사용할 수 있기 때문에 남겨놓음
    private GameObject bottomGround; //파티클 사용을 위해서

    private PlayerController cubeController; //모든 정보를 가지고 있는 PlayerController
    public LayerMask groundlayerMask; //아래가 ground인지 확인용 나중에 장애물이 추가된다면 인스펙터창에서 추가해주거나 레이어 마스크를 나눠야 함(지금은 벽만 확인)
    private bool isMoving = false; //플레이어가 이동 중이는 많은 기능들을 사용할 수 없어야 해서 존재하는 변수(스킬, 이동, 등)

    [SerializeField]private float rotateSpeed; //회전 속도
    [SerializeField]private int maxCheckDistance = 5; //yellowskill로 이동할 거리

    private float rotateRate; //회전 속도에 반비례하는 각도, 먼저 변수를 선언해주고 사용시에만 변수 값을 바꿔주기 위해서(매번 선언 하는 것은 낭비)
    private int rollBackInt=10; //통칭 까딱까딱은 float값이 들어가면 회전 상태가 이상해질 때가 있어서 고정된 int 값을 사용하게 만듬

    private void Awake()
    {
        cubeController = GetComponent<PlayerController>();
        EventManager.Subscribe(GameEventType.LockPlayerMove, LockMove); //upBlock을 타고 이동 할 때는 이동을 멈춰야 하기 때문에 UpBlock과 같이 EventManger에 구독
    }
    private void Start()
    {
        rotateRate = 90 / rotateSpeed; 

        cubeController.OnMoveEvent += Move; //PlayerController가 상속하는 TopdownController에 있는 event에 구독
        cubeController.OnSpecialMoveEvent += SpecialMove;
    }

    private void LockMove(object args) //UpBlock이 이동 중이면 플레이어의 isMoving도 true로 바꿔줌
    {
        isMoving = (bool)args;
    }

    private void Move(Vector2 direction) //스킬이 없는 일반 이동
    {
        bool redskillActive = cubeController.redSkill;

        if (isMoving) //이동 중이면 이동을 해서는 안됨
        {
            return;
        }

        if (CheckWall(direction))
        {
            return;
        }

        Vector3 ancher = transform.position + (new Vector3(direction.x, -1, direction.y)) * 0.5f;//회전시킬 위치
        Vector3 axis = Vector3.Cross(Vector3.up, new Vector3(direction.x, 0, direction.y));//두선과 수직인 회전시킬 축을 찾기

        if (!CheckNextGround(direction))//가려는 방향에 ground가 없으면
        {
            if (redskillActive)//하지만 redskill이 켜져있으면 이동 가능
            {
                StartCoroutine(RollDown(ancher, axis));
                return;
            }

            if (ReturnMapBlock() == null)//만약에 허공에 떠있으면, 아래에 블록이 없으면 ( 사실상 불가능 하지만 방어 코드 같은 느낌 )
            {//red 스킬을 키면 앞에서 처리를 해주기 때문에 여기에 들어올 수 없다
                return;
            }

            BlockMoveType blockMoveType = ReturnMapBlock().data.MoveType;//아래 블록의 종류를 확인

            if (CheckNextGround2(direction))//가려는 방향에 ground가 없지만 가려는 방향의 한칸 아래에 있는지 확인 (굴러서 한칸을 떨어질 수 있는지)
            {
                StartCoroutine(Roll(ancher, axis, 1));//OverLoading 기능 - 91 줄과 비교 (한번 검색해서 보세요)
            }
            else if (blockMoveType != BlockMoveType.Up)//지금 보니 바꿔야하는 코드 현재는 이동 블록이 UpBlock뿐이라 문제 없음 종류가 다양해지면 오류가 발생할 수 있음
            {// == null 로 바꾸면 가능 할 것 같지만 일단은 그대로 남김
                StartCoroutine(RollBack(ancher, axis));
            }
            return;
        }

        if (redskillActive)//red 스킬이 켜져있으면 일반적인 구르기는 못하게 만듬
        {
            return;
        }

        StartCoroutine(Roll(ancher, axis));//모든 조건 확인 후 일반적인 구르기
    }

    private void SpecialMove(Vector2 direction)//yellowskill이 켜져있으면 발동할 이동
    {
        if (isMoving)
        {
            return;
        }

        StartCoroutine(CheckRoad(direction));
    }

    private bool CheckWall(Vector2 direction) //바로 옆에 벽이 있는가
    {
        Vector3 dir = new Vector3(direction.x, 0, direction.y);
        Ray ray = new Ray(transform.position, dir);
        //Debug.DrawRay(transform.position, dir, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, 1.4f, groundlayerMask))//모든 체크는 중심에서 하기 때문에 0.5 + 1 딱 붙은 경우 오류가 발생해 0.1 빼주기 
        {
            return true;//벽이 있다
        }

        return false;//없다
    }

    private bool CheckWall2(Vector2 direction) // 파괴 가능한 블록(yellowSkill) 용
    {
        Vector3 dir = new Vector3(direction.x, 0, direction.y);
        Ray ray = new Ray(transform.position, dir);
        //Debug.DrawRay(transform.position, dir, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.4f))
        {
            BreakBlock wall;
            if (!hit.collider.gameObject.TryGetComponent<BreakBlock>(out wall)) return true;//옆에 무언가 있는데 BreakBlock 타입이면 무시

            if (wall.data.MoveType == BlockMoveType.Break)// BreakBlock 타입이면 부수면서 이동
            {
                wall.Broken();
            }
        }

        return false;
    }

    private bool CheckNextGround(Vector2 direction)// 이동 방향의 아래를 확인
    {
        Ray ray = new Ray(transform.position + (new Vector3(direction.x, 0, direction.y)), Vector3.down);
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (Physics.Raycast(ray, 0.6f, groundlayerMask))// 오히려 닿아야 해서 0.5 + 0.1
        {
            //cubeController.playerQuadController.BlockInteract(BlockInteractionType.None);
            return true;
        }

        return false;
    }
    private bool CheckNextGround2(Vector2 direction)// 이동 방향의 한칸 아래를 확인
    {
        Ray ray = new Ray(transform.position + (new Vector3(direction.x, 0, direction.y)), Vector3.down);
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (Physics.Raycast(ray, 1.6f, groundlayerMask))// 0.5 + 1 + 0.1
        {
            //cubeController.playerQuadController.BlockInteract(BlockInteractionType.None);
            return true;
        }

        return false;
    }

    public bool CheckGround()//현재 위치의 아래를 확인 (interaction을 위해서)
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 0.6f, groundlayerMask))
        {
            return true;
        }
        return false;
    }

    private MapBlock ReturnMapBlock()//아래에 MapBlock 블록이 존재하는지 확인
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

    IEnumerator Roll(Vector3 ancher, Vector3 axis)//일반 구르기 코루틴을 사용하려면 IEnumerator 필수
    {
        isMoving = true;        

        for (int i = 0; i < rotateRate; i++)
        {
            transform.RotateAround(ancher, axis, rotateSpeed);//지정한 점을 통과하는 벡터를 중심으로 회전 
            yield return YieldCache.WaitForSeconds(0.01f); //특정 시간 만큼 정지,  YieldCache 는 민수님이 만든 기능으로 불필한 코루틴 생성을 방지
        }

        isMoving = false;
        
        cubeController.playerQuadController.BlockInteract(ReturnMapBlock());//구르기 종료 후 블록을 확인 후 상호작용이 가능하면 실행
    }

    IEnumerator Roll(Vector3 ancher, Vector3 axis, int ver)//overloading
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

    IEnumerator RollBack(Vector3 ancher, Vector3 axis)//까딱까딱
    {
        isMoving = true;       


        for (int i = 0; i < rollBackInt; i++)//회전이후 축이 틀어지지 않도록 고정된 int값을 사용.
        {
            transform.RotateAround(ancher, axis, rotateSpeed);//먼저 이동 후
            yield return YieldCache.WaitForSeconds(0.01f);
        }
        for (int i = 0; i < rollBackInt; i++)
        {
            transform.RotateAround(ancher, axis, -rotateSpeed);// 그만큼 돌아옴
            yield return YieldCache.WaitForSeconds(0.01f);
        }

        isMoving = false;
    }

    IEnumerator RollDown(Vector3 ancher, Vector3 axis)//redskill이 켜져있을 때만 들어오는 함수
    {
        if (cubeController.redSkillCount <= 0) yield break;//이동 가능 횟수를 넘기면 더이상 이동 X
        isMoving = true;

        Vector3 downVec = ((transform.position -  ancher - new Vector3(0,0.25f,0)) * 2) / rotateRate; //반칸만 아래로

        //transform.DOMove(transform.position - (transform.position - ancher - new Vector3(0, 0.25f, 0)) * 2, 0.5f).SetEase(Ease.InOutQuart);

        for (int i = 0; i < rotateRate; i++)
        {
            transform.RotateAround(transform.position, axis, rotateSpeed);//지정한 점을 통과하는 벡터를 중심으로 회전 
            transform.position -= downVec;
            yield return new WaitForFixedUpdate();
        }
        cubeController.redSkillCount -= 1; // 이동 가능 횟수 차감

        isMoving = false;
    }

    IEnumerator CheckRoad(Vector2 direction)// yellowSkill 용
    {
        isMoving = true;
        for (int i = 0; i < maxCheckDistance; i++) //정해진 이동 거리 만큼
        {

            if (CheckNextGround(direction) && !CheckWall2(direction)) // 빠르게 maxCheckDistance만큼 이동하는 것이기 때문에
            {
                transform.position += new Vector3(direction.x, 0, direction.y);

                cubeController.playerQuadController.BlockInteract(ReturnMapBlock()); // 가는 도중에도 상호작용 가능
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

    private void OnDestroy()// 민수님이 추가한 기능 아마도 블록 파괴 시(게임 오버 시) UpBlock 구독 취소 
    {
        EventManager.Unsubscribe(GameEventType.LockPlayerMove, LockMove);
    }

    public bool Moving()//isMoving은 private 으로 유지하고 싶어서 프로퍼티와 비슷한 느낌으로 만듬, 다른 방법이 있을 것 같다
    {
        return isMoving;
    }
}
