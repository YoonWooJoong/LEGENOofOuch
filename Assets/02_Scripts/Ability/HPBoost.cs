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

        // 새로운 HP 버프 적용
        float previousMaxHp = player.MaxHp;  // 기존 최대 체력 저장
        float hpBoost = isUpgraded ? abilityData.values[1] : abilityData.values[0] * 0.01f;
        player.MaxHpBuf += hpBoost;
        float newMaxHp = player.MaxHp;  // 새로운 최대 체력 저장

        // 증가한 체력만큼 회복
        float hpIncrease = newMaxHp - previousMaxHp;
        player.ChangeHealth(hpIncrease);
        Debug.Log($"hp 부스트 {player.MaxHpBuf} 체력 증가");
    }
}