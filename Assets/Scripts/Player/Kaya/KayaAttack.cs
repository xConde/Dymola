using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayaAttack : MonoBehaviour
{
    //Weapon
    public string currentweapon = "Sword";

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
        Attack();

    }

    void Attack()
    {
        if (attackInput > 0 && isAllowedToAttack)
        {
            if (Equals(currentweapon, "Sword"))
                anim.SetTrigger("Slash");
            else if (Equals(currentweapon, "Maule"))
                anim.SetTrigger("Smash");
            else if (Equals(currentweapon, "Staff"))
                anim.SetTrigger("Cast");

            isAllowedToAttack = false;
            Invoke("canDamage", 0.85f);
        }
    }

    void canDamage() { isAllowedToAttack = true; }

    public void updateWeapon(string weaponName) { currentweapon = weaponName; }
}
