using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnTime = 3f;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    void SpawnEnemy()
    {
        if (playerHealth.currentHealth <= 0) return;
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
    }
}
