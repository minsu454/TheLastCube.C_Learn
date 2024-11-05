using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePopup : BasePopup
{
    public GameObject InGamePop; // 설정창 UI 패널
    private bool isPaused = false; // 게임이 멈춰있는지 여부
    public void OnHelpBtn()
    {
        Managers.UI.CreateUI(UIType.HelpPopup);
    }

    public void OnMainMenuBtn()
    {
        Managers.UI.CreateUI(UIType.StartSceneUI);
    }

    public void OnPauseBtn()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    // 게임을 멈추거나 재개하는 메서드
    void TogglePause()
    {
        if (isPaused)
        {
            // 게임 재개
            Time.timeScale = 1f;  // 게임 시간 재개
            InGamePop.SetActive(false);  // 설정창 닫기
        }
        else
        {
            // 게임 일시 정지
            Time.timeScale = 0f;  // 게임 시간 정지
            InGamePop.SetActive(true);  // 설정창 열기
        }

        isPaused = !isPaused;  // 게임의 상태를 토글
    }

}
