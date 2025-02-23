using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManagert : MonoBehaviour
{
    public GameObject[] CharaterPrefabs; //프리팹 배열
    public Transform spawnPoint; //선택된 캐릭터가 보일위치
    public string[] characterNames; // 캐릭터 이름
    public Text characterNameText; // 선택된 캐릭터 이름 표시


    private GameObject currentCharacter; // 현재 선택된 캐릭터
    private int selectedCharacterIndex = 0; // 선택된 캐릭터 인덱스

    public void SelectCharater(int index)
    {
        if(currentCharacter != null)
        {
            Destroy(currentCharacter);
        }
        currentCharacter = Instantiate(CharaterPrefabs[index], spawnPoint.position, Quaternion.identity);
        selectedCharacterIndex = index;
    }

    public void PlayAnimation(String animationName)
    {
        if(currentCharacter != null)
        {
            Animator animator = currentCharacter.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play(animationName);
            }
        }
    }


}
