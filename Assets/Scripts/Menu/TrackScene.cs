using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackScene : MonoBehaviour
{
    public int sceneIndex;

    public void LoadScene(int sceneIndex) {
        Indestructable.instance.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if ((SceneManager.GetActiveScene().name == "GameScene") && (Time.timeScale > 1)) 
            Cursor.visible = false;
        else
            Cursor.visible = true;     
    }
}
