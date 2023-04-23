using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Billy : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    public Animator playerAnimator;

    private Rigidbody2D rb2d;
    private bool facingRight = true;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;


    private void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        playerAnimator = this.GetComponent<Animator>();
        extraJumps = extraJumpsValue;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); 
        moveInput = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
    }

    private void Update()
    {
        playerAnimator.SetFloat("Moving", Mathf.Abs(moveInput));

        if (facingRight == false && moveInput > 0)
        {
            FlipSprite();
        }
        else if (facingRight == true && moveInput < 0)
        {
            FlipSprite();
        }

        if (isGrounded == false)
        {
            extraJumps = extraJumpsValue;
            playerAnimator.SetBool("Jump", true);
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            extraJumps--;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb2d.velocity = Vector2.up * jumpForce;
        }
    }

    void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
