using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class AttackSpeedBoost : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        PlayerCharacter player = GameManager.Instance.player;
        if (player == null) return;

        if (isUpgraded)
        {
            player.AsBuf -= abilityData.values[0];
        }
        float attackSpeedBoost = abilityData.values[isUpgraded ? 1 : 0] * 0.01f;
        player.AsBuf += attackSpeedBoost;

        Debug.Log($"공격속도 부스트 {player.AsBuf} 공속 증가");
    }
}