using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
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
    [SerializeField] private GameObject MainCanvas;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private TextMeshProUGUI GameCrealorOverText;

    private void Update()
    {
        ButtonActivate();
    }


    public void toglePanel(int selectPanelNumber)
    {
        switch (selectPanelNumber)
        {
            case 0:
                startUI.SetActive(!startUI.activeSelf);
                break;

            case 1:
                characterUI.SetActive(!characterUI.activeSelf);
                break;

            case 2:
                settingUI.SetActive(!settingUI.activeSelf);
                break;
            case 3:
                StageEnum selectedStageIndex = GameManager.Instance.SelectManager.GetSelectedStageIndex();
                Debug.Log("선택된 스테이지 인덱스" + selectedStageIndex);
                if (Enum.IsDefined(typeof(StageEnum), selectedStageIndex))
                {
                    StageEnum stage = selectedStageIndex;
                    Debug.Log("선택된 스테이지 " + stage);
                    switch (stage)
                    {
                        case StageEnum.Castle:
                            Debug.Log("Castle");
                            break;
                        case StageEnum.Swamp:
                            Debug.Log("Swamp");
                            break;
                        case StageEnum.Volcano:
                            Debug.Log("Volcano");
                            break;
                    }
                    MainCanvas.SetActive(!MainCanvas.activeSelf);
                    GameManager.Instance.StartGame();
                }
                break;
            case 4:
                GameOverPanel.SetActive(!GameOverPanel.activeSelf);
                MainCanvas.SetActive(!MainCanvas.activeSelf);
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
    /// 다음스테이지 화면을 볼 수 있게 해주는 버튼
    /// </summary>
    public void NextStageButton()
    {
        int number = (int)GameManager.Instance.SelectManager.GetSelectedStageIndex();
        number = number + 1;
        GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
    }
    /// <summary>
    /// 스테이지 화면을 볼 수 있게 해주는 버튼
    /// </summary>
    public void PreviousStageButton()
    {
        int number = (int)GameManager.Instance.SelectManager.GetSelectedStageIndex();
        number = number - 1;
        GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
    }
    public void GameOver()
    {
        GameCrealorOverText.text = "Game Over";
        GameOverPanel.SetActive(true);
    }
    public void GameClear()
    {
        GameCrealorOverText.text = "Game Clear";
        GameOverPanel.SetActive(true);
    }   
}
