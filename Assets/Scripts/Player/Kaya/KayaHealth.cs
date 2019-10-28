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
    KayaController kaya;                                        
    bool isDead;                                               
    bool damaged;                                               


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        kaya = GetComponent<KayaController>();
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
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
            Death();
    }


    void Death()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        kaya.enabled = false;
    }
}
