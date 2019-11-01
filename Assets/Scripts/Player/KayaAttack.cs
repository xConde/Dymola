using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayaAttack : MonoBehaviour
{
    //Weapon
    public string currentweapon;

    public float attackInput;

    public bool isAllowedToAttack = true;

    float durationperiod;

    PlayerShooting shoot;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        shoot = FindObjectOfType<PlayerShooting>();
        anim = GetComponent<Animator>();
        durationperiod = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attackInput = Input.GetAxis("Fire1");
        AnimateStatus();
        Attack();

    }

    void AnimateStatus() {
        if (Equals(currentweapon, "Pistol"))
        {
            anim.SetBool("HasPistol", true);
            anim.SetBool("HasRifle", false);
        }
        else if (Equals(currentweapon, "Rifle") || Equals(currentweapon, "Shotgun"))
        {
            anim.SetBool("HasPistol", false);
            anim.SetBool("HasRifle", true);
        }
        else 
        {
            anim.SetBool("HasPistol", false);
            anim.SetBool("HasRifle", false);
        }
            
    }

    void Attack()
    {
        if (attackInput > 0 && isAllowedToAttack)
        {
            if (Equals(currentweapon, "Pistol"))
            {
                if (!(shoot.powerupActive))
                    durationperiod = shoot.pistolTimeBetweenShot;
                else
                    durationperiod = shoot.timeBetweenShot;
                anim.SetTrigger("ShootPistol");               
            }
            else if (Equals(currentweapon, "Rifle"))
            {
                if (!(shoot.powerupActive))
                    durationperiod = shoot.rifleTimeBetweenShot;
                else
                    durationperiod = shoot.timeBetweenShot;
                anim.SetTrigger("ShootRifle");
            }
            else if (Equals(currentweapon, "Shotgun"))
            {
                if (!(shoot.powerupActive))
                    durationperiod = shoot.shotgunTimeBetweenShot;
                else
                    durationperiod = shoot.timeBetweenShot;
                anim.SetTrigger("ShootRifle");
            }

            isAllowedToAttack = false;
            Invoke("canDamage", durationperiod);
        }
    }

    void canDamage() { isAllowedToAttack = true; }

    public void updateWeapon(string weaponName) { currentweapon = weaponName; }
}
