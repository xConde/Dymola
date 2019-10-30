using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            
    public float currentHealth;                   
    public float sinkSpeed = 2.5f;              
    public int scoreValue = 10;                 
    public AudioClip deathClip;

    public EnemyManager manager;

    Animator anim;                              
    AudioSource enemyAudio;                                     
    CapsuleCollider capsuleCollider;            
    bool isDead;                                
    bool isSinking;                             


    void Awake()
    {
        manager = FindObjectOfType<EnemyManager>();
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);

    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        if (currentHealth <= 0)
            Death();
    }


    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        ScoreManager.score += scoreValue;
        manager.EnemyDefeated();
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        Destroy(gameObject, 2f);
    }


    public void StartSinking()
    {

        GetComponent<NavMeshAgent>().enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        Destroy(gameObject, 2f);
    }
}
