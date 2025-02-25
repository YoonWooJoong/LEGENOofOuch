using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameManager gameManager = new GameManager();
    SelectManager selectManager;
    [SerializeField] private GameObject nextStageButton;
    [SerializeField] private GameObject previousStageButton;

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject settingUI;

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
        }
    }


    /// <summary>
    /// 다음 스테이지 버튼
    /// 마지막 스테이지에서는 버튼 비활성화
    /// </summary>
    public void NextStageButton()
    {
        int number = gameManager.SelectManager.GetSelectedStageIndex();
        if (number < 2)
        {
            int nextNumber = number + 1;
            GameManager.Instance.SelectManager.SetSelectedStageIndex(nextNumber);
            selectManager.UpdateStageUI();

            previousStageButton.SetActive(true);
            if (nextNumber == 2)
            {
                nextStageButton.SetActive(false);
            }

        }
    }
    /// <summary>
    /// 이전 스테이지 버튼
    /// 처음시작 
    /// </summary>
    public void PreviousStageButton()
    {
        int number = gameManager.SelectManager.GetSelectedStageIndex();
        if (number > 0)
        {
            int nextnumber = number - 1;
            GameManager.Instance.SelectManager.SetSelectedStageIndex(nextnumber);
            selectManager.UpdateStageUI();

            nextStageButton.SetActive(true);
            if (nextnumber == 0)
            {
                previousStageButton.SetActive(false);
            }

        }
    }

    /*
    public void NextStage()
    {
        SelectIndex = (SelectIndex + 1) % SelectImages.Length;
        UpdateStageUI();
        LoadSelectedMap();
    }

    public void PreviousStage()
    {
        SelectIndex = (SelectIndex - 1 + SelectImages.Length) % SelectImages.Length;
        UpdateStageUI();
        LoadSelectedMap();
    }*/
}
