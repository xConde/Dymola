using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public GameObject player;
    public PlayerShooting shoot;
    public KayaHealth health;
    public KayaMovement movement;
    public GameObject pickupEffect;
    ActivePowerups activepowerups;

    public float powerUpDuration = 10f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");          //player
        health = player.GetComponent<KayaHealth>();         //health
        shoot = FindObjectOfType<PlayerShooting>();      //shooting
        movement = player.GetComponent<KayaMovement>();     //movement
        activepowerups = FindObjectOfType<ActivePowerups>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            pickup();
            selfDestruct();
        }
    }

    void pickup()
    {
        //pickup effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        if (gameObject.tag.Equals("PowerUpHeal"))
            powerUpHeal();
        else if (gameObject.tag.Equals("PowerUpInvincible"))
            powerUpInvincible();
        else if (gameObject.tag.Equals("PowerUpSpeed"))
            powerUpSpeed();
        else if (gameObject.tag.Equals("PowerUpDamageBoost"))
            powerUpDamageBoost();
    }

    void powerUpHeal()
    {
        activepowerups.SetActive(0);
        health.currentHealth = health.startingHealth;
        health.setSliderBar();
    }

    void powerUpInvincible()
    {
        activepowerups.SetActive(1);
        health.powerUpInvincible(powerUpDuration);
    }

    void powerUpSpeed()
    {
        activepowerups.SetActive(2);
        movement.powerUpSpeed(powerUpDuration);
    }

    void powerUpDamageBoost()
    {
        activepowerups.SetActive(3);
        shoot.powerUpWeaponDamage(powerUpDuration);
    }

    void selfDestruct()
    {
        Destroy(gameObject, 0.1f);
    }
}
