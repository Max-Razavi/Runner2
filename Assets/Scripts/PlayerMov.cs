using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.

    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded



    public float moveSpeed = 30f;
    public float jumpForce = 3000f;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;

    public Animator animator;

    bool jump = false;
    bool down = false;
    bool ground = false;
    
        
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
    }

    private void checkInput()
    {
        
        var movement = new Vector3(Input.GetAxisRaw("Horizontal"),0);
        transform.position += movement * moveSpeed * Time.deltaTime;


        if (!jump || m_Grounded)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                animator.SetBool("isRunning", true);
                spriteRenderer.flipX = false;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                animator.SetBool("isRunning", false);
            }


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                animator.SetBool("isRunning", true);
                spriteRenderer.flipX = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                animator.SetBool("isRunning", false);
            }
        }
        

       if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            down = true;
            animator.SetBool("isJumping", true);
                       
        }

       //for reset game
       if(this.transform.position.y <= -30)
        {
            Application.LoadLevel(0);
        }
              
    }

    void FixedUpdate()
    {
        //rigidbody.velocity = new Vector2(5f, rigidbody.velocity.y);

        if(jump && ground)
        {
            
            rigidbody.AddForce(new Vector2(500, Mathf.Abs(jumpForce)));
            jump = false;
            ground = false;
        }

        if (down)
        {
            rigidbody.AddForce(new Vector2(0, -500f));
            down = false;
        }


        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    animator.SetBool("isJumping", false);
                    //animator.SetBool("isRunning", false);
                }
                    
                
            }
        }



    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        
            ground = true;
            //animator.speed = 1;
      
    }
    


}
