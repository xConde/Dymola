using UnityEngine;
using System.Collections;

public class csDestroyEffect : MonoBehaviour {
	
	void Start ()
    {
        Invoke("Over", 1.5f);       
    }

    private void Over()
    {
        Destroy(gameObject);
    }
}
