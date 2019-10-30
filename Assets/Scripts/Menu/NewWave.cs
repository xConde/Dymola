using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWave : MonoBehaviour
{
    public EnemyManager manager;
    AudioSource waveIncrease;
    int wave;


    Animator anim;
    float restartTimer;


    void Awake()
    {
        waveIncrease = GetComponent<AudioSource>(); 
        manager = FindObjectOfType<EnemyManager>();
        anim = GetComponent<Animator>();
        wave = 1;
    }


    void Update()
    {

        if (manager.currentWave > wave)
        {
            waveIncrease.Play();
            anim.SetTrigger("NewWave");
        }
        
        wave = manager.currentWave;
    }
}
