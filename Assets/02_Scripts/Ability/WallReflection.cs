using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class WallReflection : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        // 반사 횟수 카운트 증가
      //  GameManager.Instance.ProjectileManager.SetWallReflectCount((int)value);

        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        float value = isUpgraded ? abilityData.values[1] : abilityData.values[0];
        
        // 피해량 감소
      //  GameManager.Instance.ProjectileManager.SetWallReflectDamage((int)value);
    }
}