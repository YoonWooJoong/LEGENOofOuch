using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private GameObject monsterProjectilePrefab;
    [SerializeField] private GameObject fairyPrefab;
    [SerializeField] private GameObject fireOrbPrefab;

    public Transform tras;

    /// <summary>
    /// 총알 생성, 리스트에 워리어, 위자드, 궁수 순서대로 프리팹 넣어야함
    /// </summary>
    /// <param name="startPosition">시작 위치</param>
    /// <param name="playerClass">플레이어 클래스</param>
    /// <param name="direction"></param>
    public void ShootPlayerProjectile(Vector3 startPosition, Vector3 direction, PlayerClassEnum playerClass, int contactWallCount, int contactEnemyCount)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);
        
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, contactWallCount,contactEnemyCount);
    }

    public void ShootEnemyProjectile(Vector3 startPosition, Vector3 direction)
    {
        GameObject origin = monsterProjectilePrefab;
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction);
    }

    public void CreateFireOrb(Vector3 playerPosition)
    {
        GameObject origin = fireOrbPrefab;
        GameObject obj1 = Instantiate(origin, playerPosition, Quaternion.identity);
        GameObject obj2 = Instantiate(origin, playerPosition, Quaternion.identity);

        SurroundController fireOrbController1 = obj1.GetComponent<SurroundController>();
        SurroundController fireOrbController2 = obj2.GetComponent<SurroundController>();
        fireOrbController1.Init(playerPosition, 0);
        fireOrbController2.Init(playerPosition, 180);
    }

    public void CreateFairy(Vector3 playerPosition)
    {
        GameObject origin = fairyPrefab;
        GameObject obj = Instantiate (origin, playerPosition, Quaternion.identity);

        SurroundController fairyController = obj.GetComponent<SurroundController>();
        fairyController.Init(playerPosition,270);
    }

    public void Start()
    {
        CreateFireOrb(tras.position);
        CreateFairy(tras.position);
    }
}
