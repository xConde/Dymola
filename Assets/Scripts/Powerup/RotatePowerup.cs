using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerup : MonoBehaviour
{
    public float XdegreesPerSecond = 450;
    public float YdegreesPerSecond = 450;
    public float ZdegreesPerSecond = 0;


    void Start()
    {
        StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        transform.Rotate(Random.Range(XdegreesPerSecond * Time.deltaTime / 2, XdegreesPerSecond * Time.deltaTime), Random.Range((YdegreesPerSecond * Time.deltaTime) / 2, YdegreesPerSecond * Time.deltaTime), Random.Range((ZdegreesPerSecond * Time.deltaTime) / 2, ZdegreesPerSecond * Time.deltaTime));
        yield return new WaitForSeconds(3);
    }
}
