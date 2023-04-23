using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    private Animator playerAnimator;

    private Rigidbody2D rb2d;
    private bool facingRight = true;

    [HideInInspector]public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;
    private bool isHit = false;
    private bool isKnockbackHappening = false;


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
        if(isKnockbackHappening == false)
        {
            rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
        } 
    }

    private void Update()
    {
        //Debug.Log("Grounded:" + isGrounded);
        playerAnimator.SetFloat("Moving", Mathf.Abs(moveInput));

        if(facingRight==false && moveInput>0)
        {
            FlipSprite();
        }
        else if(facingRight==true && moveInput<0)
        {
            FlipSprite();
        }

        if(isGrounded==false)
        {
            extraJumps = extraJumpsValue;
            playerAnimator.SetBool("Jump", true);
        }
        else
        {
            playerAnimator.SetBool("Jump", false);
        }

        if(Input.GetKeyDown(KeyCode.Space)&& extraJumps>0)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            extraJumps--;
           
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded==true)
        {
            rb2d.velocity = Vector2.up * jumpForce;         
        }

        if(isHit == true)
        {
            StartCoroutine(DamageColor());
        }
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

    public IEnumerator Knockback(float _duration, float _knockbackPower, Transform _obj, bool _isGrounded)
    {
        float timer = 0;
        isKnockbackHappening = true;
        isHit = true;
        while (_duration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (_obj.transform.position - this.transform.position).normalized;
            //rb2d.AddForce(-direction * _knockbackPower);
            //rb2d.velocity =  direction * _knockbackPower;
            if(_isGrounded == true)
            {
                rb2d.velocity = new Vector2(Mathf.Round(-direction.x) * _knockbackPower, rb2d.velocity.y);
                Debug.Log("direction" + Mathf.Round(-direction.x));
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Round(-direction.y) * (_knockbackPower * 1.5f));
            }
            
            yield return null;     
        }

        isKnockbackHappening = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player Hit");
            rb2d.velocity = Vector3.zero;
            StartCoroutine(Knockback(0.3f, 2f, collision.gameObject.transform, isGrounded));
            //if (isGrounded == false)
            //{
            //    StartCoroutine(Knockback(0.3f, 20f, collision.gameObject.transform));
            //}
            //else
            //{
            //    StartCoroutine(Knockback(5f, 300f, collision.gameObject.transform));
            //}
            isHit = true;
        }
    }
}

