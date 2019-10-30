using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWave : MonoBehaviour
{
    public static int wave;
    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        wave = 0;
    }


    void Update()
    {
        text.text = "Wave " + wave;
    }
}
