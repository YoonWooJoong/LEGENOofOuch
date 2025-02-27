using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    SelectManager selectManager;
    [SerializeField] private GameObject nextStageButton;
    [SerializeField] private GameObject previousStageButton;
    [SerializeField] private GameObject startButton;

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject TutorialUI;
    [SerializeField] private GameObject AchievementUI;
    [SerializeField] private GameObject MainCanvas;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private TextMeshProUGUI GameCrealorOverText;

    private void Update()
    {
        ButtonActivate();
    }

    ///int 값에 따른 패널 실행
    public void toglePanel(int selectPanelNumber)
    {
        switch (selectPanelNumber)
        {
            case 0://시작 패널
                startUI.SetActive(!startUI.activeSelf);
                break;

            case 1: //캐릭터 패널
                characterUI.SetActive(!characterUI.activeSelf);
                break;

            case 2: //설정 패널
                settingUI.SetActive(!settingUI.activeSelf);
                break;
            case 3: //게임시작 버튼
                StageEnum selectedStageIndex = GameManager.Instance.SelectManager.GetSelectedStageIndex();
                if (Enum.IsDefined(typeof(StageEnum), selectedStageIndex))
                {
                    StageEnum stage = selectedStageIndex;
                    switch (stage)
                    {
                        case StageEnum.Castle:
                            break;
                        case StageEnum.Swamp:
                            break;
                        case StageEnum.Volcano:
                            break;
                    }
                    MainCanvas.SetActive(!MainCanvas.activeSelf);
                    GameManager.Instance.StartGame();
                }
                break;
            case 4: //게임오버 패널 ->  메인캔버스로 이동
                GameManager.Instance.LevelManager.DestroyMap();
                SoundManager.instance.PlayBGM("MainBGM");
                GameOverPanel.SetActive(!GameOverPanel.activeSelf);
                MainCanvas.SetActive(!MainCanvas.activeSelf);
                break;
            case 5: //오버 패널 -> 다시시작하는 버튼
                GameManager.Instance.LevelManager.DestroyMap();
                GameOverPanel.SetActive(!GameOverPanel.activeSelf);
                GameManager.Instance.StartGame();
                break;
            case 6: //튜토리얼 패널
                TutorialUI.SetActive(!TutorialUI.activeSelf);
                break;
            case 7: //업적 패널
                AchievementUI.SetActive(!AchievementUI.activeSelf);
                break;



        }
    }
    /// <summary>
    /// 버튼 활성화 , 비활성화
    /// </summary>
    public void ButtonActivate()
    {
        StageEnum number = GameManager.Instance.SelectManager.GetSelectedStageIndex();

        previousStageButton.SetActive(number > 0);

        nextStageButton.SetActive((int)number < GameManager.Instance.SelectManager.stageImages.Length - 1);
    }
    /// <summary>
    /// 선택창에서 다음스테이지를 띄워주는 버튼
    /// </summary>
    public void NextStageButton()
    {
        int number = (int)GameManager.Instance.SelectManager.GetSelectedStageIndex();
        number = number + 1;
        GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
    }
    /// <summary>
    /// 선택창에서 이전스테이지 를띄워주는 버튼
    /// </summary>
    public void PreviousStageButton()
    {
        int number = (int)GameManager.Instance.SelectManager.GetSelectedStageIndex();
        number = number - 1;
        GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
    }

    /// <summary>
    /// 게임오버 , 클리어시 택스트 출력
    /// </summary>
    
    public void GameEndUI(bool isClear, float gameTimer)
    {
        GameCrealorOverText.text = isClear ? "Game Clear" : "Game Over";
        GameOverPanel.SetActive(true);

        int minutes = (int)(GameManager.Instance.gameTimer / 60);
        int seconds = (int)(GameManager.Instance.gameTimer % 60);
        Time.text = $"{minutes:00}:{seconds:00}";
    }
}
