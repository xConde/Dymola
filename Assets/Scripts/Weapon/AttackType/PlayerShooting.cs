using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float damagePerShot = 16;                  
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
    public Pause pause;


    public WeaponManagement weapon;
    string currentWeapon;

    //pistol preset
    int pistolDamagePerShot = 30;
    public float pistolTimeBetweenShot = 1f;

    //rifle preset
    int rifleDamagePerShot = 12;
    public float rifleTimeBetweenShot = .35f;

    //shotgun preset
    int shotgunDamagePerShot = 23;
    public float shotgunTimeBetweenShot = 1.5f;

    public bool powerupActive;


    void Awake()
    {
        weapon = FindObjectOfType<WeaponManagement>();
        powerupActive = false;
        shootableMask = LayerMask.GetMask("Shootable");
        pause = GetComponent<Pause>();
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
        if (Pause.GameIsPaused)
            return;

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

    public void powerUpWeaponDamage(float duration)
    {
        powerupActive = true;
        currentWeapon = weapon.currentWeapon;

        float damageMultiplier = Random.Range(.5f, 3);
        float speedMultiplier = Random.Range(.5f, 3);

        if (currentWeapon == "Pistol")
        {
            resetPistol();
            timeBetweenShot /= speedMultiplier;
            damagePerShot *= damageMultiplier;
            Invoke("resetPistol", duration);
        }
        else if (currentWeapon == "Rifle")
        {
            resetRifle();
            timeBetweenShot /= speedMultiplier;
            damagePerShot *= damageMultiplier;
            Invoke("resetRifle", duration);
        }
        else if (currentWeapon == "Shotgun")
        {
            resetShotgun();
            timeBetweenShot /= speedMultiplier;
            damagePerShot *= damageMultiplier;
            Invoke("resetShotgun", duration);
        }

        powerupActive = false;
    }

    private void resetShotgun()
    {
        damagePerShot = shotgunDamagePerShot;
        timeBetweenShot = shotgunTimeBetweenShot;
    }

    private void resetRifle()
    {
        damagePerShot = rifleDamagePerShot;
        timeBetweenShot = rifleTimeBetweenShot;
    }

    private void resetPistol()
    {
        damagePerShot = pistolDamagePerShot;
        timeBetweenShot = pistolTimeBetweenShot;

    }

}
