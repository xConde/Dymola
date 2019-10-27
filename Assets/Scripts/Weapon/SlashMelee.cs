using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashMelee : MonoBehaviour
{
    public int attackPower = 10;
    public int TankEnemyMultiplier = 9;
    public int BasicEnemyMultiplier = 2;
    public int MidtierEnemyMulitplier = 5;
    public KayaController kaya;
    public AudioSource aSource;
    public AudioClip slashHitFlesh;
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
            //if (other.gameObject.GetComponent<>()) {}
        }
    }

}
