using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     
    public int attackDamage = 10;

    UnityEngine.AI.NavMeshAgent agent;
    float npcspeed;

    Animator anim;                            
    GameObject player;                         
    KayaHealth KayaHealth;               
    EnemyHealth enemyHealth;                    
    bool playerInRange;                        
    float timer;                               


    void Awake()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        KayaHealth = player.GetComponent<KayaHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        npcspeed = agent.speed;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = true;
        
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            Attack();

        
        if (KayaHealth.currentHealth <= 0)
            anim.SetTrigger("PlayerDead");
    }

    void pauseAttack()
    {
        agent.speed = npcspeed;
    }

    void Attack()
    {
        timer = 0f;
        agent.speed = 0f;

        if (KayaHealth.currentHealth > 0)
            KayaHealth.TakeDamage(attackDamage);

        Invoke("pauseAttack", 1.3f);
    }
}
