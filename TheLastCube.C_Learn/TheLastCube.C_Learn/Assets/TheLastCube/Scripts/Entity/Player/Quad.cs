using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour //하나의 면의 정보
{
    private Renderer _renderer;
    public BlockInteractionType playerQuadType;

    [SerializeField] private int index;
    public Material normalQuadMaterial;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = normalQuadMaterial;
    }

    public void ChangeQuadRenderer(BlockInteractionType blockInteractionType)//입력받은 type에 맞게 변경
    {
        playerQuadType = blockInteractionType;
        _renderer.material = Managers.Material.Return(blockInteractionType);
    }

    public void interactQuadRenderer(BlockInteractionType blockInteractionType) //불필요한 코드 지워도 되는데 혹시 모를 오류와 
    {//초기에는 플레이어에서 상호작용도 하려고 했던 모습, 현재는 다른 스크립트에서 처리해 준다
        int caseIndex = (int)blockInteractionType - 100;

        switch (caseIndex)
        {
            case 0://red
                //상호작용 필요
                ResetQuad();
                break;
            case 1://blue
                //
                ResetQuad();
                break;
            default : Debug.Log("Need to Add script"); break;
        }
    }

    public void ResetQuad()//초기화 기능
    {
        playerQuadType = BlockInteractionType.None;
        _renderer.material = normalQuadMaterial;
    }
}
