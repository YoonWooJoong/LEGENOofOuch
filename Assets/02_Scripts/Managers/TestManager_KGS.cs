using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager_KGS : MonoBehaviour
{
    [SerializeField] PlayerCharacter player;
    [SerializeField] MonsterManager MM;

    [SerializeField] Rect[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MM.Spawn(spawnPoints[Random.Range(0, spawnPoints.Length)]);
        if (Input.GetKeyDown(KeyCode.Space))
            player.ChangeHealth(-2);
    }
}
