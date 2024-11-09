using UnityEngine;
using System.Collections.Generic;
using ObjectPool;

public class MapFloor : MonoBehaviour
{
    private readonly Dictionary<Vector3, MapEditorBlock> mapfloorDic = new Dictionary<Vector3, MapEditorBlock>();       //층에 있는 모든 블록 저장 Dictionary
    private const string poolName = "NoneMapBlock";

    /// <summary>
    /// 층하나를 생성해주는 함수
    /// </summary>
    public void Create(int mapScaleX, int mapScaleZ, int curfloor, Transform parent)
    {
        for (int x = 0; x < mapScaleX; x++)
        {
            for (int z = 0; z < mapScaleZ; z++)
            {
                GameObject go = ObjectPoolContainer.Instance.Pop(poolName);
                MapEditorBlock mapBlock = go.GetComponent<MapEditorBlock>();
                mapBlock.transform.parent = parent;
                Vector3 pos = new Vector3(x, curfloor, z);
                mapBlock.Init(curfloor, pos);

                go.SetActive(true);
                mapfloorDic.Add(pos, mapBlock);
            }
        }
    }

    /// <summary>
    /// 해당 층에 있는 위치 오브젝트 반환 함수
    /// </summary>
    public MapEditorBlock Return(Vector3 keyVec)
    {
        if (!mapfloorDic.TryGetValue(keyVec, out MapEditorBlock mapBlock))
        {
            Debug.LogError($"Is Not Found MapBlock : {keyVec}");
            return null;
        }

        return mapBlock;
    }
}