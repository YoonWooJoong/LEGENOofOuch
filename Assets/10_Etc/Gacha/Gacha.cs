using System;
using UnityEngine;
using System.Collections.Generic;

public class Gacha : MonoBehaviour
{
    public int abilityindex = Enum.GetValues(typeof(AbilityEnum)).Length;
    private int[] selectedAbility = new int[3]; // 선택된 3개 능력 인덱스
    public bool isRare = false;

    public void SelectRandomAbility()
    {
        // Rare로 분류할 인덱스 집합
        int[] rareIndices = new int[] { 7, 8, 16, 19, 21, 22, 23 };
        int[] sourceIndices;
        IsRare();
        if (isRare)
        {
            sourceIndices = rareIndices;
        }
        else
        {
            List<int> nonRareList = new List<int>();
            for (int i = 0; i < abilityindex; i++)
            {
                if (Array.IndexOf(rareIndices, i) < 0)
                {
                    nonRareList.Add(i);
                }
            }
            sourceIndices = nonRareList.ToArray();
        }

        for (int i = sourceIndices.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = sourceIndices[i];
            sourceIndices[i] = sourceIndices[randomIndex];
            sourceIndices[randomIndex] = temp;
        }

        for (int i = 0; i < selectedAbility.Length; i++)
        {
            selectedAbility[i] = sourceIndices[i];
        }
    }

    public int[] GetSelectedAbility()
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