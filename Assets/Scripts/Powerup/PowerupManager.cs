using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public KayaHealth playerHealth;
    public GameObject[] powerup;
    public float spawnTime;
    public Transform[] spawnPoints;

    void Start()
    {
        spawnTime = Random.Range(5, 20);

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
            return;

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int powerupType = Random.Range(0, powerup.Length);

        Instantiate(powerup[powerupType], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
