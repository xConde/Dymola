using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject prefab;
    public float castDelay = 0.2f;
    public float fireballSpeed = 15f;

    // Use this for initialization
    void Start()
    {
        prefab = Resources.Load("Fireball") as GameObject;
    }

    public void fireballAttack()
    {
        Invoke("delayedFireballAttack", castDelay);
    }

    void delayedFireballAttack()
    {
        GameObject missile = Instantiate(prefab) as GameObject;
        missile.transform.position = transform.position + new Vector3(0, 1, 0) + (transform.forward * 1f);
        Rigidbody rBody = missile.GetComponent<Rigidbody>();
        rBody.velocity = transform.forward * fireballSpeed;
    }
}
