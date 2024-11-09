using UnityEngine;

public static class BlockFactory
{
    /// <summary>
    /// GameManager에 넣어준 ObjectList의 인덱스를 가져오는 함수
    /// </summary>
    public static int MapBlockPrefabIndex(BlockMoveType type)
    {
        int mapBlock;
        switch (type)
        {
            case BlockMoveType.Up:
                mapBlock = (int)BlockPrefabNameType.UpBlock;
                break;
            case BlockMoveType.Break:
                mapBlock = (int)BlockPrefabNameType.BreakBlock;
                break;
            case BlockMoveType.End:
                mapBlock = (int)BlockPrefabNameType.EndBlock;
                break;
            default:
                mapBlock = (int)BlockPrefabNameType.MapBlock;
                break;
        }

        return mapBlock;
    }

    /// <summary>
    /// GameManager에 넣어준 ObjectList의 인덱스를 가져오는 함수
    /// </summary>
    public static int MapBlockEventPrefabIndex(BlockEventType type)
    {
        int index = -1;

        switch (type)
        {
            case BlockEventType.Create:
                index = (int)BlockPrefabNameType.Createventblock;
                break;
        }

        return index;
    }
}