using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMouseSpear : MonoBehaviour
{
    public EnemyCounterScript enemyCounterScript;
    public Transform target;
    public float defaultSpeed = 5f;
    public int damageTaken = 10;
    public int attackPower = 10;
    public float attackCooldown = 1f;
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
        isAbleToBeDamaged = true;
        animator = gameObject.GetComponent<Animator>();
        rBody = gameObject.GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        kaya = target.GetComponent<KayaController>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>(); // the agent component of
        health = gameObject.GetComponent<Health>();
        deathSound = Resources.Load("DeathMouse") as GameObject;
    }
    void Update()
    {
        if (!health.isDead)
        {
            if(kaya.isAlive){
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
        GameObject newMob = Instantiate(deathSound, transform.position, transform.rotation);
        // newMob.transform.position = transform.position;
		agent.isStopped = true;
        animator.SetTrigger("Dead");
        enemyCounterScript.decrementEnemyCount();
        Destroy(gameObject, 1.5f);
    }
    
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag.Equals("Player")){
            agent.speed = 0f;
        }
        if (collision.gameObject.tag.Equals("Weapon") || collision.gameObject.tag.Equals("PlayerProjectile")){
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
        animator.SetTrigger("Knockback");
        agent.speed = -10f;
        isAbleToAttack = false;
        Invoke("resetSpeed", 0.5f);
        Invoke("canDamage", 0.5f);
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


