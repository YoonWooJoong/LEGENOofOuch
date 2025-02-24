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
    public void SelectRandomAbility()
    {
        // Rare로 분류할 인덱스 집합
        AbilityEnum[] rareIndices = new AbilityEnum[] { AbilityEnum.BloodThirst, AbilityEnum.Invincibility, AbilityEnum.Blaze, AbilityEnum.Spirit, AbilityEnum.Archer, AbilityEnum.Mage, AbilityEnum.Warrior };
        AbilityEnum[] sourceIndices;
        IsRare();
        if (isRare)
        {
            sourceIndices = rareIndices;
        }
        else
        {
            List<AbilityEnum> nonRareList = new List<AbilityEnum>();
            for (int i = 0; i < abilityindex; i++)
            {
                if (Array.IndexOf(rareIndices, i) < 0)
                {
                    nonRareList.Add((AbilityEnum)i);
                }
            }
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
        }
    }

    public AbilityEnum[] GetSelectedAbility()
    {
        return selectedAbility;
    }
    public bool GetIsRare()
    {
        return isRare;
    }
    public void IsRare()
    {
        if (UnityEngine.Random.Range(0, 100) < 5)
            isRare = true;
    }
}