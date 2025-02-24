using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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


    public void NextStageButton()
    {
       int number = GameManager.Instance.selectManager.GetSelectedStageIndex();
        //if()
        GameManager.Instance.selectManager.SetSelectedStageIndex( number);
    }

    public void PreviousStageButton()
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
