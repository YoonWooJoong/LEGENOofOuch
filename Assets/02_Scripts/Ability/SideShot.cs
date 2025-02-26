using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SideShot : AbilityBase
{
    private GameManager gameManager;
    private ProjectileManager projectileManager;
    private PlayerCharacter player;
    private float offset = 0.5f; // 화살 간격
    int arrowCount;

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);
        gameManager = GameManager.Instance;
        projectileManager = gameManager.ProjectileManager;
        player = gameManager.player;
        UpdateAbility();
    }

    protected override void UpdateAbility()
    {
        // 화살 개수 설정 (업그레이드 전: 좌우 1개씩, 업그레이드 후: 좌우 2개씩)
        arrowCount = isUpgraded ? 2 : 1;
    }

    public override void UseSkill()
    {
        PlayerClassEnum pClass = player.GetPlayerClass();

        // 캐릭터가 바라보는 방향 벡터
        Vector3 lookDir = player.GetlookDir().normalized;

        // lookDir 기준으로 오른쪽 방향 벡터 구하기 (법선 벡터 이용)
        Vector3 rightDir = Vector3.Cross(Vector3.forward, lookDir).normalized;
        Vector3 leftDir = -rightDir;

        for (int i = 0; i < arrowCount; i++)
        {
            // 대칭된 위치 계산
            float posOffset = (i - (arrowCount - 1) / 2.0f) * offset;
            Vector3 leftSpawnPos = player.transform.position + rightDir * posOffset;
            Vector3 rightSpawnPos = player.transform.position - rightDir * posOffset;

            projectileManager.ShootPlayerProjectile(leftSpawnPos, leftDir, pClass);
            projectileManager.ShootPlayerProjectile(rightSpawnPos, rightDir, pClass);
        }
    }
}