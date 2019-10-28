using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyCounterScript : MonoBehaviour {
    
    public Text enemyCounterText;
    public Text levelScriptRef;
    public LevelScript levelScript;
    public int enemyVar;

	void Start () {
        enemyCounterText = gameObject.GetComponent<Text>();
        levelScript = levelScriptRef.GetComponent<LevelScript>();
		enemyVar = (levelScript.levelVar) * 10;
	}
    
    void Update(){
        if (enemyVar > 0){
            enemyCounterText.text = "Required kills: " + enemyVar.ToString("0");
        } else if ( enemyVar == 0){
            enemyCounterText.text = "Complete!";
            Invoke("loadContinueScene", 3.5f);
        }
    }
    
    void loadContinueScene(){
        Indestructable.instance.prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(3);
    }
	
	public void decrementEnemyCount(){
        this.enemyVar -= 1;
    }
}
