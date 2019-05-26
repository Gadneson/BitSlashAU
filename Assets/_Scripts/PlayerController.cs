using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    private Animator anim;
    private SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

         
    

    // Update is called once per frame
    void Update()
    {

        

    }

    private void FixedUpdate()
    {

        //depending on which input, set idle->running variable to true, and alter blend tree x/y parameters
        if (Input.GetKey("w"))
        {
            sr.flipX = false;
            //isMoving when set to true, will transition the players animator from idling to running
            anim.SetBool("isMoving", true);
            //flipX flips the x of the sprite renderer, used when the player should be animating left (mirrored)
            //sr.flipX = true;
            // think of these 2 values as X/Y coordinates, origin of the coordinates being the center having 8 possible directions.
            // right is +, left is -, e.g moving diagonally bottom left would = X
            anim.SetFloat("Horizontal", 0.0f);
            anim.SetFloat("Vertical", 1.0f);
        }
        else if (Input.GetKey("a"))
        {
            anim.SetBool("isMoving", true);
            sr.flipX = true;
            anim.SetFloat("Horizontal", -1.0f);
            anim.SetFloat("Vertical", 0.0f);
        }
        else if (Input.GetKey("s"))
        {
            anim.SetBool("isMoving", true);
            sr.flipX = false;
            anim.SetFloat("Horizontal", 0.0f);
            anim.SetFloat("Vertical", -1.0f);
        }
        else if (Input.GetKey("d"))
        {
            anim.SetBool("isMoving", true);
            sr.flipX = false;
            anim.SetFloat("Horizontal", 1.0f);
            anim.SetFloat("Vertical", 0.0f);
        }
        else //else statement for when no keys are being pressed
        {
            
            //isMoving when set to false, will transition the players animator from running to idling
            anim.SetBool("isMoving", false);


            sr.flipX = false;

        }

       
    }
}
