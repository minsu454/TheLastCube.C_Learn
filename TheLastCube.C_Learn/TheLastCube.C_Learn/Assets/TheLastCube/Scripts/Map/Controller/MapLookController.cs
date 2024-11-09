using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapLookController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed;       //움직이는 속도
    [SerializeField] private Vector3 startLookPos;  //3d로 볼때 시작 위치
    private Vector2 moveDir;                        //움직이는 방향
    private float moveY = 0;                        //움직이는 y값 저장

    [Header("Look")]
    [SerializeField] private float lookSensitivity; //화면 돌아가는 속도
    [SerializeField] private Vector3 StartLookRot;  //3d로 볼때 시작 각도
    private bool lookKeyDown;           //화면 돌리는 키 다운 변수
    private Vector2 mouseDelta;         //마우스 델타

    private void OnEnable()
    {
        transform.position = startLookPos;
        transform.rotation = Quaternion.Euler(StartLookRot);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();
    }

    /// <summary>
    /// 맵에서 움직이는 함수
    /// </summary>
    private void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;
        dir.y += moveY;

        transform.position += dir * moveSpeed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// 맵에서 카메라 움직이는 함수
    /// </summary>
    private void Look()
    {
        if (!lookKeyDown)
            return;

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    /// <summary>
    /// Input System로 움직임 값을 받아오는 함수
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveDir = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveDir = Vector2.zero;
        }
    }

    /// <summary>
    /// Input System로 위로 움직임 값을 받아오는 함수
    /// </summary>
    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveY = 1;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveY = 0;
        }
    }

    /// <summary>
    /// Input System로 아래로 움직임 값을 받아오는 함수
    /// </summary>
    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveY = -1;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            moveY = 0;
        }
    }

    /// <summary>
    /// Input System로 마우스 델타 값을 받아오는 함수
    /// </summary>
    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            lookKeyDown = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            lookKeyDown = false;
        }
    }

    public void MouseDelta(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

}
