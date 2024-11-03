using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    private Vector2 moveDir;

    [Header("Look")]
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Vector3 StartLookPos;
    private float camCurXRot;
    private Vector2 mouseDelta;

    private void OnEnable()
    {
        cameraContainer.position = StartLookPos;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnLateUpdate()
    {
        Look();
    }

    private void Move()
    {
        Vector3 dir = cameraContainer.forward * moveDir.y + cameraContainer.right * moveDir.x;

        cameraContainer.position += dir * moveSpeed * Time.fixedDeltaTime;
    }

    private void Look()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        cameraContainer.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
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

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

}
