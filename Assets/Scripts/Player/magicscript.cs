using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicscript : MonoBehaviour
{
    public GameObject prefab;
    public KayaController kaya;
    public float castDelay = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load("Fireball") as GameObject;
    }

    public void mage() {
        Invoke("delayedMagicProjectile", castDelay);
    }

    void delayedMagicProjectile() {
        GameObject missile = Instantiate(prefab) as GameObject;
        missile.transform.position = kaya.transform.position + new Vector3(0, 1, 0) + (kaya.transform.forward * 0.5f);
        Rigidbody rBody = missile.GetComponent<Rigidbody>();
        rBody.velocity = kaya.transform.forward * 40;
    }
}
