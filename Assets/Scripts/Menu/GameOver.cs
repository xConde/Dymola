using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public KayaHealth playerHealth;       
    public float restartDelay = 5f;           


    Animator anim;                          
    float restartTimer;                     


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
                Application.LoadLevel(Application.loadedLevel);
        }
    }
}
