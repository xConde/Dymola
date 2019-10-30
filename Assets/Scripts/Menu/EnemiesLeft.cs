using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemiesLeft : MonoBehaviour
{
    public static int totalEnemies;
    public static int enemiesLeft;
    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        totalEnemies = 0;
        enemiesLeft = 0;
    }


    void Update()
    {
        text.text = "Enemies Left: " + enemiesLeft + "/" + totalEnemies;
    }
}
