using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    private Vector2 moveDir;
    private float moveY = 0;

    [Header("Look")]
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Vector3 StartLookPos;
    [SerializeField] private Vector3 StartLookRot;
    private bool lookKeyDown;
    private Vector2 mouseDelta;

    private void OnEnable()
    {
        transform.position = StartLookPos;
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

    private void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;
        dir.y += moveY;

        transform.position += dir * moveSpeed * Time.fixedDeltaTime;
    }

    private void Look()
    {
        if (!lookKeyDown)
            return;

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

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
