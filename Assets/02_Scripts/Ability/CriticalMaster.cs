using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class CriticalMaster : AbilityBase
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
            player.CriChanceBuf -= abilityData.values[0];
        }
        float criChanceBoost = isUpgraded ? abilityData.values[1] : abilityData.values[0] * 0.01f;
        player.CriChanceBuf += criChanceBoost;
        player.CriDmgBuf = 0.4f;

        Debug.Log($"크리티컬 마스터 {player.AtkBuf} 확률 증가 및 크리 뎀지 증가 {player.CriDmgBuf}");
    }
}