using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxController : MonoBehaviour
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
    public string currentweapon = "pistol";

    //Player input
    Animator anim;
    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput;
    float turnInput;
    float jumpInput;
    public float attackInput;
    Vector3 velocity = Vector3.zero;
    Bounds bounds;

    //Quaternion used to represent rotations
    public Quaternion TargetRotation { get { return targetRotation; } }

    // === Serializable fields
    [System.Serializable]
    public class MovementSettings {
        public float baseForwardVelocity = 10;
        public float baseJumpVelocity = 25;

        public float forwardVelocity = 10;
        public float rotateVelocity = 100;
        public float jumpVelocity = 25;

        public float distanceToGround = 0.02f;
        public LayerMask ground;
    }

    [System.Serializable]
    public class PhysicsSettings {
        public bool isGrounded;
        public float downAcceleration = 2f;
    }

    [System.Serializable]
    public class InputSettings {
        public float inputDelay = 0.01f;
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
        public string AUTO_ATTACK = "Attack";
    }

    public MovementSettings movement = new MovementSettings();
    public PhysicsSettings physics = new PhysicsSettings();
    public InputSettings input = new InputSettings();

    //Serializable fields END ===

    // Start is called before the first frame update
    void Start()
    {
        bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f));
        targetRotation = transform.rotation;

        hitpoints = GetComponent<Health>();
        anim = GetComponent<Animator>();
        anim.SetFloat("VelocityForward", 0);

        checkRigidBody();

        attackInput = jumpInput = turnInput = forwardInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
      //  updateHPBar();

        if (isAlive)
        {
            GetInput();
            Turn();
            checkAlive();
        }
        else {
            anim.SetTrigger("Dead");
            velocity.z = 0f;
        }
        
    }

    // FixedUpdate called less than Update
    private void FixedUpdate()
    {
        if (isAlive) {
            Run();
            Jump();
            Attack();

            rBody.velocity = transform.TransformDirection(velocity);
        }
    }

    // Gets input from user
    void GetInput() {
        forwardInput = Input.GetAxis(input.FORWARD_AXIS);
        turnInput = Input.GetAxis(input.TURN_AXIS);
        jumpInput = Input.GetAxis(input.JUMP_AXIS);
        attackInput = Input.GetAxis(input.AUTO_ATTACK);
    }

    // === Movement
    void Run() {
        if (Mathf.Abs(forwardInput) > input.inputDelay)
        {
            velocity.z = movement.forwardVelocity * forwardInput;
            anim.SetFloat("VelocityForward", velocity.z);
        }
        else {
            velocity.z = 0;
            anim.SetFloat("VelocityForward", 0);
        }
    }

    void Turn() {
        if (Mathf.Abs(turnInput) > input.inputDelay) 
            targetRotation *= Quaternion.AngleAxis(movement.rotateVelocity * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
    }

    bool Grounded() {
        physics.isGrounded = Physics.Raycast(transform.position, Vector3.down, movement.distanceToGround, movement.ground);
        if (physics.isGrounded)
            anim.SetBool("isGrounded", true);
        else
            anim.SetBool("isGrounded", false);
        return physics.isGrounded;
    }

    void Jump() {
        if ((jumpInput > 0) && Grounded())
            velocity.y = movement.jumpVelocity;
        else if ((jumpInput == 0) && Grounded())
            velocity.y = 0;
        else
            velocity.y -= physics.downAcceleration;
    }

    // Movement END ===

    // Player attack
    void Attack() {
        if (attackInput > 0 && isAllowedToAttack) {
            anim.SetTrigger("Slash");
            if (Equals(currentweapon, "pistol"))
                //TBD
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
