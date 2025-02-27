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
    private float[] fireAngles = { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 }; // 전방향 각도
    private int attackCount = 0;  // 공격 횟수 추적

    protected override void Attack()
    {
        attackCount++; // 공격 횟수 증가

        if (attackCount >= attackThreshold)
        {
            FireInAllDirections(); // 전방향 공격 실행
            attackCount = 0; // 카운트 초기화
            return;
        }

        base.Attack();
    }

    private void FireInAllDirections()
    {
        foreach (float angle in fireAngles)
        {
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            GameManager.Instance.ProjectileManager.ShootEnemyProjectile(this.transform.position, direction, AttackPower);
        }
    }
}
