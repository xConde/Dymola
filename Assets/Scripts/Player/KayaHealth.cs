using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KayaHealth : MonoBehaviour
{
    public int startingHealth = 100;                            
    public int currentHealth;                                   
    public Slider healthSlider;                                 
    public Image damageImage;                                   
    public AudioClip deathClip;                                
    public float flashSpeed = 5f;                              
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);    


    Animator anim;                                             
    AudioSource playerAudio;
    KayaMovement movement;
    KayaAttack attack;
    PlayerShooting shooting;
    bool isDead;                                               
    bool damaged;

    //invicibility - used as a power up or a cool down from dmg
    bool isInvincible;
    public float takeDamageCoolDown = 1.3f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        movement = GetComponent<KayaMovement>();
        attack = GetComponent<KayaAttack>();
        shooting = GetComponent<PlayerShooting>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (damaged)
            damageImage.color = flashColour;
        else
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            recentlyTookDamage();
            damaged = true;
            currentHealth -= amount;
            healthSlider.value = currentHealth;
            playerAudio.Play();
            if (currentHealth <= 0 && !isDead)
                Death();
        }
    }

    void recentlyTookDamage() 
    {
        isInvincible = true;
        Invoke("notInvicible", takeDamageCoolDown);
    }

    public void powerUpInvicible(float duration)
    {
        if (isInvincible)
            CancelInvoke("notInvincibile");
        isInvincible = true;
        Invoke("notInvincible", duration);
    }

    void notInvicible()
    {
        isInvincible = false;
    }


    void Death()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        movement.enabled = false;
        attack.enabled = false;
        Destroy(shooting);
    }
}
