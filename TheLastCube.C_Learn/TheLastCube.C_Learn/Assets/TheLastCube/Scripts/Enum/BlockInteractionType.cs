/// <summary>
/// 블록상호작용타입
/// </summary>
public enum BlockInteractionType
{
    None = 0,
    Delete,             //밟은 곳에 있는 기믹 삭제 발판

    KeyRed = 10,        //플레이어가 들고다니는 키 발판
    KeyBlue,
    KeyYellow,

    LockRed = 100,      //해당 키 입력하는 발판
    LockBlue,
    LockYellow, 
}
