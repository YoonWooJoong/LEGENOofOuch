using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BloodThirst : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        // 최대 체력의 {n}% 만큼 회복
        float healPercentage = isUpgraded ? abilityData.values[1] : abilityData.values[0];
        GameManager.Instance.healReward = Mathf.RoundToInt(GameManager.Instance.player.MaxHp * (healPercentage * 0.01f));

        Debug.Log($"피의 갈증 활성화 {GameManager.Instance.healReward} 체력 회복");
    }
}