using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.ShaderData;
using static UnityEngine.Rendering.DebugUI;

public class FrontShot : AbilityBase
{
    GameManager gameManager;
    ProjectileManager projectileManager;
    PlayerCharacter player;
    private float offset = 0.5f;
    int arrowCount;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        gameManager = GameManager.Instance;
        projectileManager = GameManager.Instance.ProjectileManager;
        player = gameManager.player;

        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        // 화살 개수 설정
        arrowCount = isUpgraded ? (int)abilityData.values[1] : (int)abilityData.values[0];

        if (isUpgraded)
        {
            float value = (100 - 25) * 0.01f;
            projectileManager.SetFinalDecreaseDamage(value);
        }
    }

    public override void UseSkill()
    {
        Vector3 lookDir = player.LookDir;
        PlayerClassEnum pClass = player.GetPlayerClass();

        for (int i = 0; i < arrowCount; i++)
        {
            float posOffset = (i - (arrowCount - 1) / 2.0f) * offset; // 좌우 대칭 정렬

            Vector3 spawnPos = GameManager.Instance.player.transform.position + Vector3.right * posOffset;

            GameManager.Instance.ProjectileManager.ShootPlayerProjectile(spawnPos, lookDir, pClass);
        }
    }
}