using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;

    /// <summary>
    /// 총알 생성, 리스트에 워리어, 위자드, 궁수 순서대로 프리팹 넣어야함
    /// </summary>
    /// <param name="startPosition">플레이어 위치</param>
    /// <param name="playerClass">플레이어 클래스</param>
    public void ShootProjectile(Vector2 startPosition, PlayerClassEnum playerClass)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);
    }
}
