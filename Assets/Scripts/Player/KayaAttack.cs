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
        shoot = GetComponent<PlayerShooting>();
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
                anim.SetTrigger("ShootPistol");               
            else if (Equals(currentweapon, "Rifle"))
                anim.SetTrigger("ShootRifle");
            else if (Equals(currentweapon, "Shotgun"))
                anim.SetTrigger("ShootRifle");

            isAllowedToAttack = false;
            Invoke("canDamage", 0.85f);
        }
    }

    void canDamage() { isAllowedToAttack = true; }

    public void updateWeapon(string weaponName) { currentweapon = weaponName; }
}
