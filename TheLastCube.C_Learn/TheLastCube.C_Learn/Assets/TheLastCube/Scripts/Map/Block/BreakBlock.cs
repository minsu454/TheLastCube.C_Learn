using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MapBlock
{
    // Start is called before the first frame update
    void Start()
    {
        GroundRenderer.material = Managers.Material.Return(data.MoveType);
    }
}
