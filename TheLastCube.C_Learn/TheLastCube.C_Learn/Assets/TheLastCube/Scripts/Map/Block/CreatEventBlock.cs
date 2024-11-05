using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatEventBlock : MapBlock, IMapEventBlock
{
    public void OnEvent()
    {
        gameObject.SetActive(true);
    }
}
