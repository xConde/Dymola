using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotateObject : MonoBehaviour
{
    public float XdegreesPerSecond = 540;
    public float YdegreesPerSecond = 540;
    public float ZdegreesPerSecond = 540;
    
    
    void Update()
    {
        // transform.Rotate(0, Time.deltaTime * degreesPerSecond, 0);
        transform.Rotate(Random.Range((XdegreesPerSecond * Time.deltaTime)/2, XdegreesPerSecond * Time.deltaTime), Random.Range((YdegreesPerSecond * Time.deltaTime)/2, YdegreesPerSecond * Time.deltaTime), Random.Range((ZdegreesPerSecond * Time.deltaTime)/2, ZdegreesPerSecond * Time.deltaTime));
    }
}