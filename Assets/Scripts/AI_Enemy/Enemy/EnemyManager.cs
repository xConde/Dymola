using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public KayaHealth playerHealth;       
    public GameObject[] enemy;                
    public float spawnTime = 3f;            
    public Transform[] spawnPoints;
    int enemiesInRound;


    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f || enemiesInRound <= 0)
            return;
        

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy[0], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        Instantiate(enemy[1], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        Instantiate(enemy[2], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
