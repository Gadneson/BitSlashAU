using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    private Animator anim;
    private SpriteRenderer sr;


    public bool attacking;
    public float speed;
    private bool FLIP;
    private float lastHDir;
    private float lastVDir;
    private float h;
    private float v;

    public float maxHealth;
    public float maxEnergy;
    public float health;
    public float energy;
    public float damage;
    public float ep5;
    public float hp5;
    public float GCD;
    public float regenTimer;

    public Slider hpBar;
    public Slider enBar;

    // Start is called before the first frame update
    void Start()
    {
        FLIP = false;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        //hpBar = ;
        //enBar = ;
        maxHealth = 100.0f;
        maxEnergy = 100.0f;
        GCD = 1.0f;
        ep5 = 25.0f;
        hp5 = 10.0f;
        regenTimer = 5.0f;

        health = maxHealth;
        energy = maxEnergy;
        hpBar.value = calculateHealth();
        enBar.value = calculateEnergy();
    }

         
    

    // Update is called once per frame
    void Update()
    {
        //INPUT CAPTURING
        // capturing horizontal and vertical inputs, keywords horizontal & vertical monitor wasd/arrowkeys
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //GLOBAL COOLDOWN TIMER
        if (GCD > 0.0f)
        {
            GCD -= Time.deltaTime;
        } else
        {
            GCD = 0.0f;
        }

        //STATS PER 5 TIMER
        if (regenTimer > 0.0f)
        {
            regenTimer -= Time.deltaTime;
        }
        else
        {
            if (energy < maxEnergy) {
                if ((energy + ep5) > maxEnergy) {
                    energy = maxEnergy;
                } else
                {
                    energy += ep5;
                }
                enBar.value = calculateEnergy();
            }

            if (health < maxHealth)
            {
                if ((health + hp5) > maxHealth)
                {
                    health = maxHealth;
                }
                else
                {
                    health += hp5;
                }
                hpBar.value = calculateHealth();
            }
            regenTimer = 5.0f;
        }

        //ATTACK ANIMATION
        if (Input.GetKeyDown("f"))
        {

            if (GCD == 0.0f && energy >= 25.0f)
            {
                anim.SetBool("isAttacking", true);
                energy -= 25.0f;
                enBar.value = calculateEnergy();
                GCD = 1.0f;
                attacking = true;
            }
            
        }
        else {
            anim.SetBool("isAttacking", false);
            attacking = false;
        }

        //MOVEMENT ANIMATIONS/SPRITE FLIPPING
        //input checks to capture last known direction, setting movement bool to true/false, handling sprite flip.
        if (Input.GetKey("w") && !Input.GetKey("a") || Input.GetKey("s") && !Input.GetKey("a") || Input.GetKey("d") && !Input.GetKey("a")) {
            //if w,s or d AND NOT a is being pressed then; set moving to true, ensure sprite is not flipped, capture last known direction
            anim.SetBool("isMoving", true);
            FLIP = false;
            lastHDir = h;
            lastVDir = v;
        }
        else if (Input.GetKey("a")) {
            //if a is pressed under any circumstance then; set moving to true, flip the sprite for anims, capture last known direction
            anim.SetBool("isMoving", true);
            FLIP = true;
            lastHDir = h;
            lastVDir = v;
        }
        else
        {
            //if no keys are being pressed then; 
            //set h & v to 0 (ensures sprite does not move when nothing pressed)
            //Set direction for idle by passing Last known directions into hDIR and vDIR (parameters created in the animator) 
            h = 0.0f;
            v = 0.0f;
            anim.SetFloat("hDIR", lastHDir);
            anim.SetFloat("vDIR", lastVDir);
            anim.SetBool("isMoving", false);
        }
        //setting the direction of run animations based on input which is stored in h & v
        anim.SetFloat("Horizontal", h);
        anim.SetFloat("Vertical", v);

        //using a bool(true/false) above to capture whether the sprite should be flipped, and then here we are actually flipping it based on the FLIP bool
        if (FLIP == true) {
            sr.flipX = true;
        } else
        {
            sr.flipX = false;
        }


        //MOVEMENT CODE
        //defining a new vector 3 with our inputs passed into x/y multiplied by our speed variable, multiplied by deltaTime. Z should always be 0 for 2d games
        Vector3 playerMove = new Vector3(h , v , 0);
        playerMove = playerMove.normalized * speed * Time.deltaTime;
        transform.position = transform.position + playerMove; 


        

    }

    

    public void TakeDamage(float dmgTaken)
    {
        if (health > 0)
        {
            health -= dmgTaken;
            hpBar.value = calculateHealth();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private float calculateHealth()
    {
        return health / maxHealth;
    }

    private float calculateEnergy()
    {
        return energy / maxEnergy;
    }

    private void FixedUpdate()
    {

        
        
        

    }
}
