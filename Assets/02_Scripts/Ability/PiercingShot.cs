using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PiercingShot : AbilityBase
{
    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        GameManager.Instance.ProjectileManager.SetContactEnemyCount(1);

        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        float value = isUpgraded ? abilityData.values[1] : abilityData.values[0];

        value = (100 - value) * 0.01f;
        GameManager.Instance.ProjectileManager.SetContactEnemyDecreaseDamage(value);
    }
}