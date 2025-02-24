using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaManager : MonoBehaviour
{   
    public static GachaManager Instance { get; private set; }
    public GameObject gachaobject;
    public Gacha gacha;
    public GachaHandler gachaHandler;
    public int[] selectedAbility;
    public string[] AbilityName {  get; private set; }
    public string[] Abilitydescription {  get; private set; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        AbilityName = new string[3]; //배열 크기 초기화
        Abilitydescription = new string[3]; //배열 크기 초기화
        gachaobject.gameObject.SetActive(false);
    }

    public void Start()
    {
        gachaobject.gameObject.SetActive(true);
        StartGacha();
    }
    public void StartGacha()
    {
        gachaHandler.init();
        gacha.SelectRandomAbility();
        gachaHandler.StartGacha();
        GetAbilityName();
        GetAbilitydescription();
    }
    public void GetAbilityName()
    {
        //게임매니저에서 가져오기
        AbilityName[0] = "1번스킬이름";
        AbilityName[1] = "2번스킬이름";
        AbilityName[2] = "3번스킬이름";

    }
    public void GetAbilitydescription()
    {
        Abilitydescription[0] = "1번스킬설명";
        Abilitydescription[1] = "2번스킬설명";
        Abilitydescription[2] = "3번스킬설명";
    }
    public AbilityEnum GachaSelect(AbilityEnum abilityEnum)
    {
        Debug.Log(abilityEnum);
        gachaobject.gameObject.SetActive(false);
        return abilityEnum;
    }


}
