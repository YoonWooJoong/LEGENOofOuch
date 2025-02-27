using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefebs;
    [SerializeField] GameObject[] bossPrefebs;
    public List<EnemyCharacter> spawnedEnemys = new();

    public bool ClearSpawn => spawnedEnemys.Count == 0;

    /// <summary>
    /// 호출되면 지정된 위치에서 무작위 적이 나타납니다.
    /// </summary>
    /// <param name="rect">적이 나타날 범위입니다.</param>
    public void Spawn(Transform spawnPoint)
    {
        GameObject randomPrefeb = monsterPrefebs[Random.Range(0, monsterPrefebs.Length)];

        MonsterSpawn(spawnPoint, randomPrefeb);
    }

    /// <summary>
    /// 보스몬스터를 지정된 위치에 소환합니다.
    /// </summary>
    /// <param name="spawnPoint"></param>
    public void BossSpawn(Transform spawnPoint)
    {
        GameObject randomPrefeb = bossPrefebs[Random.Range(0, monsterPrefebs.Length)];

        MonsterSpawn(spawnPoint, randomPrefeb);
    }

    void MonsterSpawn(Transform spawnPoint, GameObject randomPrefeb)
    {
        //스폰포인트를 받아서 스폰
        GameObject spawned = Instantiate(randomPrefeb, spawnPoint.position, spawnPoint.rotation);
        EnemyCharacter enemyCharacter = spawned.GetComponent<EnemyCharacter>();

        spawnedEnemys.Add(enemyCharacter);
    }

    public void ClearSpawns()
    {
        while (spawnedEnemys.Count > 0)
        {
            var enemy = spawnedEnemys[0];
            spawnedEnemys.Remove(enemy);
            Destroy(enemy.gameObject);
        }
    }

    /// <summary>
    /// 필드의 적이 죽었음을 반영하는 메서드입니다. EnemyCharacter의 Death에서 호출합니다.
    /// </summary>
    /// <param name="enemy">죽은 적입니다. 즉 자기 자신입니다.</param>
    public void RemoveEnemyOnDeath(EnemyCharacter enemy)
    {
        spawnedEnemys.Remove(enemy);
    }
}
