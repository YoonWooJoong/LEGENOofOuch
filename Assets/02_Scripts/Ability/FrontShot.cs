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

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        gameManager = GameManager.Instance;
        projectileManager = GameManager.Instance.ProjectileManager;
        player = gameManager.player;
    }
    public override void UseSkill()
    {
        Vector3 lookDir = player.GetlookDir();
        PlayerClassEnum pClass = player.GetPlayerClass();
      /*  int wallCount = projectileManager.GetWallCount();
        int contactCount = projectileManager.GetContactCount();

        int arrowCount = isUpgraded ? (int)abilityData.values[0] : (int)abilityData.values[1];

        for (int i = 0; i < arrowCount; i++)
        {
            float posOffset = (i - (arrowCount - 1) / 2.0f) * offset; // ÁÂ¿ì ´ëÄª Á¤·Ä

            Vector3 spawnPos = GameManager.Instance.player.transform.position + Vector3.right * posOffset;

            GameManager.Instance.ProjectileManager.ShootPlayerProjectile(spawnPos, lookDir, pClass, wallCount, contactCount);
        }*/
    }
}