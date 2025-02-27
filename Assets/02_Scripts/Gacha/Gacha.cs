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
    public GachaAbilityController gachaAbilityController;//능력 업그레이드 수치를 저장하는 클래스
   
    /// <summary>
    /// 능력을 랜덤으로 선택
    /// 5프로 확률로 레어 능력이 선택됨
    /// 자신의 직업에 맞는 능력만 선택
    /// 이미 풀업그레이드면 후보풀에서 제외
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
        PlayerClassEnum playerClass = GameManager.Instance.playerClassEnum;

        if (isRare)
        {
            List<AbilityEnum> validRareList = new List<AbilityEnum>();
            foreach (AbilityEnum ability in rareIndices)
            {
                if (IsAbilityValidForClass(ability, playerClass))
                {
                    validRareList.Add(ability);
                }
            }
            // 레어 능력만 선택

            sourceIndices = validRareList.ToArray();
        }
        else
        {
            List<AbilityEnum> nonRareList = new List<AbilityEnum>();

            // 직업에 맞지 않는 능력들을 제외
            foreach (AbilityEnum ability in Enum.GetValues(typeof(AbilityEnum)))
            {
                if (Array.IndexOf(rareIndices, ability) < 0 && ability != DevilIndices)
                {
                    nonRareList.Add(ability);
                }
            }

            // 일반 능력 목록을 sourceIndices에 할당
            sourceIndices = nonRareList.ToArray();
        }

        List<AbilityEnum> candidatePool = new List<AbilityEnum>(sourceIndices);
        for (int i = candidatePool.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            AbilityEnum temp = candidatePool[i];
            candidatePool[i] = candidatePool[randomIndex];
            candidatePool[randomIndex] = temp;
        }

        // 각 슬롯에 대해 능력을 선택 (중복 없이)
        for (int i = 0; i < selectedAbility.Length; i++)
        {
            bool candidateFound = false;
            AbilityEnum selectedCandidate = default;
            // 후보 풀에서 풀업그레이드가 아닌 능력을 찾아 선택
            for (int j = 0; j < candidatePool.Count; j++)
            {
                if (!gachaAbilityController.FullUpgrade(candidatePool[j]))
                {
                    selectedCandidate = candidatePool[j];
                    candidatePool.RemoveAt(j); // 선택한 후보는 중복 방지를 위해 제거
                    candidateFound = true;
                    break;
                }
            }
            // 만약 후보 풀에 남은 모든 능력이 이미 풀업그레이드라면,
            // 또는 후보 풀이 비어있다면, 필요에 따라 기본값 처리 또는 경고 출력
            if (!candidateFound)
            {
                if (candidatePool.Count > 0)
                {
                    selectedCandidate = candidatePool[0];
                    candidatePool.RemoveAt(0);
                }
                else
                {
                    continue;
                }
            }
            selectedAbility[i] = selectedCandidate;
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
    /// 확률에 따라 레어 능력을 선택//test용 15퍼
    /// </summary>
    public void IsRare()
    {
        if (UnityEngine.Random.Range(0, 100) <15)
            isRare = true;
    }

    /// <summary>
    /// 가챠에서 선택한 능력을 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public AbilityEnum[] GachaSelect()
    {
        return selectedAbility;
    }
}