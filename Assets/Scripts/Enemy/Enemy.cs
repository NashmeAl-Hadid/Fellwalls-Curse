using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Animator enemyAnimator;
    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public RaycastHit2D CornerInfo;
    [HideInInspector] public RaycastHit2D targetInfoFront;
    [HideInInspector] public RaycastHit2D targetInfoBack;
    [HideInInspector] public bool chase;
    [HideInInspector] public float jumpPowerX;
    [HideInInspector] public float jumpPowerY;

    public bool canRangeAttack = false;
    public bool canMeleeAttack = false;
    public bool canChase;
    //public bool canJump;

    private Transform playerDetection = null;
    private Transform cornerDetection = null;
    private Transform groundDetection = null;
    private Transform target; //fix later if player in sight

    private float speed;
    public float speedValue = 5f;
    public float cornerDetectRange = 5f;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool facingRight;

    private bool isJumpPossible;
    private float distanceToTargetX;
    private float distanceToTargetY;

    public float chaseRangeX = 1f;
    public float chaseRangeY = 0.5f;
    public float meleeAttackRangeX = 1f;
    public float meleeAttackRangeY = 0.5f;
    public float rangeMaxRangeX = 5f;
    public float rangeMinRangeX = 1f;
    public float playerDetectRange;
    public LayerMask layerMaskDetection;
    [HideInInspector]public bool isAttacking;
    [HideInInspector]public bool targetInBack;
    public bool isAttackCancelable=true;
    public bool isMovementStopped = false;
    [HideInInspector]public float meleeAttackTimer = 0;
    public float meleeAttackTimerValue = 0;
    [HideInInspector] public float rangeAttackTimer = 0;
    public float rangeAttackTimerValue = 0;

    [HideInInspector] public bool isKnockbackHappening;
    private bool isHit = false;
    public bool isDead = false;

    private void Awake()
    {
        enemyAnimator = this.GetComponent<Animator>();
        rb2d = this.GetComponent<Rigidbody2D>();

        if (GameObject.FindGameObjectWithTag("Player")!= null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
      
        playerDetection = gameObject.transform.Find("Player_Detection").transform;
        cornerDetection = gameObject.transform.Find("Corner_Detection").transform;
        groundDetection = gameObject.transform.Find("Ground_Detection").transform;

        meleeAttackTimer = 0;
        rangeAttackTimer = 0;
        isKnockbackHappening = false;
    }

    private void Start()
    {
        chase = false;
        isAttacking = false;
        speed = speedValue;
    }

    private void Update()
    {
       //Debug.Log("x range: " + Mathf.Abs(distanceToTargetX));
       //Debug.Log("can melee attack: " + MeleeAttackCheck());
       // Debug.Log("attack timer" + meleeAttackTimer);
       // Debug.Log("chase check" + ChaseCheck());

        CornerInfo = Physics2D.Raycast(cornerDetection.position, Vector2.down, cornerDetectRange);
        targetInfoFront = Physics2D.Raycast(playerDetection.position, playerDetection.transform.right, playerDetectRange,layerMaskDetection);


        if(isMovementStopped==true)
        {
            speed = 0;
        }
        else
        {
            speed = speedValue;
        }

        if (canRangeAttack == true)
        {
            targetInfoBack = Physics2D.Raycast(playerDetection.position, -(playerDetection.transform.right), playerDetectRange, layerMaskDetection);
        }
        if(target!=null)
        {
            distanceToTargetX = target.position.x - playerDetection.transform.position.x;
            distanceToTargetY = target.position.y - playerDetection.transform.position.y;
        }
     
        Vector2 targetCheckforward = -transform.TransformDirection(Vector2.right) * playerDetectRange;
        Debug.DrawRay(playerDetection.position, targetCheckforward, Color.green);


        Debug.Log(RangeAttackCheck());

        if (isGrounded == false)
        {
            //enemyAnimator.SetBool("Jump", true);
            //whileJumping = true;
        }
        else
        {
            //enemyAnimator.SetBool("Jump", false);
           // whileJumping = false;
        }

        GroundCheck();

        if(canChase==true)
        {
            chase = ChaseCheck();
        }  
        
        if(meleeAttackTimer > 0)
        {
            meleeAttackTimer -= Time.deltaTime;
        }

        if (rangeAttackTimer > 0)
        {
            rangeAttackTimer -= Time.deltaTime;
        }

        if (isHit == true)
        {
            StartCoroutine(DamageColor());
        }
    }

    public bool TargetDetected()
    {
        if (targetInfoFront.collider == true && targetInfoFront.transform.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void makeAttackFalse()
    {
        isAttacking = false;
        meleeAttackTimer = meleeAttackTimerValue;
    }

    public bool MeleeAttackCheck()
    {
        if (targetInfoFront.collider != null && target!=null)
        {
            if (targetInfoFront.transform.CompareTag("Player"))
            {
                if (Mathf.Abs(distanceToTargetX) < meleeAttackRangeX && Mathf.Abs(distanceToTargetY) < meleeAttackRangeY)
                {

                   return true;

                }
                else
                {
                    return false;
                }     
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool RangeAttackCheck()
    {
        if(canRangeAttack==false)
        {
            return false;
        }    

        if (targetInfoFront.collider != null && targetInfoFront.transform.CompareTag("Player"))
        {
            if (Mathf.Abs(distanceToTargetX) < rangeMaxRangeX && Mathf.Abs(distanceToTargetX) > rangeMinRangeX)
            {
                return true;
            }
                
        }
        if (targetInfoBack.collider != null && targetInfoBack.transform.CompareTag("Player"))
        {
            if (Mathf.Abs(distanceToTargetX) < rangeMaxRangeX && Mathf.Abs(distanceToTargetX) > rangeMinRangeX)
            {
                targetInBack = true;
                return true;
            }                       
        }
        else
        {
            targetInBack = false;
            return false;
        }

        return false;
    }

    public void NormalPatrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public bool NormalPatrolCheck()
    {
        int changeToGoIdle = 25;
        if (CornerInfo.collider == false)
        {
            Flip();
            changeToGoIdle = Random.Range(1, 25);
            if (changeToGoIdle < 10)
            {
                return false;

            }

            return true;
            
        }
        else if (CornerInfo.collider == true && CornerInfo.transform.gameObject.tag != "Ground")
        {
            Flip();
            changeToGoIdle = Random.Range(1, 25);
            if (changeToGoIdle < 10)
            {
                return false;
            }

            return true;
        }

        return true;
    }

    private bool ChaseCheck()
    {   
       if(target!=null)
       {

            if (CornerInfo.collider == true && CornerInfo.transform.gameObject.tag == "Ground")
            {
                if ((Mathf.Abs(distanceToTargetX) < chaseRangeX && Mathf.Abs(distanceToTargetX) > 2.1f))
                {
                    if (Mathf.Abs(distanceToTargetY) < chaseRangeY)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
       }
       else
       {
            return false;
       }
    }

    public void Chasing()
    {   if(CornerInfo.collider == true && CornerInfo.transform.gameObject.tag == "Ground")
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    
        if ((target.position.x < transform.position.x)  && facingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = false;
        }

        if ((target.position.x > transform.position.x)  && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundDetection.position, groundCheckRadius, whatIsGround);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("JumpTrigger") && isJumpPossible == false)
        //{
        //    int jumpChance = Random.Range(1, 25);
        //    JumpTriggerTest jumpTriggerScript = other.GetComponent<JumpTriggerTest>();

        //    if (jumpChance < 25)
        //    {
        //        jumpPowerX = jumpTriggerScript.jumpPowerX;
        //        jumpPowerY = jumpTriggerScript.jumpPowerY;
        //        canJump = true;
        //        isJumpPossible = true;
        //        StartCoroutine(JumpState());
        //    }
        //}
        //if(other.gameObject.CompareTag("PlayerHit"))
        //{
        //    HitBox playerAttackHitBox = other.GetComponent<HitBox>();
        //    if(playerAttackHitBox !=null)
        //    {

        //    }

        //}
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
            rb2d.velocity = Vector3.zero;
            StartCoroutine(Knockback(0.3f, 2f, collision.gameObject.transform, isGrounded));
            isHit = true;
        }
    }

    IEnumerator JumpState()
    { 
        yield return new WaitForSeconds(4f);
        isJumpPossible = false;
    }

    public void Flip()
    {
        if (isGrounded == true)
        {
            if (facingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingRight = true;
            }
        }
    }

    private IEnumerator DamageColor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        isHit = false;
    }

    public IEnumerator Knockback(float _duration, float _knockbackPower, Transform _obj, bool _isGrounded)
    {
        float timer = 0;
        Vector2 direction = new Vector2(-1f,0f);
        isKnockbackHappening = true;
        isHit = true;
        while (_duration > timer && _obj != null)
        {
            timer += Time.deltaTime;

            if (isDead == false)
            {
                direction = (_obj.transform.position - this.transform.position).normalized;


                if (_isGrounded == true)
                {
                    rb2d.velocity = new Vector2(Mathf.Round(-direction.x) * _knockbackPower, rb2d.velocity.y);
                    Debug.Log("direction" + Mathf.Round(-direction.x));
                }
                else
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Round(-direction.y) * (_knockbackPower * 1.5f));
                }
            }
            yield return null;
        }

        isKnockbackHappening = false;
    }
}
