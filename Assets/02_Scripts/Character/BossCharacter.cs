using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BossCharacter : EnemyCharacter
{
    [SerializeField] bool isBoss;
    [SerializeField] private int attackThreshold;  // {0}회마다 발사할 기준
    private int attackCount = 0;  // 공격 횟수 추적

    protected override void Attack()
    {
        attackCount++; // 공격 횟수 증가

        if (attackCount >= attackThreshold)
        {
            animHandle.Attack(AttackSpeed);
            FireInAllDirections(); // 전방향 공격 실행
            attackCount = 0; // 카운트 초기화
            return;
        }

        base.Attack();
    }

    private void FireInAllDirections()
    {
        for (float angle = 0; angle < 360; angle += 30)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            GameManager.Instance.ProjectileManager.ShootEnemyProjectile(this.transform.position, direction, AttackPower);
        }
    }
}
