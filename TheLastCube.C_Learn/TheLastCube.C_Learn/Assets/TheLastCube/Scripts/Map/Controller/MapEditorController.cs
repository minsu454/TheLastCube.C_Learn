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
    [SerializeField] private float moveSpeed;           //움직이는 속도
    [SerializeField] private LayerMask groundLayer;     //땅 레이어
    private Vector2 moveDir;                            //움직이는 방향
        
    [Header("Draw")]
    private GameObject curhitblock;                     //현재 맞은 블록 저장변수
    private bool mouseLeftBtn = false;                  //마우스 왼클릭 변수
    private bool mouseRightBtn = false;                 //마우스 우클릭 변수

    private BasePopup basePopup;                        //높에 설정 UI저장 변수

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
        TryDrawMaterial();
    }

    /// <summary>
    /// 머터리얼을 설정할 수 있는지 시도해보는 함수
    /// </summary>
    private void TryDrawMaterial()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //UI 반환
            return;

        if (!mouseLeftBtn && !mouseRightBtn)
            return;

        GetSelectedMapPosition();

        if (curhitblock == null)
            return;

        if (mouseLeftBtn)
        {
            Enum type = MapEditorManager.Instance.EnumType;
            SetMaterial(type, MapEditorManager.Instance.CurMaterial);
        }
        else
        {
            SetMaterial(BlockColorType.None, null);
        }
    }

    /// <summary>
    /// 머터리얼 설정해주는 함수
    /// </summary>
    private void SetMaterial(Enum type, Material material)
    {
        MapEditorBlock block = curhitblock.GetComponent<MapEditorBlock>();
        int floor = MapEditorManager.Instance.MapData.ReturnCurFloor();

        if (type is BlockColorType colorType)
        {
            if (colorType == BlockColorType.None)
                MapEditorManager.Instance.MapData.RemoveSave(floor, block);
            else
                MapEditorManager.Instance.MapData.AddSave(floor, block);

            block.SetGround(material, colorType);
        }
        else if (type is BlockMoveType moveType)
        {
            block.SetMove(material, moveType);
        }
        else if (type is BlockInteractionType interactionType)
        {
            block.SetInteraction(material, interactionType);
        }
        else if (type is BlockEventType eventType)
        {
            block.SetEvent(material, eventType);
        }
    }

    /// <summary>
    /// 레이쏴서 블록에 맞은 것을 저장해주는 함수
    /// </summary>
    private void GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayer))
        {
            curhitblock = hit.transform.gameObject;
        }
    }

    /// <summary>
    /// 맵에서 움직이는 함수
    /// </summary>
    private void Move()
    {
        Vector3 dir = transform.forward * moveDir.y + transform.right * moveDir.x;

        transform.position += dir * moveSpeed * Time.fixedDeltaTime;
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
    /// Input System로 왼쪽 마우스버튼 입력 값을 받아오는 함수
    /// </summary>
    public void OnMouseLeftBtn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            mouseLeftBtn = true;
            
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            mouseLeftBtn = false;
        }
    }

    /// <summary>
    /// Input System로 오른쪽 마우스버튼 입력 값을 받아오는 함수
    /// </summary>
    public void OnMouseRightBtn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            mouseRightBtn = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            mouseRightBtn = false;
        }
    }

    /// <summary>
    /// Input System로 마우스 휠 버튼 입력 값을 받아오는 함수
    /// </summary>
    public void OnMouseMiddleBtn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (basePopup != null)
                return;

            GetSelectedMapPosition();

            if (curhitblock == null)
                return;

            MapEditorBlock block = curhitblock.GetComponent<MapEditorBlock>();
            if (!block.Data.eventBlock)
                return;

            MapEditorManager.Instance.MapData.EventBlock = block;
            basePopup = Managers.UI.CreateUI(UIType.MapInteractionEditorUI);
        }
    }
}
