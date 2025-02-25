using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    SelectManager selectManager;
    [SerializeField] private GameObject nextStageButton;
    [SerializeField] private GameObject previousStageButton;
    [SerializeField] private GameObject startButton;

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


    public void NextStageButton()
    {
        int number = GameManager.Instance.SelectManager.GetSelectedStageIndex();
        if (number < 2)
        {
            number = number + 1;
            GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
            selectManager.UpdateStageUI();

            previousStageButton.SetActive(true);
        }
        if (number == 2)
        {
            nextStageButton.SetActive(false);
        }
    }

    public void PreviousStageButton()
    {
        int number = GameManager.Instance.SelectManager.GetSelectedStageIndex();
        if (number > 0)
        {
            number = number - 1;
            GameManager.Instance.SelectManager.SetSelectedStageIndex(number);
            selectManager.UpdateStageUI();

            nextStageButton.SetActive(true);
        }
        if (number == 0)
        {
            previousStageButton.SetActive(false);
        }
    }

    public void StartButton()
    {
        
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
