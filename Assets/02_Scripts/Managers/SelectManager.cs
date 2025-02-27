using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
public class SelectManager : MonoBehaviour
{

    public Image imageStage;
    public Sprite[] stageImages;
    [SerializeField] private string[] stageName;
    public TextMeshProUGUI textStageName;
    private int selectedStageIndex; // 선택된 스테이지 인덱스


    public Image characterPreview; //선택된 캐릭터 미리보기
    [SerializeField] private Sprite[] characterImages; //캐릭터이미지 배열
    [SerializeField] private string[] characterNames; // 캐릭터 이름
    public TextMeshProUGUI characterNameText; // 선택된 캐릭터 이름 표시
    private int selectedCharacterIndex; // 선택된 캐릭터 인덱스


    private void Awake()
    {
        SelectCharater(0);
        SetSelectedStageIndex(0);
    }


    /// <summary>
    /// 선택된 캐릭터 인덱스값 반환
    /// </summary>
    /// <returns></returns>
    public PlayerClassEnum GetSelectedCharacter() 
    {
        return (PlayerClassEnum)selectedCharacterIndex;
    }


    /// <summary>
    ///선택창에 캐릭터 이미지,이름 표시
    /// </summary>
    /// <param name="index"></param>
    public void SelectCharater(int index)
    {
        PlayerClassEnum playerClass = (PlayerClassEnum)index;

        selectedCharacterIndex = index;
        GameManager.Instance.SelectManager.selectedCharacterIndex = index;//게임메니저에 선택된 캐릭터 인덱스 전달

        characterPreview.sprite = characterImages[index]; // 선택된 캐릭터 이미지 표시
        characterNameText.text = characterNames[index]; //선택된 캐릭터 이름 표시
    }

    /// <summary>
    /// 스테이지 인덱스값  반환
    /// </summary>
    /// <returns></returns>
    public StageEnum GetSelectedStageIndex()
    {
        return (StageEnum)selectedStageIndex;
    }


    /// <summary>
    /// 선택창에 스테이지 이미지,이름 표시
    /// </summary>
    /// <param name="number"></param>
    public void SetSelectedStageIndex(int number)
    {
        StageEnum stage = (StageEnum)number;

        selectedStageIndex = number;
        GameManager.Instance.SelectManager.selectedStageIndex = number;// 게임메니저에 선택된 스테이지 인덱스 전달

        imageStage.sprite = stageImages[number];
        textStageName.text = stageName[number];
    }
}
