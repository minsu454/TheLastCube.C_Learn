/// <summary>
/// 블록움직이는타입
/// </summary>
public enum BlockMoveType
{
    None = 0,
    Start,      //시작 블록
    End,        //끝나는 지점 블록
    Up,         //밟을 시 위로 올라가는 블록
    Break,      //파괴가능 블록
}