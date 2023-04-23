using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1;
    public float jumpForce = 1;

    public bool facingRight = true;
    public bool canMove = true;
    public bool isMoving = false;

    private bool isHit = false; //playerdamageKnock

    [HideInInspector]
    public bool isGrounded;

    private Transform groundDetection;
    public float checkRadius;
    public LayerMask ground;
    [HideInInspector] public bool isClimbing;

    private float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    [HideInInspector] public float movement;
    [HideInInspector] public float inputVertical;
    public float distance;
    public LayerMask whatIsLadder;

    private Rigidbody2D rb2d;
    //private CapsuleCollider2D cc;

    private Animator animator;

    public PlayerCombat playerCombat;
    public PlayerSlide slideController;

    private Vector2 colliderSize;

     private bool isKnockbackHappening = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        groundDetection = gameObject.transform.Find("groundDetection");

        //cc = GetComponent<CapsuleCollider2D>();
        //colliderSize = cc.size;

        isKnockbackHappening = false;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundDetection.position, checkRadius, ground);
        //Debug.Log("knockback happening: " + isKnockbackHappening);

        if (slideController.Slide == false && canMove == true && playerCombat.isAttacking == false)
        {
            movement = Input.GetAxis("Horizontal");
            
            if (isKnockbackHappening == false)
            {
                transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
            }

            if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
            {
                if (isKnockbackHappening == false)
                {
                    isJumping = true;
                    jumpTimeCounter = jumpTime;
                    rb2d.velocity = Vector2.up * jumpForce;
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (isKnockbackHappening == false)
                {
                    if (jumpTimeCounter > 0 && isJumping == true)
                    {
                        rb2d.velocity = Vector2.up * jumpForce;
                        jumpTimeCounter -= Time.deltaTime;
                    }
                    else
                    {
                        isJumping = false;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }

            if ((movement > 0) && facingRight == false)
            {
                //spriteRenderer.flipX = !spriteRenderer.flipX;
                //facingRight = true;
                FlipSprite();
            }
            else if ((movement < 0) && facingRight == true)
            {
                //spriteRenderer.flipX = !spriteRenderer.flipX;
                //facingRight = false;
                FlipSprite();
            }

            animator.SetBool("Jump", !isGrounded);
            animator.SetFloat("Run", Mathf.Abs(movement / movementSpeed));
        }

        if (movement != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if(hitInfo.collider != null)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                isClimbing = true;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                isClimbing = false;
            }
        }

        if(isClimbing == true && hitInfo.collider != null)
        {
            if (isKnockbackHappening == false)
            {
                inputVertical = Input.GetAxisRaw("Vertical");
                rb2d.velocity = new Vector2(rb2d.velocity.x, inputVertical * movementSpeed);
                rb2d.gravityScale = 0;
            }
        }
        else
        {
            if (isKnockbackHappening == false)
            {
                rb2d.gravityScale = 7.5f;
            }
        }

        if (isHit == true)
        {
            StartCoroutine(DamageColor());
        }

        SlopeCheck();

        //if (Input.GetButtonDown("Jump") && Mathf.Abs(rig.velocity.y) < 0.001f)
        //{
        //    rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //}
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        
    }
    private void SlopeCheckVertical(Vector2 checkPos)
    {

    }

    public IEnumerator Knockback(float _duration, float _knockbackPower, Transform _obj, bool _isGrounded)
    {
        float timer = 0;
        isKnockbackHappening = true;
        isHit = true;
        while (_duration > timer && _obj != null)
        {
            timer += Time.deltaTime;
            Vector2 direction = (_obj.transform.position - this.transform.position).normalized;    

            if (_isGrounded == true)
            {
                rb2d.velocity = new Vector2(Mathf.Round(-direction.x) * _knockbackPower, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Round(-direction.y) * (_knockbackPower * 1.5f));
            }

            yield return null;
        }

        isKnockbackHappening = false;
    }

    private IEnumerator DamageColor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        isHit = false;
    }

    void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            rb2d.velocity = Vector3.zero;
            StartCoroutine(Knockback(0.3f, 2f, collision.gameObject.transform, isGrounded));

            isHit = true;
        }
    }
}
