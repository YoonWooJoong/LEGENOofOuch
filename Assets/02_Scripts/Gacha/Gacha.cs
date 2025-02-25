using System;
using UnityEngine;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEditor.Searcher;

public class Gacha : MonoBehaviour
{
    public int abilityindex = Enum.GetValues(typeof(AbilityEnum)).Length;
    private AbilityEnum[] selectedAbility = new AbilityEnum[3]; // 선택된 3개 능력 인덱스
    public bool isRare = false;

    /// <summary>
    /// 능력을 랜덤으로 선택
    /// 5프로 확률로 레어 능력이 선택됨
    /// 자신의 직업에 맞는 능력만 선택
    /// </summary>
    public void SelectRandomAbility()
    {
        // Rare로 분류할 인덱스 집합
        AbilityEnum[] rareIndices = new AbilityEnum[] {
        AbilityEnum.BloodThirst, AbilityEnum.Invincibility, AbilityEnum.Blaze,
        AbilityEnum.Spirit, AbilityEnum.Archer, AbilityEnum.Mage, AbilityEnum.Warrior
    };
        AbilityEnum DevilIndices = AbilityEnum.ExtraLife;
        AbilityEnum[] sourceIndices;

        // IsRare()로 레어 능력 여부 설정
        IsRare();

        // 플레이어의 직업을 가져옴
        PlayerClassEnum playerClass = PlayerClassEnum.Archer;
        //하드코딩됨 나중에 수정할것

        if (isRare)
        {
            // 레어 능력만 선택
            sourceIndices = rareIndices;
        }
        else
        {
            List<AbilityEnum> nonRareList = new List<AbilityEnum>();

            // 직업에 맞지 않는 능력들을 제외
            foreach (AbilityEnum ability in Enum.GetValues(typeof(AbilityEnum)))
            {
                if (Array.IndexOf(rareIndices, ability) < 0 && ability != DevilIndices && IsAbilityValidForClass(ability, playerClass))
                {
                    nonRareList.Add(ability);
                }
            }

            // 일반 능력 목록을 sourceIndices에 할당
            sourceIndices = nonRareList.ToArray();
        }

        for (int i = sourceIndices.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            AbilityEnum temp = sourceIndices[i];
            sourceIndices[i] = sourceIndices[randomIndex];
            sourceIndices[randomIndex] = temp;
        }

        for (int i = 0; i < selectedAbility.Length; i++)
        {
            selectedAbility[i] = sourceIndices[i];
            Debug.Log($"선택된 능력 {i} : {selectedAbility[i]}");
        }
    }

    /// <summary>
    /// 직업에 맞는 능력만 선택하도록 필터링
    /// </summary>
    private bool IsAbilityValidForClass(AbilityEnum ability, PlayerClassEnum playerClass)
    {
        switch (playerClass)
        {
            case PlayerClassEnum.Warrior:
                return ability != AbilityEnum.Archer && ability != AbilityEnum.Mage;
            case PlayerClassEnum.Archer:
                return ability != AbilityEnum.Warrior && ability != AbilityEnum.Mage;
            case PlayerClassEnum.Mage:
                return ability != AbilityEnum.Warrior && ability != AbilityEnum.Archer;
            default:
                return true; // 모든 직업이 선택할 수 있는 경우 (기본값)
        }
    }

    /// <summary>
    /// 게임메니저에게 선택된 능력을 전달
    /// </summary>
    /// <returns></returns>
    public AbilityEnum[] GetSelectedAbility()
    {
        return selectedAbility;
    }

    /// <summary>
    /// 레어 능력이 선택되었는지 여부 반환
    /// </summary>
    /// <returns></returns>
    public bool GetIsRare()
    {
        return isRare;
    }

    /// <summary>
    /// 확률에 따라 레어 능력을 선택
    /// </summary>
    public void IsRare()
    {
        if (UnityEngine.Random.Range(0, 100) < 5)
            isRare = true;
    }

    public AbilityEnum[] GachaSelect()
    {
        return selectedAbility;
    }
}