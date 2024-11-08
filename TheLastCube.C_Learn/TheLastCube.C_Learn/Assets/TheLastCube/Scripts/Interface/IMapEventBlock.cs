/// <summary>
/// 블록 이벤트 인터페이스
/// </summary>
public interface IMapEventBlock
{
    /// <summary>
    /// 이벤트 실행
    /// </summary>
    public void OnEvent();

    /// <summary>
    /// 데이터 세팅
    /// </summary>
    public void SetData(BlockEventType type);
}