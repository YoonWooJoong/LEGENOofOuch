using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPrefebs;
    List<EnemyCharacter> spawnedEnemys = new();

    public void Spawn(Rect rect)
    {
        GameObject randomPrefeb = monsterPrefebs[Random.Range(0, monsterPrefebs.Length)];

        Vector2 randomP = new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));

        GameObject spawned = Instantiate(randomPrefeb, new Vector3(randomP.x, randomP.y), Quaternion.identity);
        EnemyCharacter enemyCharacter = spawned.GetComponent<EnemyCharacter>();

        spawnedEnemys.Add(enemyCharacter);
    }
}
