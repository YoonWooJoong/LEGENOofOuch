using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMapManager : MonoBehaviour
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
   [SerializeField] private GameObject[] selectedMapInstance = new GameObject[TotalMaps];// 선택된 맵의 인스턴스
    private StageContainer stageContainer;
    private const int TotalMaps = 15;
    private const int NormalMaps = 14;
    private const int devilround = 4;
    public int roundIndex = 0;
    public Transform playerSpawn;
    public Transform[] monsterSpawn;

    /// <summary>
    /// 스테이지에 해당하는 맵을 생성 
    /// </summary>
    public void SpawnRandomMap()
    {
        //StageEnum stage = GameManager.Instance.GetStage();
        StageEnum stage = StageEnum.Castle;
        // 랜덤한 맵 선택
        switch (stage)
        {
            case StageEnum.Castle:
                InstantiateMaps(stageCastlePrefabs,stagecastleDevilPrefabs,stagecastleBossPrefabs);
                break;
            case StageEnum.Swamp:
                InstantiateMaps(stageSwampPrefabs,stageSwampDevilPrefabs,stageSwampBossPrefabs);
                break;
            case StageEnum.Volcano:
                InstantiateMaps(stageVolcanicPrefabs,stageVolcanicDevilPrefabs,stageVolcanicBossPrefabs);
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
        selectedMapInstance[TotalMaps-1] = Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
        selectedMapInstance[TotalMaps - 1].SetActive(false);
    }

    public void SpawnEntity() 
    {
        SpawnPlayer();
        SpawnMonsters();
        GameManager.Instance.AbilityManager.SetAbility(AbilityEnum.FrontShot);
    }

    public void MapStart()
    {
        selectedMapInstance[0].SetActive(true);
        Debug.Log($"선택된 맵: {selectedMapInstance[0].name}");
        SetTransrate();
    }

    /// <summary>
    /// 클리어시 다음맵 활성화.
    /// 맵을 활성화하면서 스폰포인트 전달
    /// 스테이지 클리어시 모든 맵 삭제
    /// </summary>
    public void NextMap()
    {
        // 현재 맵 비활성화
        if (roundIndex >= 0 && roundIndex < selectedMapInstance.Length)
        {
            selectedMapInstance[roundIndex].SetActive(false);
        }

        roundIndex++;

        // 만약 마지막 맵에 도달하면 모든 맵을 삭제하고 스테이지 클리어
        if (roundIndex >= selectedMapInstance.Length)
        {
            foreach (GameObject map in selectedMapInstance)
            {
                Destroy(map);
            }
            // 스테이지 클리어
            return;
        }

        // 새로운 맵 활성화
        if (roundIndex < selectedMapInstance.Length)
        {
            selectedMapInstance[roundIndex].SetActive(true);
            SetTransrate();
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
    public void SpawnPlayer()
    {
        if (playerSpawn == null)
        {
            Debug.LogError("PlayerSpawn 위치가 설정되지 않았습니다!");
            return;
        }

        if (GameManager.Instance.player == null)
        {
            GameObject newPlayer = Instantiate(GameManager.Instance.playerPrefab, playerSpawn.position, playerSpawn.rotation);
            GameManager.Instance.player = newPlayer.GetComponent<PlayerCharacter>();


            if (GameManager.Instance.player != null)
            {
                Debug.Log("새로운 플레이어가 생성되었습니다.");
            }
            else
            {
                Debug.LogError("생성된 플레이어에 PlayerCharacter 컴포넌트가 없습니다!");
            }
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
        //여기에 스테이지 매니저에서 몬스터 마리수 정해줄것
        if (monsterSpawn == null || monsterSpawn.Length < 3)
        {
            Debug.LogError("몬스터 스폰 포인트가 부족합니다! 최소 3개 이상 필요합니다.");
            return;
        }

        // 랜덤한 3개의 스폰 위치 선택 (중복 없이)
        Transform[] selectedSpawns = GetRandomSpawnPoints(3);

        // 선택된 위치에 몬스터 생성
        foreach (Transform spawnPoint in selectedSpawns)
        {
            Debug.Log("몬스터 생성넘겨줌");
            GameManager.Instance.MonsterManager.Spawn(spawnPoint);
        }
    }

    /// <summary>
    /// 몬스터 스폰 포인트 중 랜덤한 포인트를 선택하여 반환
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private Transform[] GetRandomSpawnPoints(int count)
    {
        List<Transform> spawnList = new List<Transform>(monsterSpawn);
        Transform[] selected = new Transform[count];

        // Fisher-Yates 셔플을 사용하여 리스트 섞기
        for (int i = spawnList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = spawnList[i];
            spawnList[i] = spawnList[randomIndex];
            spawnList[randomIndex] = temp;
        }

        // 앞에서부터 `count`개 선택
        for (int i = 0; i < count; i++)
        {
            selected[i] = spawnList[i];
        }

        return selected;
    }
}


/////////////////////////*승규님 코드*/////////////////////////
//public Image SelectImage; // UI에 표시할 이미지
//public Text SelectName; // 맵 이름 표시
//public Sprite[] SelectImages; // 맵 이미지 배열
//public string[] SelectNames; // 맵 이름 배열
//public GameObject[] MapPrefabs; // 맵 프리팹 배열
//public Transform mapSpawnPoint;
//public Camera mainCamera;

//private int SelectIndex = 0; // 현재 선택된 맵의 인덱스
//private GameObject currentMapInstance; // 현재 활성화된 맵 프리팹

//void Start()
//{
//    LoadSelectedMap();
//}

//void UpdateStageUI()
//{
//    if (SelectImages.Length > 0 && SelectNames.Length > 0)
//    {
//        SelectImage.sprite = SelectImages[SelectIndex];
//        SelectName.text = SelectNames[SelectIndex];
//    }
//}

////void GameStart()

//void LoadSelectedMap()
//{
//    // 기존 맵 삭제
//    if (currentMapInstance != null)
//    {
//        Destroy(currentMapInstance);
//    }

//    if (MapPrefabs != null && SelectIndex < MapPrefabs.Length && MapPrefabs[SelectIndex] != null)
//    {
//        currentMapInstance = Instantiate(MapPrefabs[SelectIndex], Vector3.zero, Quaternion.identity);
//    }
//    else
//    {
//        Debug.LogError("맵 프리팹이 존재하지 않음: " + SelectNames[SelectIndex]);
//    }
//}
/////////////////////////*승규님 코드*/////////////////////////