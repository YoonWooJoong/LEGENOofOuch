using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager_KGS : MonoBehaviour
{
    [SerializeField] PlayerCharacter player;
    [SerializeField] MonsterManager MM;

    [SerializeField] Rect[] spawnPoints;
    EnemyCharacter enemy;

    /// <summary>
    /// 좌클릭:적 생성, 스페이스:플레이어 2데미지, E:가장 먼저 생성된 적에게 2데미지
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MM.Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)]);
        if (Input.GetKeyDown(KeyCode.Space))
            player.ChangeHealth(-2);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (enemy == null)
                enemy = FindFirstObjectByType<EnemyCharacter>();
            enemy?.ChangeHealth(-2);
        }
    }
}
