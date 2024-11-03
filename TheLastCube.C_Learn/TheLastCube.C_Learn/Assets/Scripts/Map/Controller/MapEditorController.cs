using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapEidtorController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 moveDir;

    [Header("Draw")]
    private GameObject curhitblock;
    private bool isDraw = false;

    private void OnEnable()
    {
        transform.position = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        SetMaterial();
    }

    private void SetMaterial()
    {
        if (!isDraw)
            return;

        GetSelectedMapPosition();

        if (curhitblock == null)
            return;

        MapBlock block = curhitblock.GetComponent<MapBlock>();

        Debug.Log(MapEditorManager.Instance.CurMaterial);

        block.SetGroundMaterial(MapEditorManager.Instance.CurMaterial);
    }

    private void GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayer))
        {
            curhitblock = hit.transform.gameObject;
        }
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

    public void OnMouseLeftBtn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isDraw = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isDraw = false;
        }
    }

    public void OnMouseRightBtn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isDraw = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isDraw = false;
        }
    }
}
