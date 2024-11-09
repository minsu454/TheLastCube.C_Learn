using System;
using System.Collections.Generic;

/// <summary>
/// 맵 하나의 정보 class
/// </summary>
[Serializable]
public class TotalMapData
{
    public string name;             //맵이름
    public int maxFloor;            //맵에 최대 높이
    public List<BlockData> list;    //블록 list
}