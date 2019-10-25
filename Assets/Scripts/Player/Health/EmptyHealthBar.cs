using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmptyHealthBar : MonoBehaviour
{
    public float hitpoints;

    // Update is called once per frame
    void Update()
    {
        hitpoints = gameObject.GetComponent<Image>().fillAmount;
        if (hitpoints <= 0f)
            Invoke("EndOfLevel", 2);
    }

    void EndOfLevel() {
        Indestructable.instance.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
    }
}
