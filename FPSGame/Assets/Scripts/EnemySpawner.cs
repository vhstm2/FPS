using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("1번 스폰포인트")]
    public Transform spawnPoint1;
    float spawnTime1 = 3f;
    float lastSpawnTIme1;
    [Header("2번 스폰포인트")]
    public Transform spawnPoint2;
    float spawnTime2 = 5f;
    float lastSpawnTime2;

    public GameObject zombiePrefab;
    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gState != GameManager.GameState.Run) return;


        if (Time.time >= lastSpawnTIme1 + spawnTime1)                   //3초에 한번
        {
            lastSpawnTIme1 = Time.time;
            GameObject enemy = Instantiate(zombiePrefab, spawnPoint1.position + new Vector3(Random.Range(-1f  ,1f),0, Random.Range(-1f ,1f)), spawnPoint1.rotation);
        }
         if(Time.time >= lastSpawnTime2 + spawnTime2)           //5초에 한번
        {
            lastSpawnTime2 = Time.time;
            GameObject enemy = Instantiate(zombiePrefab, spawnPoint2.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), spawnPoint2.rotation);
        }
    }
}
