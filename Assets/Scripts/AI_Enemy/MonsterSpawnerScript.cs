using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonsterSpawnerScript : MonoBehaviour {

    public static int monsterCounter = 0;
    public int monsterCap;
    public GameObject mouseSpear;
    public GameObject witch;
    public GameObject golem;
    public float spawnCooldown = 10f;
    public float spawnDelay = 5f;
    public float totalSpawnTime;
    
    public Text levelScriptRef; // Manally assign this
    public LevelScript levelScript;
    
	void Start() {
            monsterCounter = 0;
            monsterCap = 100;
            setMonsterCap();
            InvokeRepeating("spawnCondition", spawnDelay, spawnCooldown);
	}
    
    void spawnCondition(){
        if (monsterCounter < monsterCap){
            spawnMonster();
        }
    }
    
    void spawnMonster(){
        int i = Random.Range(1, 10);
        if ( i == 1 || i == 2){
            instantiateMonster(golem);
        } else if ( (i > 2) && (i < 5) ){
            instantiateMonster(witch);
        } else {
            instantiateMonster(mouseSpear);
        }
    }
    
    void setMonsterCap(){
        levelScript = levelScriptRef.GetComponent<LevelScript>();
        monsterCap = (levelScript.levelVar) * 10;
        totalSpawnTime = monsterCap * spawnCooldown;
    }
    
    void instantiateMonster(GameObject monster){
        monsterCounter += 1;
        Instantiate(monster, transform.position, transform.rotation);
    }
}
