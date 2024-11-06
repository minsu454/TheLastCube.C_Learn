using Common.Scene;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    /// <summary>
    /// 초기화 함수
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// 닫는 함수
    /// </summary>
    public virtual void Close()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CloseUI();
    }

    /// <summary>
    /// 닫으면서 다음 씬 불러오는 함수
    /// </summary>
    protected void Close(SceneType type)
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CloseUI(() => SceneManagerEx.LoadScene(type));
    }
}
