using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponManagement : MonoBehaviour
{
    public GameObject Sword, Maule, Staff;
    string currentWeapon;
    public float weaponSwitchDelay = 1.25f;
    public bool canSwitch;
    public KayaController kaya;
    public AudioSource switchSound;

    //Weapon Display UI
    GameObject SwordUI, MauleUI, StaffUI;


    // Start is called before the first frame update
    void Start()
    {
        //Display Sword UI by default
        SwordUI = GameObject.FindGameObjectWithTag("SwordUI");
        SwordUI.SetActive(true);

        MauleUI = GameObject.FindGameObjectWithTag("MauleUI");
        MauleUI.SetActive(false);

        StaffUI = GameObject.FindGameObjectWithTag("StaffUI");
        StaffUI.SetActive(false);

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
        if (Sword.activeSelf)
            currentWeapon = "Sword";
        else if (Maule.activeSelf)
            currentWeapon = "Maule";
        else if (Staff.activeSelf)
            currentWeapon = "Staff";
        kaya.updateWeapon(currentWeapon);
    }

    void switchWeapon() {
        //if the player presses 1, 2, or 3 and is allowed to switch
        if ((Input.GetKeyDown(KeyCode.Alpha1)  || (Input.GetKeyDown(KeyCode.Alpha2)) || (Input.GetKeyDown(KeyCode.Alpha3))) && canSwitch) {

            if ((Input.GetKeyDown(KeyCode.Alpha1)) && !Sword.activeSelf)
            {
                weaponStatus();
                Sword.SetActive(true);
                Maule.SetActive(false);
                Staff.SetActive(false);

                //UI
                SwordUI.SetActive(true);
                MauleUI.SetActive(false);
                StaffUI.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha2)) && !Maule.activeSelf)
            {
                weaponStatus();
                Sword.SetActive(false);
                Maule.SetActive(true);
                Staff.SetActive(false);

                //UI
                SwordUI.SetActive(false);
                MauleUI.SetActive(true);
                StaffUI.SetActive(false);
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha3)) && !Staff.activeSelf)
            {
                weaponStatus();
                Sword.SetActive(false);
                Maule.SetActive(false);
                Staff.SetActive(true);

                //UI
                SwordUI.SetActive(false);
                MauleUI.SetActive(false);
                StaffUI.SetActive(true);
            }
        }
    }

    void weaponStatus() {
        switchSound.Play();
        canSwitch = false;
        Invoke("setSwitchTrue", weaponSwitchDelay);
    }

    void setSwitchTrue() { canSwitch = true; }

}
