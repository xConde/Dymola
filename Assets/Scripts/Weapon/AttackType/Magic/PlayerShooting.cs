using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 7;                  
    public float timeBetweenShot = .8f;        
    public float range = 200f;                      

    float timer;                                    
    Ray shootRay;                                   
    RaycastHit shootHit;                            
    int shootableMask;                              
    ParticleSystem magicParticles;                   
    LineRenderer magicLine;                         
    AudioSource magicAudio;                          
    Light magicLight;                                
    float effectsDisplayTime = 0.2f;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        magicParticles = GetComponent<ParticleSystem>();
        magicLine = GetComponent<LineRenderer>();
        magicAudio = GetComponent<AudioSource>();
        magicLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenShot)
            Shoot();

        if (timer >= timeBetweenShot * effectsDisplayTime)
            DisableEffects();
    }

    public void DisableEffects()
    {
        magicLine.enabled = false;
        magicLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        magicAudio.Play();

        magicLight.enabled = true;

        magicParticles.Stop();
        magicParticles.Play();

        magicLine.enabled = true;
        magicLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
                enemyHealth.TakeDamage(damagePerShot);

            magicLine.SetPosition(1, shootHit.point);
        }
        else
            magicLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
    }
}
