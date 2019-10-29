using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashMelee : MonoBehaviour
{
    public int attackPower = 10;
    public int TankEnemyMultiplier = 9;
    public int BasicEnemyMultiplier = 2;
    public int MidtierEnemyMulitplier = 5;
    public KayaAttack kaya;
    public AudioSource slashHit;

    private void Start()
    {
        slashHit = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Monster") && (kaya.attackInput > 0))
        {
            slashHit.Play();
            if (other.gameObject.GetComponent<AIGolem>())
            {
                AIGolem golem = other.gameObject.GetComponent<AIGolem>();
                if (golem.isAbleToBeDamaged)
                    golem.doMeleeDamage(attackPower * TankEnemyMultiplier);
            }
            else if (other.gameObject.GetComponent<AIWitch>())
            {
                AIWitch witch = other.gameObject.GetComponent<AIWitch>();
                if (witch.isAbleToBeDamaged)
                    witch.doMeleeDamage(attackPower * BasicEnemyMultiplier);
            }
            else if (other.gameObject.GetComponent<AIMouseSpear>())
            {
                AIMouseSpear mouse = other.gameObject.GetComponent<AIMouseSpear>();
                if (mouse.isAbleToBeDamaged)
                    mouse.doMeleeDamage(attackPower * MidtierEnemyMulitplier);
            }
        }
    }
}
