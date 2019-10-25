using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indestructable : MonoBehaviour
{
    public static Indestructable instance = null;

    public int prevSceneIndex = 0;
    public int savedLevel = 1;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public int getPrevSceneIndex() { return prevSceneIndex;  }
    public int getSavedLevel() { return savedLevel; }
}
