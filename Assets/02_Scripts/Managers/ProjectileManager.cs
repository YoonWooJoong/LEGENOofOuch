using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private GameObject monsterProjectilePrefabs;

    /// <summary>
    /// 총알 생성, 리스트에 워리어, 위자드, 궁수 순서대로 프리팹 넣어야함
    /// </summary>
    /// <param name="startPosition">시작 위치</param>
    /// <param name="playerClass">플레이어 클래스</param>
    /// <param name="direction"></param>
    public void ShootPlayerProjectile(Vector2 startPosition, Vector2 direction, PlayerClassEnum playerClass, int contactWallCount, int contactEnemyCount)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);
        
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, contactWallCount,contactEnemyCount);
    }

    public void ShootEnemyProjectile(Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = monsterProjectilePrefabs;
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction);
    }
}
