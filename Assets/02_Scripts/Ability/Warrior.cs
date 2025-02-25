using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Warrior : AbilityBase
{
    private float skillCooldown = 5f; // 5초마다 실행
    private float sizeMultiplier; // {0}% 증가 값
    private Coroutine skillCoroutine; // 스킬 실행 코루틴

    private GameManager gameManager;
    private ProjectileManager projectileManager;
    private PlayerCharacter player;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        gameManager = GameManager.Instance;
        projectileManager = gameManager.ProjectileManager;
        player = gameManager.player;

        UpdateAbility();

        if (skillCoroutine != null)
        {
            StopCoroutine(skillCoroutine);
        }
        skillCoroutine = StartCoroutine(AutoShootProjectile());
    }

    protected override void UpdateAbility()
    {
        sizeMultiplier = isUpgraded ? abilityData.values[1] : abilityData.values[0];
    }

    private IEnumerator AutoShootProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(skillCooldown);

            Vector3 playerPos = player.transform.position;
            Vector3 lookDir = player.GetlookDir();
            PlayerClassEnum pClass = player.GetPlayerClass();
           /* int wallCount = projectileManager.GetWallCount();
            int contactCount = projectileManager.GetContactCount();

            Vector3 projectileScale = Vector3.one * sizeMultiplier; // 크기 적용

            // 거대한 검기 발사
           GameObject obj = projectileManager.ShootPlayerProjectile(playerPos, lookDir, pClass, wallCount, contactCount);
            obj.transform.localScale = projectileScale;*/
        }
    }
}