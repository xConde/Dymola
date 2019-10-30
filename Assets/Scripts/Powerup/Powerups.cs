using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public GameObject player;
    public PlayerShooting shoot;
    public KayaHealth health;
    public KayaMovement movement;

    public float powerUpDuration = 10f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");          //player
        health = player.GetComponent<KayaHealth>();         //health
        shoot = player.GetComponent<PlayerShooting>();      //shooting
        movement = player.GetComponent<KayaMovement>();     //movement
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (gameObject.tag.Equals("PowerUpHeal"))
                powerUpHeal();
            else if (gameObject.tag.Equals("PowerUpInvincible"))
                powerUpInvincible();
            else if (gameObject.tag.Equals("PowerUpSpeed"))
                powerUpSpeed();
            else if (gameObject.tag.Equals("powerUpDamageBoost"))
                powerUpDamageBoost();

            selfDestruct();
        }
    }

    void powerUpHeal()
    {
        health.currentHealth = health.startingHealth;
    }

    void powerUpInvincible()
    {
        health.powerUpInvicible(powerUpDuration);
    }

    void powerUpSpeed()
    {
        movement.powerUpSpeed(powerUpDuration);
    }

    void powerUpDamageBoost()
    {
        shoot.powerUpWeaponDamage(powerUpDuration);
    }

    void selfDestruct()
    {
        Destroy(gameObject, 0.1f);
    }
}
