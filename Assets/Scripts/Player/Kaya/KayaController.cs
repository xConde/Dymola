using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KayaController : MonoBehaviour
{
    //Max checks
    public Health hitpoints;
    public bool isAllowedToAttack = true;
    public bool isAlive = true;

    //Damage
    public bool isInvincible;
    public int damageAmount = 50;
    public float takenDamageCooldown = 1.5f;
    public float timeSinceTakenDamage = 0.0f;

    //Weapon
    public string currentweapon = "Sword";

    //raycasting
    float camRayLength = 100f;
    int floorMask;
    public LayerMask ground;

    //movement
    public float speed = 12f;

    //Player input
    Animator anim;
    Rigidbody rBody;
    float forwardInput;
    float turnInput;
    float jumpInput;
    public float attackInput;
    Vector3 velocity = Vector3.zero;
    Bounds bounds;


    // === Serializable fields
    [System.Serializable]
    public class MovementSettings {
        Vector3 move;
        public float baseForwardVelocity = 10;
        public float baseJumpVelocity = 25;

        public float forwardVelocity = 10;
        public float sideVelocity = 10;
        public float rotateVelocity = 100;
        public float jumpVelocity = 25;

    }

    [System.Serializable]
    public class InputSettings {
        public float inputDelay = 0.01f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string AUTO_ATTACK = "Fire1";
    }

    public MovementSettings movement = new MovementSettings();
    public InputSettings input = new InputSettings();

    //Serializable fields END ===

    // Start is called before the first frame update
    void Awake()
    {
        //Create a lawyer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        //Animiation
        anim = GetComponent<Animator>();
        checkRigidBody();


        //Movement
        attackInput = jumpInput = turnInput = forwardInput = 0;
        anim.SetFloat("VelocityForward", 0);

        //bounds
        bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f));

        //hitpoints
        hitpoints = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
      //  updateHPBar();     
    }

    // FixedUpdate called less than Update
    private void FixedUpdate()
    {
        forwardInput = Input.GetAxis(input.FORWARD_AXIS);
        turnInput = Input.GetAxis(input.TURN_AXIS);
        attackInput = Input.GetAxis(input.AUTO_ATTACK);

        if (isAlive) {
            Move(turnInput, forwardInput);
            Turn();
            Attack();
            Animiating(forwardInput, turnInput);
        }
    }



    // === Movement
    void Move(float h, float v) {
        velocity.Set(h, 0f, v);
        velocity = velocity.normalized * speed * Time.deltaTime;
        anim.SetFloat("VelocityForward", v);
        anim.SetFloat("VelocitySide", h);
        rBody.MovePosition(transform.position + velocity);
    }

    void Turn() {
        //create a ray from the mouse cursor on screen in the direction of the camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //create a raycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        //perform the raycast and if it hits something on the floor layer
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rBody.MoveRotation(newRotation);
        }
    }
    // Movement END ===

    // Player attack
    void Attack() {
        if (attackInput > 0 && isAllowedToAttack) {
            anim.SetTrigger("Slash");
            if (Equals(currentweapon, "staff"))
              //  MagicScript.Mage();
            isAllowedToAttack = false;
            Invoke("canDamage", 0.85f);
        }
    }

    // === Damage to player
    private void OnCollisionEnter(Collision collision)
    {
        if (!isInvincible) {
            if (collision.gameObject.tag.Equals("EnemyProjectile")) {
                EnableDamageCoolDown();
                Debug.Log("Took damage from " + collision.gameObject + " -20HP ");
                hitpoints.DecrementHealth(20);
            }
        }
    }

    public void HurtPlayer(int EnemyPower) {
        if (!isInvincible)
        {
            EnableDamageCoolDown();
            Debug.Log("Took damage from an enemy taking -" + EnemyPower + "HP");
            hitpoints.DecrementHealth(EnemyPower);
        }
    }
    
    void EnableDamageCoolDown() {
        isInvincible = true;
        Invoke("notInvincible", takenDamageCooldown);
    }

    // Damage to player END === 

    void Animiating(float h, float v) {
        bool moving = h != 0f || v != 0f;

        anim.SetBool("IsMoving", moving);
    }

    // === Resets & Updates
    void resetSpeed() { movement.forwardVelocity = movement.baseForwardVelocity; }
    void resetJump() { movement.jumpVelocity = movement.baseJumpVelocity; }
    void canDamage() { isAllowedToAttack = true;  }
    public void updateWeapon(string weaponName) { currentweapon = weaponName; }

    /**
    // === Hitpoints Bar
    void updateHPBar() { hitpointsBar.fillAmount = ratioHP(hitpoints.currentHealth, hitpoints.minimumHealth, hitpoints.startingHealth, 0f, 1f);  }
    float ratioHP(float currentHealth, float inMin, float inMax, float outMin, float outMax) {
        return (currentHealth - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    // Hitpoints Bar END ===
    */

    void checkAlive() {
        if (hitpoints.currentHealth <= 0) {
            isAlive = false;
            anim.SetTrigger("Dead");
            gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0f, 2f, 0f);
        }
    } 

    // Resets & Updates END ===



    // Adds components to rBody, if something fails it will printout in the debugger
    void checkRigidBody() {
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("Add rigidbody to the object");
    }



}
