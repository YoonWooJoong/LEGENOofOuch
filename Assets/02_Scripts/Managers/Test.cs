using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject[] mapPrefabs; // 여러 개의 맵 프리팹 배열
    private GameObject selectedMapInstance; // 선택된 맵의 인스턴스
    private StageContainer stageContainer;


    public void SpawnRandomMap()
    {
        // 랜덤한 맵 선택
        GameObject selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)];

        // 맵을 생성 (0,0,0 위치에 배치)
        selectedMapInstance = Instantiate(selectedMap, Vector3.zero, Quaternion.identity);
        stageContainer = selectedMapInstance.GetComponent<StageContainer>();
        Debug.Log($"선택된 맵: {selectedMapInstance.name}");

        // 생성된 맵에서 플레이어 스폰 포인트 찾기
        Transform playerSpawnPoint = stageContainer.playerSpawnPoint;
        Transform[] monsterSpawnPoint = stageContainer.enemySpawnPoint;

        GameManager.Instance.GetTransrate(playerSpawnPoint, monsterSpawnPoint);

    }
}


