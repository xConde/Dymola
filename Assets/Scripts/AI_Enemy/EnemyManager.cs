using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public KayaHealth playerHealth;
    public GameObject[] enemy;          //Charles, RatGuy, Mr Tank === by ranking of 60%, 30%, 10%
    public float spawnTime = 3f;        //Time between each enemy spawn
    public Transform[] spawnPoints;     //Quantity of spawns available to spawn enemies

    // Waves Information
    int totalWaves = 90;
    int totalEnemiesInCurrentWave;      
    int enemiesInWaveLeft;              //totalEnemiesinCurrentWave - deadEnemies
    int spawnedEnemies;                 //Enemies currently spawned on current wave
    public int currentWave;
    int enemiesKilledInWave;

    // Enemy spawn quantity & quality
    int initalSpawnAmount = 10;
    int spawnMultiplier;
    public int[] enemyQuantity = new int[3]; 
    public double[] enemyPercent = { .6, .3, .09 };


    void Start()
    {
        currentWave = 0;
        StartNextWave();
    }

    void StartNextWave() {
        currentWave++;
        CurrentWave.wave = currentWave;

        if (currentWave > totalWaves)
            return;
        //spawnMultipler, increases enemies per wave
        spawnMultiplier = initalSpawnAmount + (initalSpawnAmount * (currentWave / 5));
        //totalEnemiesInCurrentWave carries the value of enemies quantity
        totalEnemiesInCurrentWave = spawnMultiplier;
        EnemiesLeft.totalEnemies = totalEnemiesInCurrentWave;
        EnemiesLeft.enemiesLeft = totalEnemiesInCurrentWave;

        //spread enemy quantity per group
        enemyQuantity[0] = Mathf.FloorToInt((int)(totalEnemiesInCurrentWave * enemyPercent[0]));
        enemyQuantity[1] = Mathf.FloorToInt((int)(totalEnemiesInCurrentWave * enemyPercent[1]));
        enemyQuantity[2] = Mathf.FloorToInt((int)(totalEnemiesInCurrentWave * enemyPercent[2]));

        //preset definitions
        enemiesKilledInWave = 0;
        enemiesInWaveLeft = 0;
        spawnedEnemies = 0;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < totalEnemiesInCurrentWave)
        {
            spawnedEnemies++;
            enemiesInWaveLeft++;
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            //Create an instance of the enemy prefab at the randomly selected point
            int randomEnemy = roll();
            if (enemyQuantity[randomEnemy] <= 0)
                randomEnemy = roll();  

            Instantiate(enemy[randomEnemy], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            enemyQuantity[randomEnemy]--;
            yield return new WaitForSeconds(spawnTime);

        }
    }

    int roll()
    {
        return Random.Range(0, enemyQuantity.Length);
    }

    public void EnemyDefeated()
    {
        enemiesInWaveLeft--;
        enemiesKilledInWave++;
        int remaining = totalEnemiesInCurrentWave - enemiesKilledInWave;
        EnemiesLeft.enemiesLeft = remaining;

        if (enemiesInWaveLeft == 0 && spawnedEnemies == totalEnemiesInCurrentWave)
            StartNextWave();
    }

}
