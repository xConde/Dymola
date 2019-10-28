using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour {

    public Text levelText;
    public int levelVar;
    public int currentSceneIndex;
    
    void Start () {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (Indestructable.instance.prevSceneIndex == 0){
            levelVar = 1;
        } else if (Indestructable.instance.prevSceneIndex == 1){
            levelVar = Indestructable.instance.savedLevel;
        } else if (Indestructable.instance.prevSceneIndex == 3){ // next level
            levelVar = Indestructable.instance.savedLevel;
            levelVar +=1;
        }
        Indestructable.instance.savedLevel = levelVar;
        levelText = gameObject.GetComponent<Text>();
        if ( currentSceneIndex == 1){
            levelText.text = "LEVEL: " + levelVar.ToString("0");
        } else if (currentSceneIndex == 2){
            levelText.text = "You got to level  " + levelVar.ToString("0") + "!";
        } else if (currentSceneIndex == 3) {
            levelText.text = "You cleared level " + levelVar.ToString("0") + "!";
        }
	}
}
