using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaManager : MonoBehaviour
{   
    public static GachaManager Instance { get; private set; }
    public GameObject gachaobject;
    public Gacha gacha;
    public GachaController gachaHandler;
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


    /// <summary>
    /// 게임매니저에서 가챠를 실행하는 함수
    /// </summary>
    public void StartGacha()
    {
        //가챠싸운드 / SFX/UI폴더에 넣어놨습니다.
        SoundManager.instance.PlaySFX("");
        gachaobject.gameObject.SetActive(true);
        gachaHandler.init();
        gacha.SelectRandomAbility();
        GameManager.Instance.SetAbilityText();
        gachaHandler.StartGacha();
    }

    /// <summary>
    /// 게임매니저에서 스킬이름을 가져오는 함수
    /// </summary>
    public void GetAbilityName(string[] name)
    {
        //게임매니저에서 가져오기
        AbilityName[0] = name[0];
        AbilityName[1] = name[1];
        AbilityName[2] = name[2];

    }

    /// <summary>
    /// 게임매니저에서 스킬설명을 가져오는 함수
    /// </summary>
    public void GetAbilitydescription(string[] description)
    {
        Abilitydescription[0] = description[0];
        Abilitydescription[1] = description[1];
        Abilitydescription[2] = description[2];
    }

    /// <summary>
    /// 가챠에서 선택한 스킬을 반환하는 함수
    /// </summary>
    /// <param name="abilityEnum"></param>
    /// <returns></returns>
    public void GachaSelect(AbilityEnum abilityEnum)
    {
        gachaobject.gameObject.SetActive(false);
        GameManager.Instance.GetAbility(abilityEnum);
    }


}
