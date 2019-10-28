using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIWitch : MonoBehaviour
{
    public EnemyCounterScript enemyCounterScript;
    public Transform target;
    public float defaultSpeed = 10f;
    public int damageAmount = 0;
    public float attackCooldown = 2f;
    public float lastAttackTime = 0f;
    public bool isAbleToAttack;
    public float takeDamageCooldown = 0.25f;
    public bool isAbleToBeDamaged;
    public bool alreadyDead;
    public GameObject impactParticles;
    public Fireball Fireball;
    
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
        isAbleToBeDamaged = true;
        animator = gameObject.GetComponent<Animator>();
        rBody = gameObject.GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        kaya = target.GetComponent<KayaController>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>(); // the agent component of
        health = gameObject.GetComponent<Health>();
        deathSound = Resources.Load("DeathWitch") as GameObject;
        Fireball = gameObject.GetComponent<Fireball>();
    }
    void Update()
    {
        if (!health.isDead) {
            if (kaya.isAlive){
                agent.SetDestination(target.position);
                transform.LookAt(target);
                float dist = Vector3.Distance(transform.position, target.position);
                if (dist <= agent.stoppingDistance){
                    Attack();
                } else if (dist > agent.stoppingDistance){
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
        if (lastAttackTime <= Time.timeSinceLevelLoad - attackCooldown){
            agent.speed = 0f;
            Invoke("resetSpeed", 0.2f);
            lastAttackTime = Time.timeSinceLevelLoad;
            animator.SetTrigger("CastFireball");
            animator.SetInteger("animation", 0);
            Fireball.fireballAttack();
        }
    }
    
    void Die()
    {
        alreadyDead = true;
        agent.isStopped = true;
        GameObject newMob = Instantiate(deathSound, transform.position, transform.rotation);
        animator.SetTrigger("Dead");
        enemyCounterScript.decrementEnemyCount();
        Destroy(gameObject, 2.5f);
    }
    
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag.Equals("Player")){
            
            agent.speed = 0f;
        }
        if (collision.gameObject.tag.Equals("PlayerProjectile")){
            knockback();
            if (isAbleToBeDamaged){
                Instantiate(impactParticles, transform);
                Debug.Log("there is a collision with" + collision.gameObject);
                isAbleToBeDamaged = false;
                Invoke("canBeDamaged", takeDamageCooldown);
                health.DecrementHealth(10);
            }
        }
    }
    
    public void doMeleeDamage(int ATK){
        knockback();
        isAbleToBeDamaged = false;
        Invoke("canBeDamaged", takeDamageCooldown);
        health.DecrementHealth(ATK);
    }
    
    private void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag.Equals("Player")){
            resetSpeed();
        }
    }
    
    void knockback(){
        agent.speed = -10f;
        Invoke("resetSpeed", 0.5f);
    }
    
    void resetSpeed(){
        agent.speed = defaultSpeed;
    }

    bool AnimationIsPlaying(string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }
    
    void canDamage(){
        isAbleToAttack = true;
    }
    
    void canBeDamaged(){
        isAbleToBeDamaged = true;
    }
}


