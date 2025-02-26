using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs; // 클래스별 투사체 프리팹
    [SerializeField] private GameObject monsterProjectilePrefab; // 몬스터별 투사체 프리팹
    [SerializeField] private GameObject fairyPrefab; // 요정 프리팹
    [SerializeField] private GameObject fireOrbPrefab; // 불의 원 프리팹
    [SerializeField] private GameObject fairyProjectilePrefab; // 요정 프리팹

    private int contactWallCount;
    private int contactEnemyCount;
    private float contactWallDecreaseDamage;
    private float contactEnemyDecreaseDamage;
    private float finalDecreaseDamage;
    private float darkTouchDecreaseDamage;
    private float blazeDecresaseDamage;
    private float fireOrbDecreaseDamage;
    private float fairyDecreaseDamage;
    private bool isDarkTouch = false;
    private bool isBlaze = false;
    private bool isDeathBoom = false;
    



    /// <summary>
    /// 총알 생성, 리스트에 워리어, 위자드, 궁수 순서대로 프리팹 넣어야함
    /// </summary>
    /// <param name="startPosition">시작 위치</param>
    /// <param name="playerClass">플레이어 클래스</param>
    /// <param name="direction"></param>
    public void ShootPlayerProjectile(Vector3 startPosition, Vector3 direction, PlayerClassEnum playerClass)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, isDarkTouch, isBlaze, GameManager.Instance.player.AttackPower, contactWallCount, contactEnemyCount);
    }

    /// <summary>
    /// Enemy 총알 생성
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="direction"></param>
    public void ShootEnemyProjectile(Vector3 startPosition, Vector3 direction)
    {
        GameObject origin = monsterProjectilePrefab;
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileEnemyController projectileEnemyController = obj.GetComponent<ProjectileEnemyController>();
        projectileEnemyController.Init(direction);
    }

    /// <summary>
    /// 스킬 불의 원 생성
    /// </summary>
    /// <param name="playerPosition"></param>
    public void CreateFireOrb(Vector3 playerPosition)
    {
        GameObject origin = fireOrbPrefab;
        GameObject obj1 = Instantiate(origin, playerPosition, Quaternion.identity);
        GameObject obj2 = Instantiate(origin, playerPosition, Quaternion.identity);

        SurroundController fireOrbController1 = obj1.GetComponent<SurroundController>();
        SurroundController fireOrbController2 = obj2.GetComponent<SurroundController>();
        fireOrbController1.Init(0);
        fireOrbController2.Init(180);
    }

    /// <summary>
    /// 스킬 정령 생성
    /// </summary>
    /// <param name="playerPosition"></param>
    public void CreateFairy(Vector3 playerPosition)
    {
        GameObject origin = fairyPrefab;
        GameObject obj = Instantiate(origin, playerPosition, Quaternion.identity);

        SurroundController fairyController = obj.GetComponent<SurroundController>();
        fairyController.Init(270);
    }

    /// <summary>
    /// 요정 공격
    /// </summary>
    /// <param name="fairyPosition">요정 위치</param>
    /// <param name="direction">표적과 요정의 위치 뺀값</param>
    public void ShootFairy(Vector3 fairyPosition, Vector3 direction)
    {
        GameObject origin = fairyProjectilePrefab;
        GameObject obj = Instantiate(origin, fairyPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, false, false, GameManager.Instance.player.AttackPower* fairyDecreaseDamage);
        
    }

    public GameObject ShootBigSwordAura(Vector3 startPosition, Vector3 direction, PlayerClassEnum playerClass)
    {
        GameObject origin = projectilePrefabs[Convert.ToInt32(playerClass)];
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, isDarkTouch, isBlaze, contactWallCount, contactEnemyCount);
        float x = obj.transform.localScale.x * 2; // 크기 100% 증가
        float y = obj.transform.localScale.y * 2;
        obj.transform.localScale = new Vector3(x, y, 1);

        return obj;
    }



    /// <summary>
    /// 아래 변수들을 가져오거나 내보내는 함수
    /// contactWallCount; // 벽 반동 횟수
    /// contactEnemyCount; // 적 관통 횟수
    /// contactWallDecreaseDamage; // 벽 반동시 데미지 감소량
    /// contactEnemyDecreaseDamage; // 적 관통시 데미지 감소량
    /// finalDecreaseDamage; // 최종데미지 감소량
    /// darkTouchDecrreaseDamage; // 어둠의 접촉 데미지 감소량
    /// isDeathBoom; // 데스붐 bool형
    /// BlazeDecresaseDamage; // 블레이즈 데미지 감소량
    /// fireOrbDecreaseDamage; // 불의원 데미지 감소량
    /// fairyDecreaseDamage // 정령 데미지 감소량
    /// </summary>
    /// <returns></returns>
    #region
    public int GetContactWallCount()
    {
        return contactWallCount;
    }

    public void SetContactWallCount(int count)
    {
        contactWallCount = count;
    }
    public int GetContactEnemyCount()
    {
        return contactEnemyCount;
    }

    public void SetContactEnemyCount(int count)
    {
        contactEnemyCount = count;
    }

    public float GetContactWallDecreaseDamage()
    {
        return contactWallDecreaseDamage;
    }

    public void SetContactWallDecreaseDamage(float Damage)
    {
        contactWallDecreaseDamage = Damage;
    }

    public float GetContactEnemyDecreaseDamage()
    {
        return contactEnemyDecreaseDamage;
    }

    public void SetContactEnemyDecreaseDamage(float Damage)
    {
        contactEnemyDecreaseDamage = Damage;
    }

    public float GetFinalDecreaseDamage()
    {
        return finalDecreaseDamage;
    }

    public void SetFinalDecreaseDamage(float Damage)
    {
        finalDecreaseDamage = Damage;
    }

    public float GetDarkTouchDecreaseDamage()
    {
        return darkTouchDecreaseDamage;
    }

    public void SetDarkTouchDecreaseDamage(float Damage)
    {
        darkTouchDecreaseDamage = Damage;
    }


    public float GetBlazeDecresaseDamage()
    {
        return blazeDecresaseDamage;
    }

    public void SetBlazeDecresaseDamage(float Damage)
    {
        blazeDecresaseDamage = Damage;
    }

    public float GetFireOrbDecreaseDamage()
    {
        return fireOrbDecreaseDamage;
    }

    public void SetFireOrbDecreaseDamage(float Damage)
    {
        fireOrbDecreaseDamage = Damage;
    }
    public float GetFairyDecreaseDamage()
    {
        return fairyDecreaseDamage;
    }

    public void SetFairyDecreaseDamage(float Damage)
    {
        fairyDecreaseDamage = Damage;
        
    }
    public bool GetDeathBoom()
    {
        return isDeathBoom;
    }
    public void SetDeathBoom(bool isBoom)
    {
        isDeathBoom = isBoom;
    }


    #endregion


}
