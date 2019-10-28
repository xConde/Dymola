using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIGolem : MonoBehaviour
{
    public EnemyCounterScript enemyCounterScript;
    public Transform target;
    public float defaultSpeed = 4f;
    public int damageTaken = 10;
    public int attackPower = 50;
    public float attackCooldown = 2f;
    public float lastAttackTime = 0f;
    public bool isAbleToAttack;
    public float takeDamageCooldown = 0.25f;
    public bool isAbleToBeDamaged;
    public bool alreadyDead;
    public GameObject impactParticles;
    
    Rigidbody rBody;
    UnityEngine.AI.NavMeshAgent agent;
    public GameObject deathSound;
    public Animator animator;
    public Health health;
    public KayaController kaya;

    void Start()
    {
        GameObject counterText = GameObject.FindWithTag("EnemyCounter");
        enemyCounterScript = counterText.GetComponent<EnemyCounterScript>();
        isAbleToAttack = true;
        animator = gameObject.GetComponent<Animator>();
        rBody = gameObject.GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        kaya = target.GetComponent<KayaController>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>(); // the agent component of
        health = gameObject.GetComponent<Health>();
        deathSound = Resources.Load("DeathGolem") as GameObject;
        
        agent.speed = 0f;
        Invoke("resetSpeed", 2.7f);
        Invoke("canBeDamaged", 2.7f);
    }
    void Update()
    {
        if (!health.isDead){
            if (kaya.isAlive){
                agent.SetDestination(target.position);
                transform.LookAt(target);
                float dist = Vector3.Distance(transform.position, target.position);
                if (dist <= agent.stoppingDistance){
                    Attack();
                } else {
                    Chasing();
                }
            } else {
                animator.SetInteger("animation", 0);
            }
        } else {
            if (!alreadyDead){
                Die();
            }
        }
    }
    void Chasing()
    {
        animator.SetInteger("animation", 1);
    }
    void Attack()
    {
        if (isAbleToAttack){
            agent.speed = 0f;
            Invoke("resetSpeed", 0.4f);
            animator.SetTrigger("Attack");
            animator.SetInteger("animation", 0);
            isAbleToAttack = false;
            Invoke("canDamage", attackCooldown);
        }
    }
    
    void attackDelay(){
        kaya.HurtPlayer(attackPower);
    }
    
    void Die()
    {
        alreadyDead = true;
        agent.isStopped = true;
        GameObject newMob = Instantiate(deathSound, transform.position, transform.rotation);
        animator.SetTrigger("Dead");
        enemyCounterScript.decrementEnemyCount();
        Destroy(gameObject, 3.2f);
    }
    
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag.Equals("Player")){
            agent.speed = 0f;
        }
        
        if (isAbleToBeDamaged){
            if (collision.gameObject.tag.Equals("PlayerProjectile")){
                isAbleToBeDamaged = false;
                Invoke("canBeDamaged", takeDamageCooldown);
                stun();
                Instantiate(impactParticles, transform);
                Debug.Log("there is a collision with" + collision.gameObject);
                health.DecrementHealth(damageTaken);
            }
        }
    }
    
    public void doMeleeDamage(int ATK){
        isAbleToBeDamaged = false;
        Invoke("canBeDamaged", takeDamageCooldown);
        health.DecrementHealth(ATK);
    }
    
    private void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag.Equals("Player")){
            resetSpeed();
        }
    }
    
    void stun(){
        animator.SetTrigger("Knockback");
        agent.speed = 0;
        isAbleToAttack = false;
        Invoke("resetSpeed", 2f);
        Invoke("canDamage", 2f);
    }
    
    void resetSpeed(){
        agent.speed = defaultSpeed;
    }
    
    void canDamage(){
        isAbleToAttack = true;
    }
    
    void canBeDamaged(){
        isAbleToBeDamaged = true;
    }
}


