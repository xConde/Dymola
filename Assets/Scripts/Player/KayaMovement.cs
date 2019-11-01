using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KayaMovement : MonoBehaviour
{
    public float speed = 6f;
    public float startingSpeed = 6f;

    public float startingEnergy = 100;
    public float currentEnergy = 100;
    public Slider EnergySlider;

    Vector3 movement;

    bool hasSpeedPowerup = false;
    bool isRunning;

    //animiation
    Animator anim;
    Rigidbody rBody;
    int floorMask;
    float camRayLength = 100f;

    //player input
    float forwardInput;
    float turnInput;
    float jumpInput;

    void Awake()
    {
        isRunning = false;
        currentEnergy = startingEnergy;
        floorMask = LayerMask.GetMask("Floor");

        //Animiation
        anim = GetComponent<Animator>();
        checkRigidBody();
    }

    private void Update()
    {

        if (((Input.GetKeyDown(KeyCode.LeftShift)) && currentEnergy >= 25 && !hasSpeedPowerup && !isRunning))
        {
            isRunning = true;
            currentEnergy -= 25;
            EnergySlider.value = currentEnergy;
            speed = 16;
            Invoke("resetSpeed", 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !hasSpeedPowerup)
            resetSpeed();

        currentEnergy += 3 * Time.deltaTime;

        setSliderBar();
    }

    void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        Move(turnInput, forwardInput);
        Turn();
        Animiating(forwardInput, turnInput);
    }


    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        anim.SetFloat("VelocityForward", v);
        anim.SetFloat("VelocitySide", h);
        rBody.MovePosition(transform.position + movement);
    }

    void Turn()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rBody.MoveRotation(newRotation);
        }
    }

    // Adds components to rBody, if something fails it will printout in the debugger
    void checkRigidBody()
    {
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("Add rigidbody to the object");
    }

    void Animiating(float h, float v)
    {
        bool moving = h != 0f || v != 0f;

        anim.SetBool("IsMoving", moving);
    }

    public void powerUpSpeed(float duration)
    {
        hasSpeedPowerup = true;
        currentEnergy = startingEnergy;
        EnergySlider.value = currentEnergy;
        float speedMultiplier = Random.Range(1, 3);

        speed *= speedMultiplier;
        Invoke("resetSpeed", duration);
    }

    private void resetSpeed()
    {
        speed = startingSpeed;
        hasSpeedPowerup = false;
        isRunning = false;
    }

    void setSliderBar()
    {
        EnergySlider.value = currentEnergy;
    }
}
