using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    public GameObject Sword, Maule, Staff;
    string currentWeapon;
    public float weaponSwitchDelay = 1.25f;
    public bool canSwitch;
    public KayaController kaya;
    public AudioSource switchSound;


    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Tab) && canSwitch) {
            switchSound.Play();
            canSwitch = false;
            Invoke("setSwitchTrue", weaponSwitchDelay);

            if (Sword.activeSelf)
            {
                Sword.SetActive(false);
                Maule.SetActive(true);
            }
            else if (Maule.activeSelf)
            {
                Maule.SetActive(false);
                Staff.SetActive(true);
            }
            else if (Staff.activeSelf) {
                Staff.SetActive(false);
                Sword.SetActive(true);
            }
        }
    }

    void setSwitchTrue() { canSwitch = true; }

}
