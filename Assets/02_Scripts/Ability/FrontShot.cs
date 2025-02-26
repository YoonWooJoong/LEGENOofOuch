using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.ShaderData;

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

        //projectileManager.GetContactCount();  최종 공격력 낮추는 함수 25%
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        // 화살 개수 설정
        arrowCount = isUpgraded ? (int)abilityData.values[0] : (int)abilityData.values[1];
    }
    public override void UseSkill()
    {
        Vector3 lookDir = player.GetlookDir();
        PlayerClassEnum pClass = player.GetPlayerClass();
      /*  int wallCount = projectileManager.GetWallCount();
        int contactCount = projectileManager.GetContactCount();

        for (int i = 0; i < arrowCount; i++)
        {
            float posOffset = (i - (arrowCount - 1) / 2.0f) * offset; // 좌우 대칭 정렬

            Vector3 spawnPos = GameManager.Instance.player.transform.position + Vector3.right * posOffset;

            GameManager.Instance.ProjectileManager.ShootPlayerProjectile(spawnPos, lookDir, pClass, wallCount, contactCount);
        }*/
    }
}