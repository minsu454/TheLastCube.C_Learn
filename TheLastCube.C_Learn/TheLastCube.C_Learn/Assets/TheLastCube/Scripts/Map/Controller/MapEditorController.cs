using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        if (EventSystem.current.IsPointerOverGameObject()) //UI 반환
            return;

        if (!isDraw)
            return;

        GetSelectedMapPosition();

        if (curhitblock == null)
            return;

        MapBlock block = curhitblock.GetComponent<MapBlock>();
        Enum type = MapEditorManager.Instance.EnumType;
        int floor = MapEditorManager.Instance.MapData.ReturnCurFloor();

        if (type is BlockColorType colorType)
        {
            if (colorType == BlockColorType.None)
                MapEditorManager.Instance.MapData.RemoveSave(floor, block);
            else
                MapEditorManager.Instance.MapData.AddSave(floor, block);

            block.SetGround(MapEditorManager.Instance.CurMaterial, colorType);
        }
        else if (type is BlockMoveType moveType)
        {
            block.SetMove(MapEditorManager.Instance.CurMaterial, moveType);
        }
        else if (type is BlockInteractionType interactionType)
        {
            block.SetInteraction(MapEditorManager.Instance.CurMaterial, interactionType);
        }
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
