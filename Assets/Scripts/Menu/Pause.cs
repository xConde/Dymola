using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                ResumePeriod();
            else
                PausePeriod();           
        }
    }

    public void ResumePeriod()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    void PausePeriod()
    {
        Cursor.visible = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("You have quit the game");
        if (UnityEditor.EditorApplication.isPlaying == true)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
}
