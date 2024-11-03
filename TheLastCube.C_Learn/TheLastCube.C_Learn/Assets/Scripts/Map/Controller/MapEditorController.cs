using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapEidtorController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    private Vector2 moveDir;

    private void OnEnable()
    {
        transform.position = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;

        transform.position += dir * moveSpeed * Time.fixedDeltaTime;
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
}
