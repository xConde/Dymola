using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHitFireball : MonoBehaviour {
    
    public GameObject impactSound;
    
    void Start(){
        // impactSound = Resources.Load("ImpactFireball") as GameObject;
    }
    
	private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            // GameObject newMob = Instantiate(impactSound) as GameObject;
            GameObject newMob = Instantiate(impactSound);
            newMob.transform.position = transform.position;
            Destroy(gameObject, 0.1f);
        } else if (collision.gameObject.tag.Equals("Ground")) {
           Destroy(gameObject);
        }
    }
}