using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               
    KayaHealth playerHealth;      
    EnemyHealth enemyHealth;       
    UnityEngine.AI.NavMeshAgent nav;               


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<KayaHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            nav.SetDestination(player.position);
        else
            nav.enabled = false;
    }
}
