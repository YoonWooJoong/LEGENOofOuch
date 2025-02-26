using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // 완성되면 각각 주석 해제
    // Player player;
    // Battel battle;
    // PlayerClassEnum chooseplayerClass;
    // StageEnum chooseStage;

    [field: SerializeField] public AbilityManager AbilityManager { get; private set; }
    [field: SerializeField] public UIManager UIManager { get; private set; }
    [field: SerializeField] public ProjectileManager ProjectileManager { get; set; }
    [field: SerializeField] public SelectManager SelectManager { get; private set; }
    [field: SerializeField] public TileMapManager TileMapManager { get; private set; }
    [field: SerializeField] public MonsterManager MonsterManager { get; private set; }
    [field: SerializeField] public GachaManager GachaManager { get; private set; }

    public PlayerClassEnum playerClassEnum;
    public StageEnum stageEnum;
    public Test test;
    //public Transform playerSpawn;
    //public Transform[] monsterSpawn;

    public GameObject playerPrefab;
    public PlayerCharacter player;
    public int healReward = 0;

    private void Awake()
    {


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        Initialized();
    }
    /// <summary>
    /// 게임 시작
    /// 게임 시작에 관련된 함수들 호출
    /// 맵생성과 플레이어생성 , 몬스터생성
    /// </summary>
    public void StartGame()
    {
        
        playerClassEnum = SelectManager.GetSelectedCharacter();
        stageEnum = SelectManager.GetSelectedStageIndex();
        TileMapManager.SpawnRandomMap(stageEnum);
        TileMapManager.MapStart();
        TileMapManager.SpawnEntity(playerClassEnum);
        Debug.Log("StartGame");
        //SpawnPlayer();
        //SpawnMonsters();
        //적생성
        //플레이어생성
        //스테이지선택
        //

    }
    /// <summary>
    /// 몬스터를 죽였을때 호출되는 함수
    /// </summary>
    /// <param name="enemy"></param>
    public void KillMonster(EnemyCharacter enemy)
    {
        MonsterManager.RemoveEnemyOnDeath(enemy);
        //플레이어에게 경험치를 제공합니다.
        player.GetExp(17);
        //체력회복 스킬이 있으면 그 수치만큼 체력을 회복시켜줍니다.
        player.ChangeHealth(healReward);
        Debug.Log("KillMonster");
        if (MonsterManager.ClearSpawn)
        {
            //GachaManager.StartGacha();
        }
    }

    /// <summary>
    /// 가챠에서 선택한 능력을 어빌리티매니저에게 전달
    /// </summary>
    /// <param name="abilityEnum"></param>
    public void GetAbility(AbilityEnum abilityEnum)
    {
        AbilityManager.SetAbility(abilityEnum);
    }

    /// <summary>
    /// 어빌리티 매니저에서 스킬정보를 받아와서 UI에 전달
    /// </summary>
    public void SetAbilityText()
    {
        AbilityEnum[] selectedAbility = GachaManager.gacha.GetSelectedAbility();
        string[] abilityName = new string[3];
        string[] abilityDescription = new string[3];

        for (int i = 0; i < selectedAbility.Length; i++)
        {

            AbilityDataSO abilityData = AbilityManager.FindAbilityData(selectedAbility[i]);
            abilityName[i] = abilityData.AbilityName;
            int upgradeCount = GachaManager.gacha.gachaAbilityController.GetUpgradeCount(selectedAbility[i]);
            if (upgradeCount > 0)
            {
                abilityName[i] += $"\n<color=yellow>+{upgradeCount}</color>";
            }
            abilityDescription[i] = abilityData.Description.Replace("{0}", abilityData.Values[upgradeCount].ToString());
        }
        GachaManager.GetAbilityName(abilityName);
        GachaManager.GetAbilitydescription(abilityDescription);
    }
    public void GoNextMap()
    {
        if (MonsterManager.ClearSpawn)    
            TileMapManager.NextMap();
    }

    public void Initialized()
    {
        //프로젝타일 매니저 init
        AbilityManager.ClearOwnedAbilities();
        ProjectileManager.ClearProjectile();
        GachaManager.gacha.gachaAbilityController.ClearUpgradeCount();
        //플레이어 init
        player.ClearPlayerBuf();
    }
}
