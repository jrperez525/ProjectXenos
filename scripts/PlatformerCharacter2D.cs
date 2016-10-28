using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{ 
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        //Unlocking abilities
        private bool unlockJump = false;
        private bool unlockJumpDistance = false;
        public float unlockJumpPosition = 500f;
        private bool unlockPropeller = false;
        private bool unlockShoot = true;
        public float unlockShootDistance = 5f;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        //Wall Jumping Variable
        public bool wallSliding;
        public Transform wallCheckPoint;
        public bool wallCheck;
        public LayerMask wallLayerMask;
        public float wallSlide = 0.5f;
        public float jumpHeight = 2f;
        public float jumpWidthLeft = -2f;
        public float jumpWidthRight = 2f;
        public float wallJumpForce = 500f;

        //Propeller Variables
        public float propellerFallSpeed = -3f;
        private bool propeller = false;

        //Shooting Variables
        public GameObject bullet;
        public Transform shootRight;
        private int bulletcounter = 3;
        public float bulletSpeed = 100;
        private bool facingRight = true;
        private bool facingLeft;

        void OnLevelWasLoaded(int level)
        {
            if (level == 2)
            {
                unlockJump = true;
            }
            else if (level == 3)
            {
                unlockPropeller = true;
            }
        }

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            if (transform.position.x > unlockJumpPosition)
            {
                unlockJumpDistance = true;
            }

            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
                GetComponent<Rigidbody2D>().gravityScale = 3;
                m_Anim.SetBool("Propeller", false);
            }

            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

         //   if (unlockJump)
              //  {
                if (!m_Grounded)
                {
                    wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);

                    if (m_FacingRight && Input.GetAxis("Horizontal") > 0.1f || !m_FacingRight && Input.GetAxis("Horizontal") < 0.1f)
                    {
                        if (wallCheck)
                        {
                            HandleWallSliding();
                        }
                    }
              //  if (unlockPropeller)
             //   {
                     if (Input.GetKey(KeyCode.LeftShift))
                     {
                      m_Anim.SetBool("Propeller", true);
                      propeller = true;
                      PropellerAbility();
                 // }
               // }
            }

            if (wallCheck == false || m_Grounded)
            {
                wallSliding = false;
            }
           }

            if (unlockShoot)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    m_Anim.SetBool("Shoot", true);
                    Shooting();
                }
            }

            if (bulletcounter < 3)
            {
                bulletcounter++;
            }
        }
        void Shooting()
        {
            if (bulletcounter > 0)
            {
                if (facingLeft)
                {
                  
                    GameObject bulletClone;
                    // bulletClone = Instantiate(bullet, shootRight.transform.position, shootRight.transform.rotation) as GameObject;
                    bulletClone = Instantiate(bullet) as GameObject;
                    bulletClone.transform.position = shootRight.transform.position;
                    bulletClone.GetComponent<Rigidbody2D>().AddForce(Vector3.left * bulletSpeed);
                    bulletcounter--;
                    m_Anim.SetBool("Shoot", false);
                }
                if (facingRight)
                {
                   
                    GameObject bulletClone;
                    //   bulletClone = Instantiate(bullet, shootRight.transform.position, shootRight.transform.rotation) as GameObject;
                    bulletClone = Instantiate(bullet) as GameObject;
                    bulletClone.transform.position = shootRight.transform.position;
                    bulletClone.GetComponent<Rigidbody2D>().AddForce(Vector3.right * bulletSpeed);
                    bulletcounter--;
                    m_Anim.SetBool("Shoot", false);
                }
            }
        }

        void HandleWallSliding()
        {
          //  if (unlockJump && unlockJumpDistance)
           //  {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, wallSlide);

            wallSliding = true;

            if (Input.GetButtonDown("Jump") && wallSliding)
            {
                if (m_FacingRight)
                {
                    m_Rigidbody2D.AddForce(new Vector2(jumpWidthLeft * wallJumpForce, jumpHeight * wallJumpForce / 4));
                    wallSliding = false;
                }
                else
                {
                    m_Rigidbody2D.AddForce(new Vector2(jumpWidthRight * wallJumpForce, jumpHeight * wallJumpForce / 4));
                    wallSliding = false;
                }
            }
         // }
        }

       void PropellerAbility()
        {
            if (Input.GetKey(KeyCode.LeftShift) && propeller)
            {
               
                m_Rigidbody2D.gravityScale = 0;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, propellerFallSpeed);
            }
            else if(m_Grounded)
            {
                m_Anim.SetBool("Propeller", false);
                m_Anim.SetBool("Ground", true);
            }
        }

        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                if (m_Grounded)
                {
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
                }
                else
                {
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
                }

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                    facingRight = true;
                    facingLeft = false;
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                    facingLeft = true;
                    facingRight = false;
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground") && !wallSliding)
            {
                // Add a vertical force to the player.

                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
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
    }
}
