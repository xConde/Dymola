using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public KayaHealth playerHealth;
    public GameObject[] enemy;          //Charles, RatGuy, Mr Tank === by ranking of 60%, 30%, 10%
    public float spawnTime = 3f;        //Time between each enemy spawn
    public Transform[] spawnPoints;     //Quantity of spawns available to spawn enemies
    public int currentWave;

    // Waves Information
    int totalWaves = 90;
    int totalEnemiesInCurrentWave;      
    int spawnedEnemies;                 //Enemies currently spawned on current wave
    int enemiesKilledInWave;

    // Enemy spawn quantity & quality
    float initalSpawnAmount = 10;
    float spawnMultiplier;
    int charlesQuantity, ratguyQuantity, mrtankQuantity;

    double[] enemyPercent = { .6, .3, .09 };


    void Start()
    {
        currentWave = 0;
        spawnMultiplier = 0;
        StartNextWave();
    }

    void StartNextWave() {
        currentWave++;
        CurrentWave.wave = currentWave;

        if (currentWave > totalWaves)
            return;
        //spawnMultipler, increases enemies per wave
        spawnMultiplier = initalSpawnAmount + (initalSpawnAmount * currentWave / 5);
        //totalEnemiesInCurrentWave carries the value of enemies quantity
        totalEnemiesInCurrentWave = (int) spawnMultiplier;
        EnemiesLeft.totalEnemies = totalEnemiesInCurrentWave;
        EnemiesLeft.enemiesLeft = totalEnemiesInCurrentWave;

        //spread enemy quantity per group
        charlesQuantity = Mathf.RoundToInt((int)(totalEnemiesInCurrentWave * enemyPercent[0]));
        ratguyQuantity = Mathf.RoundToInt((int)(totalEnemiesInCurrentWave * enemyPercent[1]));
        mrtankQuantity = Mathf.RoundToInt((int)(totalEnemiesInCurrentWave * enemyPercent[2]));

        //if we have a round error fix it by just tagging on the difference to ratguys
        if (totalEnemiesInCurrentWave > (charlesQuantity + ratguyQuantity + mrtankQuantity))
            ratguyQuantity += totalEnemiesInCurrentWave - (charlesQuantity + ratguyQuantity + mrtankQuantity);

        //preset definitions
        enemiesKilledInWave = 0;
        spawnedEnemies = 0;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < totalEnemiesInCurrentWave)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            //Create an instance of the enemy prefab at the randomly selected point
            int randomEnemy = roll();

            if (charlesQuantity > 0 && randomEnemy == 0)
            {   //only allow enemies to spawn if they're actually being spawned. This was setting offsetting enemy quantity spawn by being outside of this for each roll
                spawnedEnemies++; 
                charlesQuantity--;
                Instantiate(enemy[randomEnemy], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                yield return new WaitForSeconds(spawnTime);
            }
            else if (ratguyQuantity > 0 && randomEnemy == 1)
            {
                spawnedEnemies++;
                ratguyQuantity--;
                Instantiate(enemy[randomEnemy], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                yield return new WaitForSeconds(spawnTime);
            }
            else if (mrtankQuantity > 0 && randomEnemy == 2)
            {
                spawnedEnemies++;
                mrtankQuantity--;
                Instantiate(enemy[randomEnemy], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                yield return new WaitForSeconds(spawnTime);
            }
            else
                roll();
        }
    }

    int roll()
    {
        return Random.Range(0, 3);
    }

    public void EnemyDefeated()
    {
        enemiesKilledInWave++;
        int remaining = totalEnemiesInCurrentWave - enemiesKilledInWave;
        EnemiesLeft.enemiesLeft = remaining; //post remaining on UI canvas

        if (enemiesKilledInWave == totalEnemiesInCurrentWave && spawnedEnemies == totalEnemiesInCurrentWave)
            StartNextWave();
    }

}
