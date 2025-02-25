using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class HPBoost : AbilityBase
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
            player.MaxHpBuf -= abilityData.values[0];
        }
        float hpBoost = abilityData.values[isUpgraded ? 1 : 0] * 0.01f;
        player.MaxHpBuf += hpBoost;

        Debug.Log($"hp 부스트 {player.MaxHpBuf} 체력 증가");
    }
}