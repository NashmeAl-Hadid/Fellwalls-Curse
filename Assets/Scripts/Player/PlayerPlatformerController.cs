using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;
    public Vector2 move = Vector2.zero;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    private Animator animator;
    public bool facingRight = true;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVeloctiy()
    {
        //Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        //bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        //if (flipSprite)
        //{
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //}

        //Debug.Log((move.x < 0f) && facingRight == true);

        if ((move.x > 0f) && facingRight == false)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            facingRight = true;
        }
        else if ((move.x < 0f) && facingRight == true)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            facingRight = false;
        }

        animator.SetBool("Jump", !grounded);
        animator.SetFloat("Run", Mathf.Abs(velocity.x / maxSpeed));

        targetVelocity = move * maxSpeed;
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if(other.tag == "Ladders" && Input.GetKey(KeyCode.W))
    //    {
    //        other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, maxSpeed);
    //    }
    //    else if (other.tag == "Ladders" && Input.GetKey(KeyCode.S))
    //        {
    //            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -maxSpeed);
    //        }
    //}
}
