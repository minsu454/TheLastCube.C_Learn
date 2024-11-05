using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
    private Renderer _renderer;
    public BlockInteractionType playerQuadType;

    public int index;
    public Renderer normalQuadRenderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = normalQuadRenderer.material;
    }

    public void ChangeQuadRenderer(BlockInteractionType blockInteractionType)
    {
        playerQuadType = blockInteractionType;
        _renderer.material = Managers.Material.Return(blockInteractionType);
    }

    public void interactQuadRenderer(BlockInteractionType blockInteractionType)
    {
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

    public void ResetQuad()
    {
        playerQuadType = BlockInteractionType.None;
        _renderer.material = normalQuadRenderer.material;
    }
}
