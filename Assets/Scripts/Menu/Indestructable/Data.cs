using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    public static Data singleton;
    public Text scoreText = null;
    public Text levelText = null;
    public int score = 0;
    public int level = 0;
    public GameObject musicSlider;
    public bool alive = true;
    public float seconds, minutes;
    bool GameStarted;


    public void Awake()
    {
        GameObject[] data = GameObject.FindGameObjectsWithTag("Data");
        if (data.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        singleton = this;

        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("level", 0);
        musicSlider.GetComponent<MusicSlider>().Start();
}

    public void UpdateScore(int points)
    {
        score += points;
        PlayerPrefs.SetInt("score", score);
        if (scoreText != null)
            scoreText.text = "Score    " + score;
    }

    public void UpdateLevel()
    {
        level++;
        PlayerPrefs.SetInt("level", level);

        if (levelText != null) {
            level = PlayerPrefs.GetInt("level");
            levelText.text = "Level   " + level;
        }
    }


}
