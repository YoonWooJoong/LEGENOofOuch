using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class SelectManager : MonoBehaviour
{

    public Image imageStage;
    public Sprite[] stageImages;
    [SerializeField] private string[] stageName;
    public TextMeshProUGUI textStageName;
    private int selectedStageIndex = 0; // 선택된 스테이지 인덱스


    public Image characterPreview; //선택된 캐릭터 미리보기
    [SerializeField] private Sprite[] characterImages; //캐릭터이미지 배열
    [SerializeField] private string[] characterNames; // 캐릭터 이름
    public TextMeshProUGUI characterNameText; // 선택된 캐릭터 이름 표시
    private int selectedCharacterIndex = 0; // 선택된 캐릭터 인덱스

    private void Awake()
    {
        SelectCharater(0);
        SetSelectedStageIndex(0);
    } 

    public void SelectCharater(int index)
    {
        selectedCharacterIndex = index;
        characterPreview.sprite = characterImages[index];
        characterNameText.text = characterNames[index];
    }



    public int GetSelectedStageIndex()
    {
        return selectedStageIndex;
    }

    public void SetSelectedStageIndex(int number)
    {
        selectedStageIndex = number;
        imageStage.sprite = stageImages[number];
        textStageName.text = stageName[number];
    }
}
