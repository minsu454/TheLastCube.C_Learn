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
}
