using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMov : MonoBehaviour
{

    [SerializeField] private LayerMask WhatIsGround;	// A mask determining what is ground to the character
    [SerializeField] private Transform GroundCheck;	// A position marking where to check if the player is grounded.
    const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    


    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    
    public LevelManager _levelManager;

    public float moveSpeed = 30f;
    public float jumpForce = 3000f;

    private Rigidbody2D rigidbody;
    public Animator animator;

    float horizontalMove = 0f;

    bool jump = false;
    

    public Vector3 respawnPoint;
    
     
       
        
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        _levelManager = FindObjectOfType<LevelManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
    }

    private void checkInput()
    {
        //move with button
        horizontalMove = CrossPlatformInputManager.GetAxisRaw("Horizontal");
               
        rigidbody.velocity = new Vector2(horizontalMove * moveSpeed, rigidbody.velocity.y);

        Vector3 targetVelocity = new Vector2(horizontalMove , rigidbody.velocity.y);
        transform.position += targetVelocity * Time.deltaTime;

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        var Grounded = rigidbody.velocity.y == 0f;

        // If the input is moving the player right and the player is facing left...
        if (horizontalMove > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalMove < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        if (Grounded && !jump)
        { 
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                jump = true;
                rigidbody.AddForce(new Vector2(0, jumpForce));
                animator.SetBool("isJumping", true);
            }
            
        }
        
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        //The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        //This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    animator.SetBool("isJumping", false);
                    jump = false;
                }

            }
        }


        //for reseting game
        if (this.transform.position.y <= -70)
        {
            Application.LoadLevel(0);
        }
              
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        
        if (collision.tag == "fallDetector")
        {
            //transform.position = respawnPoint;
            _levelManager.Respawn();
            

        }
        if(collision.tag == "checkPoint")
        {
             respawnPoint = collision.transform.position;
            
        }
    }

}
//private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
//[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement

//public Joystick joystick;
//protected Joystick joystiick;
//protected joyStick joybutton;

//joystiick = FindObjectOfType<Joystick>();
//joybutton = FindObjectOfType<joyStick>();


//move with joyStick
//if (joystiick.Horizontal >= .2f)
//{
//    horizontalMove = moveSpeed;
//}else if(joystiick.Horizontal <= -.2f)
//{
//    horizontalMove = -moveSpeed;
//}
//else
//{
//    horizontalMove = 0f;
//}
////if (CrossPlatformInputManager.GetButtonDown("Jump"))
////{
////    if (rigidbody.velocity.y == 0)
////        rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
////}
