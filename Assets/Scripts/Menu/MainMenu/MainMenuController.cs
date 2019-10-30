using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public int lives = 1;
    GameObject[] panels;
    GameObject[] MainMenuButtons;
    public static bool GameStarted = false;

    private void Start()
    {
        panels = GameObject.FindGameObjectsWithTag("Subpanel");
        MainMenuButtons = GameObject.FindGameObjectsWithTag("MainMenuButton");

        foreach (GameObject p in panels)
            p.SetActive(false);
    }

    public void LoadGameScene()
    {
        GameStarted = true;
        PlayerPrefs.SetInt("lives", lives);
        SceneManager.LoadScene("Environment", LoadSceneMode.Single);
     
    }

    public void ClosePanel(Button button)
    {
        button.gameObject.transform.parent.gameObject.SetActive(false);
        foreach (GameObject b in MainMenuButtons)
            b.SetActive(true);
    }

    public void OpenPanel(Button button)
    {
        button.gameObject.transform.GetChild(1).gameObject.SetActive(true);

        foreach (GameObject b in MainMenuButtons)
            if (b != button.gameObject)
                b.SetActive(false);

  
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            QuitGame();
    }
}
