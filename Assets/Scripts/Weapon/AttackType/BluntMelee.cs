using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluntMelee : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    public int attackDamage = 24;

    Animator anim;
    GameObject monster;
    EnemyHealth enemyHealth;
    KayaHealth KayaHealth;
    bool enemyInRange;
    float timer;


    void Awake()
    {
        monster = GameObject.FindGameObjectWithTag("Monster");
        enemyHealth = monster.GetComponent<EnemyHealth>();
        KayaHealth = GetComponent<KayaHealth>();
        anim = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
        if (other.gameObject == monster)
        {
            enemyInRange = true;
            Debug.Log("========================== HIT ====================");
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

        if (timer >= timeBetweenAttacks && enemyInRange && KayaHealth.currentHealth > 0)
            Attack();
    }


    void Attack()
    {
        timer = 0f;

        if (enemyHealth.currentHealth > 0)
            enemyHealth.TakeDamage(attackDamage);
    }
}
