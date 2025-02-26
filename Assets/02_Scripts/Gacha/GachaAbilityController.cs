using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GachaAbilityController : MonoBehaviour
{

    private int[] upgradeCounts;

    private void Awake()
    {
        int count = Enum.GetValues(typeof(AbilityEnum)).Length;
        upgradeCounts = new int[count];
    }

    /// <summary>
    /// 지정한 능력의 업그레이드 횟수를 1 증가시킵니다.
    /// </summary>
    /// <param name="ability">업그레이드할 능력</param>
    public void UpgradeAbility(AbilityEnum ability)
    {
        upgradeCounts[(int)ability]++;
    }

    /// <summary>
    /// 지정한 능력의 현재 업그레이드 횟수를 반환합니다.
    /// </summary>
    /// <param name="ability">조회할 능력</param>
    /// <returns>업그레이드 횟수</returns>
    public int GetUpgradeCount(AbilityEnum ability)
    {
        return upgradeCounts[(int)ability];
    }

    public bool FullUpgrade(AbilityEnum ability)
    {
        if (upgradeCounts[(int)ability] == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ClearUpgradeCount()
    {
        for (int i = 0; i < upgradeCounts.Length; i++)
        {
            upgradeCounts[i] = 0;
        }
    }
}