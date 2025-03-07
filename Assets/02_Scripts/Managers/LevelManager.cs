using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public GameObject[] stageCastlePrefabs;
    public GameObject stagecastleBossPrefabs;
    public GameObject stagecastleDevilPrefabs;
    public GameObject[] stageSwampPrefabs;
    public GameObject stageSwampBossPrefabs;
    public GameObject stageSwampDevilPrefabs;
    public GameObject[] stageVolcanicPrefabs;
    public GameObject stageVolcanicBossPrefabs;
    public GameObject stageVolcanicDevilPrefabs;
    public GameObject TradeUI;
    [SerializeField] private GameObject[] selectedMapInstance = new GameObject[TotalMaps];// 선택된 맵의 인스턴스
    private StageContainer stageContainer;
    private const int TotalMaps = 15;
    private const int NormalMaps = 14;
    private const int devilround = 4;
    public GameObject devilPrefab;
    public DevilInteraction devil;
    public int roundIndex = 0;
    public Transform playerSpawn;
    public Transform[] monsterSpawn;
    private StageEnum stageEnum;

    /// <summary>
    /// 스테이지에 해당하는 맵을 생성 
    /// </summary>
    public void SpawnRandomMap(StageEnum stage)
    {
        stageEnum = stage;
        roundIndex = 0;
        // 랜덤한 맵 선택
        switch (stage)
        {
            case StageEnum.Castle:
                InstantiateMaps(stageCastlePrefabs, stagecastleDevilPrefabs, stagecastleBossPrefabs);
                break;
            case StageEnum.Swamp:
                InstantiateMaps(stageSwampPrefabs, stageSwampDevilPrefabs, stageSwampBossPrefabs);
                break;
            case StageEnum.Volcano:
                InstantiateMaps(stageVolcanicPrefabs, stageVolcanicDevilPrefabs, stageVolcanicBossPrefabs);
                break;
        }
        MapStart();
        // 생성된 맵에서 플레이어 스폰 포인트 찾기
        Transform playerSpawnPoint = stageContainer.playerSpawnPoint;
        Transform[] monsterSpawnPoint = stageContainer.enemySpawnPoint;

        GetTransrate(playerSpawnPoint, monsterSpawnPoint);
    }

    /// <summary>
    /// 맵을 생성하여 배열에 집어넣는다.
    /// </summary>
    /// <param name="mapPrefabs"></param>
    private void InstantiateMaps(GameObject[] mapPrefabs, GameObject devilPrefab, GameObject bossPrefab)
    {
        for (int i = 0; i < NormalMaps; i++)
        {
            if (i == devilround)
            {
                selectedMapInstance[i] = Instantiate(devilPrefab, Vector3.zero, Quaternion.identity);
                selectedMapInstance[i].SetActive(false);
                continue;
            }
            GameObject selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)];
            selectedMapInstance[i] = Instantiate(selectedMap, Vector3.zero, Quaternion.identity);
            selectedMapInstance[i].SetActive(false);
        }
        selectedMapInstance[TotalMaps - 1] = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
        selectedMapInstance[TotalMaps - 1].SetActive(false);
    }

    /// <summary>
    /// 게임시작시 첫번째 맵을 활성화
    /// </summary>
    public void MapStart()
    {
        selectedMapInstance[0].SetActive(true);
        SetTransrate();
    }
    /// <summary>
    /// 클리어시 다음맵 활성화.
    /// 맵을 활성화하면서 스폰포인트 전달
    /// 스테이지 클리어시 모든 맵 삭제
    /// </summary>
    public void NextMap()
    {
        //필드에 나와있는 모든 물약 삭제
        ClearPotion();

        // 보스 몹 제외 현재 맵 비활성화
        if (roundIndex >= 0 && roundIndex < selectedMapInstance.Length - 1)
        {
            selectedMapInstance[roundIndex].SetActive(false);
        }

        roundIndex++;

        // 만약 마지막 맵에 도달하면 모든 맵을 삭제하고 스테이지 클리어
        if (roundIndex >= selectedMapInstance.Length)
        {
            GameManager.Instance.EndGame();
            switch (stageEnum)
            {
                case StageEnum.Castle:
                    Achievements.TriggerFirstCastleClear();
                    break;
                case StageEnum.Swamp:
                    Achievements.TriggerFirstSwampClear();
                    break;
                case StageEnum.Volcano:
                    Achievements.TriggerFirstVolcanoClear();
                    break;
            }
            roundIndex = 0;
            // 스테이지 클리어
            return;
        }
        if (roundIndex == devilround)
        {
            selectedMapInstance[roundIndex].SetActive(true);
            SetTransrate();
            SpawnPlayer(GameManager.Instance.playerClassEnum);
            SpawnDevil();
            return;
        }
        // 새로운 맵 활성화
        if (roundIndex < selectedMapInstance.Length)
        {
            if (devil != null)
            {
                Destroy(devil.gameObject);
            }
            selectedMapInstance[roundIndex].SetActive(true);
            SetTransrate();
            SpawnEntity(GameManager.Instance.playerClassEnum);
        }
    }


    /// <summary>
    /// 맵의 플레이어와 몬스터의 스폰 포인트를 게임메니저에게 전달
    /// </summary>
    public void SetTransrate()
    {
        stageContainer = selectedMapInstance[roundIndex].GetComponent<StageContainer>();
        Transform playerSpawnPoint = stageContainer.playerSpawnPoint;
        Transform[] monsterSpawnPoint = stageContainer.enemySpawnPoint;
        GetTransrate(playerSpawnPoint, monsterSpawnPoint);
    }

    /// <summary>
    /// 소환포인트를 받아오는 함수
    /// </summary>
    /// <param name="_playerSpawn"></param>
    /// <param name="_monsterSpawn"></param>
    public void GetTransrate(Transform _playerSpawn, Transform[] _monsterSpawn)
    {
        playerSpawn = _playerSpawn;
        monsterSpawn = _monsterSpawn;
    }

    /// <summary>
    /// 플레이어를 생성, 이동시키는 함수
    /// 맵 이동시 플레이어 위치를 바꾸어준다.
    /// </summary>
    public void SpawnPlayer(PlayerClassEnum playerClassEnum)
    {
        if (playerSpawn == null)
        {
            return;
        }

        if (GameManager.Instance.player == null)
        {
            GameObject newPlayer = Instantiate(GameManager.Instance.playerPrefab, playerSpawn.position, playerSpawn.rotation);
            GameManager.Instance.player = newPlayer.GetComponent<PlayerCharacter>();
            GameManager.Instance.player.SetClass(playerClassEnum);

            GameManager.Instance.AbilityManager.SetAbility(AbilityEnum.FrontShot);
        }
        else
        {
            GameManager.Instance.player.transform.position = playerSpawn.position;
        }
    }

    /// <summary>
    /// 몬스터를 생성하는 함수
    /// 스폰포인트중 랜덤한포인트에 몬스터 생성
    /// </summary>
    public void SpawnMonsters()
    {
        GameManager.Instance.MonsterManager.ClearSpawns();

        //보스스테이이면 보스몹 소환하고 종료
        if (roundIndex == TotalMaps - 1 && monsterSpawn.Length >= 1)
        {
            GameManager.Instance.MonsterManager.BossSpawn(monsterSpawn[0]);
            return;
        }

        if (monsterSpawn == null || monsterSpawn.Length < 3)
        {
            Debug.LogError("몬스터 스폰 포인트가 부족합니다! 최소 3개 이상 필요합니다.");
            return;
        }

        // 랜덤한 3개의 스폰 위치 선택 (중복 없이)
        Transform[] selectedSpawns = GetRandomSpawnPoints(roundIndex + 2);

        // 선택된 위치에 몬스터 생성
        foreach (Transform spawnPoint in selectedSpawns)
        {
            GameManager.Instance.MonsterManager.Spawn(spawnPoint);
        }
    }

    /// <summary>
    /// 몬스터 스폰 포인트 중 랜덤한 포인트를 선택하여 반환
    /// </summary>
    /// <param name="count">몇마리의 몬스터 소환할지</param>
    /// <returns></returns>
    private Transform[] GetRandomSpawnPoints(int count)
    {
        List<Transform> spawnList = new List<Transform>(monsterSpawn);
        Transform[] selected = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            // spawnList에서 임의의 위치를 선택하여 selected 배열에 저장
            int randomIndex = Random.Range(0, spawnList.Count);
            selected[i] = spawnList[randomIndex];
        }

        return selected;
    }

    /// <summary>
    /// 악마를 생성하는 함수
    /// </summary>
    public void SpawnDevil()
    {

        if (devil == null)
        {
            GameObject newDevil = Instantiate(devilPrefab, monsterSpawn[0].position, monsterSpawn[0].rotation);
            devil = newDevil.GetComponent<DevilInteraction>();
            TradeUI.gameObject.SetActive(true);
            devil.tradeUI = TradeUI;
            TradeUI.gameObject.SetActive(false);
        }
        else
        {
            devil.transform.position = monsterSpawn[0].position;
        }
    }
    /// <summary>
    /// 플레이어와 몬스터를 생성하는 함수
    /// </summary>
    /// <param name="playerClassEnum"></param>
    public void SpawnEntity(PlayerClassEnum playerClassEnum)
    {
        SpawnPlayer(playerClassEnum);
        SpawnMonsters();
    }

    /// <summary>
    /// 맵을 삭제하는 함수
    /// 게임종료시 호출
    /// </summary> 
    public void DestroyMap()
    {
        foreach (GameObject map in selectedMapInstance)
        {
            Destroy(map);
        }
    }

    /// <summary>
    /// 맵에 남아있는 물약을 삭제합니다.
    /// </summary>
    public void ClearPotion()
    {
        var potions = FindObjectsOfType<HpPotion>();
        foreach (var potion in potions)
            Destroy(potion.gameObject);
    }
    public int NowRound()
    {
        return roundIndex;
    }
}