using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponManagement : MonoBehaviour
{
    public GameObject Pistol, Rifle, Maule;
    public string currentWeapon;
    public float weaponSwitchDelay = 1.25f;
    public bool canSwitch;
    public KayaAttack kaya;
    public AudioSource switchSound;

    //Weapon Display UI
    GameObject PistolUI, RifleUI, MauleUI;


    // Start is called before the first frame update
    void Start()
    {
        //Display Sword UI by default
        PistolUI = GameObject.FindGameObjectWithTag("PistolUI");
        RifleUI = GameObject.FindGameObjectWithTag("RifleUI");
        MauleUI = GameObject.FindGameObjectWithTag("MauleUI");

        //Start with pistol
        WeaponOption1();

        canSwitch = true;
        switchSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        checkCurrentWeapon();
        switchWeapon();
    }

    void checkCurrentWeapon() {
        if (Pistol.activeSelf)
            currentWeapon = "Pistol";
        else if (Rifle.activeSelf)
            currentWeapon = "Rifle";
        else if (Maule.activeSelf)
            currentWeapon = "Maule";
        kaya.updateWeapon(currentWeapon);
    }

    void switchWeapon() {
        //if the player presses 1, 2, or 3 and is allowed to switch
        if ((Input.GetKeyDown(KeyCode.Alpha1)  || (Input.GetKeyDown(KeyCode.Alpha2)) || (Input.GetKeyDown(KeyCode.Alpha3))) && canSwitch) {

            if ((Input.GetKeyDown(KeyCode.Alpha1)) && !Pistol.activeSelf)
            {
                weaponStatus();
                WeaponOption1();
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha2)) && !Rifle.activeSelf)
            {
                weaponStatus();
                WeaponOption2();
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha3)) && !Maule.activeSelf)
            {
                weaponStatus();
                WeaponOption3();
            }
        }
    }

    void weaponStatus() {
        switchSound.Play();
        canSwitch = false;
        Invoke("setSwitchTrue", weaponSwitchDelay);
    }

    void WeaponOption1() {
        Pistol.SetActive(true);
        Rifle.SetActive(false);
        Maule.SetActive(false);

        //UI
        PistolUI.SetActive(true);
        RifleUI.SetActive(false);
        MauleUI.SetActive(false);
    }

    void WeaponOption2() {
        Pistol.SetActive(false);
        Rifle.SetActive(true);
        Maule.SetActive(false);

        //UI
        PistolUI.SetActive(false);
        RifleUI.SetActive(true);
        MauleUI.SetActive(false);
    }

    void WeaponOption3() {
        Pistol.SetActive(false);
        Rifle.SetActive(false);
        Maule.SetActive(true);

        //UI
        PistolUI.SetActive(false);
        RifleUI.SetActive(false);
        MauleUI.SetActive(true);
    }

    void setSwitchTrue() { canSwitch = true; }

}
