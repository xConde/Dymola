using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluntMelee : MonoBehaviour
{
    public float timeBetweenAttacks = 2f;
    public int attackDamage = 42;

    Animator anim;
    GameObject monster;
    EnemyHealth enemyHealth;
    KayaHealth KayaHealth;
    KayaAttack KayaAttack;
    bool enemyInRange;
    float timer;

    AudioSource mauleeAudio;


    void Awake()
    {

        mauleeAudio = GetComponent<AudioSource>();
        KayaAttack = FindObjectOfType<KayaAttack>();
        enemyHealth = FindObjectOfType<EnemyHealth>();
        KayaHealth = FindObjectOfType<KayaHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered OnTriggerEnter");
        monster = GameObject.FindGameObjectWithTag("Monster");

        if (other.gameObject == monster)
        {
            enemyInRange = true;
            Debug.Log("enemyInRange set to true");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == monster)
            enemyInRange = false;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && enemyInRange && KayaHealth.currentHealth > 0 && KayaAttack.mauleAttacking)
            Attack();
    }


    void Attack()
    {
        timer = 0f;
        Debug.Log("Entered Attack");
        mauleeAudio.Play();
        if (enemyHealth.currentHealth > 0)
            enemyHealth.TakeDamage(attackDamage);
    }
}
