using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatEventBlock : MapBlock, IMapEventBlock
{
    public void SetData(BlockEventType eventType)
    {
        data.EventType = eventType;
        GroundRenderer.material = Managers.Material.Return(data.EventType);
        gameObject.SetActive(false);
    }

    public void OnEvent()
    {
        gameObject.SetActive(true);
    }
}
