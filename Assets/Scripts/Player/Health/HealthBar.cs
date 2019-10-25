using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] float fillRatio;
    [SerializeField] Image content;

    // Update is called once per frame
    void Update()
    {
        content.fillAmount = fillRatio;
    }
}
