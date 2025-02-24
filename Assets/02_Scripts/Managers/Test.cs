using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject[] mapPrefabs; // 여러 개의 맵 프리팹 배열
    private GameObject selectedMapInstance; // 선택된 맵의 인스턴스

    private void Start()
    {
        SpawnRandomMap();
    }

    private void SpawnRandomMap()
    {
        // 랜덤한 맵 선택
        GameObject selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)];

        // 맵을 생성 (0,0,0 위치에 배치)
        selectedMapInstance = Instantiate(selectedMap, Vector3.zero, Quaternion.identity);

        Debug.Log($"선택된 맵: {selectedMapInstance.name}");

        // 생성된 맵에서 플레이어 스폰 포인트 찾기
        Transform playerSpawnPoint = selectedMapInstance.transform.Find("PlayerSpawnPoint");
        Transform monsterSpawnPoint = selectedMapInstance.transform.Find("MonsterSpawnPoint");

        ////if (PlayerSpawnPoint != null)
        //{
        //    //Debug.Log($"플레이어 스폰 위치: {PlayerSpawnPoint.position}");
        //    //MovePlayerToSpawn(PlayerSpawnPoint);
        ////}

    }

    private void GiveTransrate()
    {
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            Debug.Log("플레이어가 스폰 위치로 이동됨.");
        }
        else
        {
            Debug.LogError("GameManager 또는 플레이어 오브젝트가 없습니다!");
        }
    }
}


