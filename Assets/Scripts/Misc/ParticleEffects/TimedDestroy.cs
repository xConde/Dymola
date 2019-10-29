using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float selfDestructTime = 3f;

    void Start()
    {
        Destroy(gameObject, selfDestructTime);
    }
}
