/// <summary>
/// UI 타입
/// </summary>
public enum UIType
{
    MapInteractionEditorUI = 0,     //맵 이벤트 에디터 UI
    MapLookUI,                      //맵 3인칭 뷰로 볼 때 UI
    FileBrowserPopup,               //파일 브라우저 UI
    ChoiceFloorPopup,               //몇층까지 올라갈 수 입력하는 UI

    MapChoicePopup = 10,            //맵고르는 UI
    HelpPopup,                      //도움말 UI
    OptionPopup,                    //옵션 UI

    PausePopup = 20,                //Pause UI
    ClearPopup                      //Clear UI
}
