using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActivePowerups : MonoBehaviour
{
    GameObject PowerUpHeal, PowerUpInvincible, PowerUpSpeed, PowerUpDamageBoost; //HUB slots for powerup display

    // Start is called before the first frame update
    void Start()
    {
        //Display powerup UI by default PowerUpInvincibleUI
        PowerUpHeal = GameObject.FindGameObjectWithTag("PowerUpHealUI");
        PowerUpInvincible = GameObject.FindGameObjectWithTag("PowerUpInvincibleUI");
        PowerUpSpeed = GameObject.FindGameObjectWithTag("PowerUpSpeedUI");
        PowerUpDamageBoost = GameObject.FindGameObjectWithTag("PowerUpDamageBoostUI");
        SetAllInActive();

    }

    void SetAllInActive()
    {
        PowerUpHeal.SetActive(false);
        PowerUpInvincible.SetActive(false);
        PowerUpSpeed.SetActive(false);
        PowerUpDamageBoost.SetActive(false);
    }

    public void SetActive(int powerup)
    {
        switch (powerup) {
            case 0:
                PowerUpHeal.SetActive(true);
                Invoke("SetInActiveHealth", 10);
                break;
            case 1:
                PowerUpInvincible.SetActive(true);
                Invoke("SetInActiveInvincibility", 10);
                break;
            case 2:
                PowerUpSpeed.SetActive(true);
                Invoke("SetInActiveSpeed", 10);
                break;
            case 3:
                PowerUpDamageBoost.SetActive(true);
                Invoke("SetInActiveDamage", 10);
                break;
        }
    }

    void SetInActiveHealth()
    {
        PowerUpHeal.SetActive(false);
    }

    void SetInActiveInvincibility()
    {
        PowerUpInvincible.SetActive(false);
    }

    void SetInActiveSpeed()
    {
        PowerUpSpeed.SetActive(false);
    }

    void SetInActiveDamage()
    {
        PowerUpDamageBoost.SetActive(false);
    }


}
