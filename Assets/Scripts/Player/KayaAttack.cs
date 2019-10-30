using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayaAttack : MonoBehaviour
{
    //Weapon
    public string currentweapon;

    public float attackInput;

    public bool isAllowedToAttack = true;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        else if (Equals(currentweapon, "Rifle"))
        {
            anim.SetBool("HasPistol", false);
            anim.SetBool("HasRifle", true);
        }
        else if (Equals(currentweapon, "Maule"))
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
                anim.SetTrigger("ShootPistol");
            }
            else if (Equals(currentweapon, "Rifle"))
            {
                anim.SetTrigger("ShootRifle");
            }
            else if (Equals(currentweapon, "Maule"))
                anim.SetTrigger("Smash");

            isAllowedToAttack = false;
            Invoke("canDamage", 0.85f);
        }
    }

    void canDamage() { isAllowedToAttack = true; }

    public void updateWeapon(string weaponName) { currentweapon = weaponName; }
}
