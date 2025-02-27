using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Mage : AbilityBase
{
    private GameManager gameManager;
    private PlayerCharacter player;
    private MonsterManager monsterManager;
    private Coroutine skillCoroutine;
    private float damageMultiplier; // {0}% 데미지 배율
    public GameObject lightningEffectPrefab; // 번개 파티클 프리팹

    public override void Init(AbilityDataSO abilityDataSO)
    {
        base.Init(abilityDataSO);

        gameManager = GameManager.Instance;
        player = gameManager.player;
        monsterManager = gameManager.MonsterManager;

        UpdateAbility();

        if (skillCoroutine != null)
        {
            StopCoroutine(skillCoroutine);
        }
        skillCoroutine = StartCoroutine(AutoLightningStrike());
    }

    protected override void UpdateAbility()
    {
        damageMultiplier = (isUpgraded ? abilityData.values[1] : abilityData.values[0]) * 0.01f;
    }

    private IEnumerator AutoLightningStrike()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (monsterManager.spawnedEnemys.Count == 0) continue;

            // 랜덤한 적 선택
            int randomIndex = Random.Range(0, monsterManager.spawnedEnemys.Count);
            EnemyCharacter targetEnemy = monsterManager.spawnedEnemys[randomIndex];

            if (targetEnemy == null || targetEnemy.GetCurHp() <= 0) continue;

            // 데미지 적용
            float lightningDamage = player.AttackPower * damageMultiplier;
            targetEnemy.ChangeHealth(-lightningDamage);

            // 번개 파티클 생성
            if (lightningEffectPrefab != null)
            {
                GameObject effect = Instantiate(lightningEffectPrefab, targetEnemy.transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
                Destroy(effect, 1f); // 1초 후 파티클 삭제
            }
        }
    }
}