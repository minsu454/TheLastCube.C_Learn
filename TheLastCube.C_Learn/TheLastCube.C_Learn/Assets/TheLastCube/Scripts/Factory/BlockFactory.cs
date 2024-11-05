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
}