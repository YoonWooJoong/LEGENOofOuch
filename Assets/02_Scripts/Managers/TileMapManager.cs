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
   [SerializeField] private GameObject[] selectedMapInstance = new GameObject[TotalMaps];// ���õ� ���� �ν��Ͻ�
    private StageContainer stageContainer;
    private const int TotalMaps = 15;
    private const int NormalMaps = 14;
    private const int devilround = 4;
    public int roundIndex = 0;
    public Transform playerSpawn;
    public Transform[] monsterSpawn;

    /// <summary>
    /// ���������� �ش��ϴ� ���� ���� 
    /// </summary>
    public void SpawnRandomMap()
    {
        //StageEnum stage = GameManager.Instance.GetStage();
        StageEnum stage = StageEnum.Castle;
        // ������ �� ����
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
        // ������ �ʿ��� �÷��̾� ���� ����Ʈ ã��
        Transform playerSpawnPoint = stageContainer.playerSpawnPoint;
        Transform[] monsterSpawnPoint = stageContainer.enemySpawnPoint;

        GetTransrate(playerSpawnPoint, monsterSpawnPoint);
    }

    /// <summary>
    /// ���� �����Ͽ� �迭�� ����ִ´�.
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
        Debug.Log($"���õ� ��: {selectedMapInstance[0].name}");
        SetTransrate();
    }

    /// <summary>
    /// Ŭ����� ������ Ȱ��ȭ.
    /// ���� Ȱ��ȭ�ϸ鼭 ��������Ʈ ����
    /// �������� Ŭ����� ��� �� ����
    /// </summary>
    public void NextMap()
    {
        // ���� �� ��Ȱ��ȭ
        if (roundIndex >= 0 && roundIndex < selectedMapInstance.Length)
        {
            selectedMapInstance[roundIndex].SetActive(false);
        }

        roundIndex++;

        // ���� ������ �ʿ� �����ϸ� ��� ���� �����ϰ� �������� Ŭ����
        if (roundIndex >= selectedMapInstance.Length)
        {
            foreach (GameObject map in selectedMapInstance)
            {
                Destroy(map);
            }
            // �������� Ŭ����
            return;
        }
        if (roundIndex == devilround)
        {
            selectedMapInstance[roundIndex].SetActive(true);
            SetTransrate();
            SpawnPlayer();
            return;
        }
        // ���ο� �� Ȱ��ȭ
        if (roundIndex < selectedMapInstance.Length)
        {
            selectedMapInstance[roundIndex].SetActive(true);
            SetTransrate();
            SpawnEntity();
        }
    }


    /// <summary>
    /// ���� �÷��̾�� ������ ���� ����Ʈ�� ���Ӹ޴������� ����
    /// </summary>
    public void SetTransrate()
    {
        stageContainer = selectedMapInstance[roundIndex].GetComponent<StageContainer>();
        Transform playerSpawnPoint = stageContainer.playerSpawnPoint;
        Transform[] monsterSpawnPoint = stageContainer.enemySpawnPoint;
        GetTransrate(playerSpawnPoint, monsterSpawnPoint);
    }

    /// <summary>
    /// ��ȯ����Ʈ�� �޾ƿ��� �Լ�
    /// </summary>
    /// <param name="_playerSpawn"></param>
    /// <param name="_monsterSpawn"></param>
    public void GetTransrate(Transform _playerSpawn, Transform[] _monsterSpawn)
    {
        playerSpawn = _playerSpawn;
        monsterSpawn = _monsterSpawn;
    }

    /// <summary>
    /// �÷��̾ ����, �̵���Ű�� �Լ�
    /// �� �̵��� �÷��̾� ��ġ�� �ٲپ��ش�.
    /// </summary>
    public void SpawnPlayer()
    {
        if (playerSpawn == null)
        {
            Debug.LogError("PlayerSpawn ��ġ�� �������� �ʾҽ��ϴ�!");
            return;
        }

        if (GameManager.Instance.player == null)
        {
            GameObject newPlayer = Instantiate(GameManager.Instance.playerPrefab, playerSpawn.position, playerSpawn.rotation);
            GameManager.Instance.player = newPlayer.GetComponent<PlayerCharacter>();


            if (GameManager.Instance.player != null)
            {
                Debug.Log("���ο� �÷��̾ �����Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogError("������ �÷��̾ PlayerCharacter ������Ʈ�� �����ϴ�!");
            }
        }
        else
        {
            GameManager.Instance.player.transform.position = playerSpawn.position;
        }
    }

    /// <summary>
    /// ���͸� �����ϴ� �Լ�
    /// ��������Ʈ�� ����������Ʈ�� ���� ����
    /// </summary>
    public void SpawnMonsters()
    {
        //���⿡ �������� �Ŵ������� ���� ������ �����ٰ�
        if (monsterSpawn == null || monsterSpawn.Length < 3)
        {
            Debug.LogError("���� ���� ����Ʈ�� �����մϴ�! �ּ� 3�� �̻� �ʿ��մϴ�.");
            return;
        }

        // ������ 3���� ���� ��ġ ���� (�ߺ� ����)
        Transform[] selectedSpawns = GetRandomSpawnPoints(3);

        // ���õ� ��ġ�� ���� ����
        foreach (Transform spawnPoint in selectedSpawns)
        {
            Debug.Log("���� �����Ѱ���");
            GameManager.Instance.MonsterManager.Spawn(spawnPoint);
        }
    }

    /// <summary>
    /// ���� ���� ����Ʈ �� ������ ����Ʈ�� �����Ͽ� ��ȯ
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private Transform[] GetRandomSpawnPoints(int count)
    {
        List<Transform> spawnList = new List<Transform>(monsterSpawn);
        Transform[] selected = new Transform[count];

        // Fisher-Yates ������ ����Ͽ� ����Ʈ ����
        for (int i = spawnList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = spawnList[i];
            spawnList[i] = spawnList[randomIndex];
            spawnList[randomIndex] = temp;
        }

        // �տ������� `count`�� ����
        for (int i = 0; i < count; i++)
        {
            selected[i] = spawnList[i];
        }

        return selected;
    }
    public void SpawnEntity()
    {
        SpawnPlayer();
        SpawnMonsters();
        GameManager.Instance.AbilityManager.SetAbility(AbilityEnum.FrontShot);
    }
}


/////////////////////////*�±Դ� �ڵ�*/////////////////////////
//public Image SelectImage; // UI�� ǥ���� �̹���
//public Text SelectName; // �� �̸� ǥ��
//public Sprite[] SelectImages; // �� �̹��� �迭
//public string[] SelectNames; // �� �̸� �迭
//public GameObject[] MapPrefabs; // �� ������ �迭
//public Transform mapSpawnPoint;
//public Camera mainCamera;

//private int SelectIndex = 0; // ���� ���õ� ���� �ε���
//private GameObject currentMapInstance; // ���� Ȱ��ȭ�� �� ������

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
//    // ���� �� ����
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
//        Debug.LogError("�� �������� �������� ����: " + SelectNames[SelectIndex]);
//    }
//}
/////////////////////////*�±Դ� �ڵ�*/////////////////////////