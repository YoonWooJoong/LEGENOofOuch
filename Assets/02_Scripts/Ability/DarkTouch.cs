using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DarkTouch : AbilityBase
{
    private GameManager gameManager;
    private ProjectileManager projectileManager;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        gameManager = GameManager.Instance;
        projectileManager = gameManager.ProjectileManager;

        projectileManager.SetDarkTouch(true);
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        float value = isUpgraded ? abilityData.values[1] : abilityData.values[0];
        value *= 0.01f;
        projectileManager.SetDarkTouchDecreaseDamage(value);
    }
}