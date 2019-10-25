using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 300;
    public int extraHealthMultiplier = 1;
    public int currentHealth;
    public int minimumHealth = 0;
    public bool isDead;
    public int currentLevel;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        Invoke("resetHealth", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        checkLife();
    }

    public int GetHealth() { return this.currentHealth; }

    public void AddHealth(int replenish) {
        if (!isFull())
            this.currentHealth += replenish;
        if (currentHealth > startingHealth)
            resetHealth(); 
    }

    public void DecrementHealth(int damageTaken) { this.currentHealth -= damageTaken; }

    public bool isFull() {
        if (startingHealth > currentHealth)
            return false;
        return true;
    }

    public void resetHealth() {
        GameObject levelText = GameObject.FindWithTag("LevelCounter");
       // currentLevel = levelText.GetComponent<LevelScript>().levelVariation;

        if (gameObject.tag.Equals("Player"))
            this.currentHealth = startingHealth;
        else
        {
            this.currentHealth += (currentLevel * extraHealthMultiplier);
            if (currentHealth > startingHealth)
                currentHealth = startingHealth;
        }
    }

    void checkLife() { if (currentHealth <= 0) isDead = true; }

}
