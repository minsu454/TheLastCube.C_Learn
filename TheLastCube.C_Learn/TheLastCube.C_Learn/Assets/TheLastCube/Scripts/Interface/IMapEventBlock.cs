/// <summary>
/// 매니저 인터페이스
/// </summary>
public interface IMapEventBlock
{
    public void OnEvent();

    public void SetData(BlockEventType type);
}