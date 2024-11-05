using UnityEngine;

public static class BlockFactory
{
    public static int MapBlockPrefabIndex(BlockMoveType type)
    {
        int mapBlock;
        switch (type)
        {
            case BlockMoveType.Up:
                mapBlock = (int)BlockPrefabNameType.UpBlock;
                break;
            default:
                mapBlock = (int)BlockPrefabNameType.MapBlock;
                break;
        }

        return mapBlock;
    }

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