using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluntMelee : MonoBehaviour
{
    public int attackPower = 14;
    public int TankEnemyMultiplier = 9;
    public int BasicEnemyMultiplier = 2;
    public int MidtierEnemyMulitplier = 5;
    public KayaController kaya;
    public AudioSource aSource;
    public AudioClip slashHitSolid;

    private void Start()
    {
        aSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Monster") && (kaya.attackInput > 0))
        {
            aSource.PlayOneShot(slashHitSolid, 1f);
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
