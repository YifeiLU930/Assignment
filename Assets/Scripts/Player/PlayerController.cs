using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    public float speed;
    public float jumpForce;

    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;

    Coroutine jumpForceChange;
    
   
   
    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }

    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        yield return new WaitForSeconds(3.0f);

        jumpForce /= 2;
        jumpForceChange = null;

    }

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        
        {
            speed = 6.0f;
            Debug.Log("Speed Not Set");
        }
        
        if (jumpForce <= 0)
        
        {
            jumpForce = 300;
            Debug.Log("jumpForce Not Set");
        }
        
        if (!groundCheck)
       
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("GroundCheck Not Set");
         
        }
        
        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            Debug.Log("groundCheckRadius Not Set");
        }
    }


    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        bool MouseInput = Input.GetButtonDown("Fire1");
        bool MouseInput2 = Input.GetButtonDown("Fire2");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }
       
        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

       
        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("LeftMouseClick", MouseInput);
        anim.SetBool("RightMouseClick", MouseInput2);
        //Flip

        if (hInput != 0)
            sr.flipX = (hInput < 0);

        if (isGrounded)
            rb.gravityScale = 1;

         
    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish"))
        {
            collision.gameObject.GetComponentInParent<EnemyWalker>().Squish();

            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (collision.CompareTag("Checkpoint"))
        {
            GameManager.instance.currentLevel.UpdateCheckpoint(collision.gameObject.transform);

        }


    }

}

