using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MultiShot : AbilityBase
{
    GameManager gameManager;
    ProjectileManager projectileManager;
    PlayerCharacter player;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        gameManager = GameManager.Instance;
        projectileManager = GameManager.Instance.ProjectileManager;
        player = gameManager.player;
        player.AsBuf -= 0.15f;
        gameManager.AbilityManager.SetMultiShotOn(true);

        UpdateAbility();
    }
    protected override void UpdateAbility()
    {
        float value;
        if (isUpgraded)
        {
            value = abilityData.values[1];

            float value2 = abilityData.values[0];
            value2 = (100 - value2) * 0.01f;
            projectileManager.SetFinalDecreaseDamage(-value2);
        }
        else
        {
            value = abilityData.values[0];
        }


      //  float value = isUpgraded ? abilityData.values[1] : abilityData.values[0];

        value = (100 - value) * 0.01f;
        projectileManager.SetFinalDecreaseDamage(value);

    }
}