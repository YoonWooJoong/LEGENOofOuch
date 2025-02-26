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
        float value = isUpgraded ? abilityData.values[1] : abilityData.values[0];
        //projectileManager.GetContactCount(value);  최종 공격력 낮추는 함수

    }
}