using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSelfDestructScript : MonoBehaviour {

    public float selfDestructTime = 3f;
    
	void Start () {
		Destroy(gameObject, selfDestructTime);
	}
}
