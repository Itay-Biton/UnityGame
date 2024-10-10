using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public float timeDelay = 2f;
    public GameObject enemy;
    public int spawnAreaRadius = 3;
    int enemyCount = 0;
    public int maxEnemies = 3;

    List<float> deathTimes = new List<float>();

    void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void Update()
    {
        if (enemyCount < maxEnemies)
        {
            // Loop through death times to check for ready respawns
            for (int i = deathTimes.Count - 1; i >= 0; i--)
            {
                if (Time.time - deathTimes[i] >= timeDelay)
                {
                    deathTimes.RemoveAt(i);  // Remove the used timer
                    SpawnEnemy();
                }
            }
        }
    }
    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 spawnPoint = transform.position + Random.insideUnitSphere * spawnAreaRadius;
        spawnPoint.y = transform.position.y;
        return spawnPoint;
    }
    private void SpawnEnemy()
    {
        Instantiate(enemy, GetRandomSpawnPoint(), Quaternion.identity, transform);
        enemyCount++;
    }
    public void EnemyDead() {
        deathTimes.Add(Time.time);
        enemyCount--;
    }
}
