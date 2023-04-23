using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerTest : MonoBehaviour
{
    public float speed;
    private bool facingRight = true;
    public Transform cornerDetection;
    public float cornerDetectRange = 2f;
    public float chaseRangeX = 1f;
    public float chaseRangeY = 0.5f;

    public Transform[] patrolPoints;
    Transform currentPatrolPoint;
    int currentPatrolIndex;

    public Transform target;   
    public bool isChasing;
    public bool isPatorling;
    //public bool onGround;
    private Animator enemyAnimator;

    private Rigidbody2D rb2d;

    public bool isGrounded;
    public Transform groundDetection;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool canNormalPatrol = false;
    public bool canPointPatrol = false;
    public bool canChase = false;

    public bool canJump;
    public bool doJump;
    public bool whileJumping;

    private void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        isChasing = false;
        enemyAnimator = this.GetComponent<Animator>();
        rb2d= this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {       
        GroundCheck();

        if (isGrounded == true && whileJumping==true)
        {
            rb2d.velocity = Vector3.zero;
            Debug.Log("Grounded after Jump");
        }

        if (isGrounded == false)
        {
            enemyAnimator.SetBool("Jump", true);
            whileJumping = true;
        }
        else
        {
            enemyAnimator.SetBool("Jump", false);
            whileJumping = false;
        }

        if(canChase==true)
        {
            ChaseCheck();
        }
        
        if (isPatorling == false && isChasing == false)
        {
            StartCoroutine(WaitInIdle());
        }

        if (isPatorling==true && isChasing==false)
        {
            if(canPointPatrol)
            {
                PointPatrol();
            }
          
            if(canNormalPatrol)
            {
                NormalPatrol();
            }
        }
    } 

    void ChaseCheck()
    {
        float distanceToTargetX = target.position.x - transform.position.x;
        float distanceToTargetY = target.position.y - transform.position.y;


        if (Mathf.Abs(distanceToTargetX) < chaseRangeX && Mathf.Abs(distanceToTargetY) < chaseRangeY)
        {
            isChasing = true;
            isPatorling = false;
            StopAllCoroutines();
            Chasing();
        }
        else
        {
            isChasing = false;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundDetection.position, groundCheckRadius, whatIsGround);
    }

    IEnumerator WaitInIdle()
    {     
        Idle();

        Debug.Log("State: " + "Wait In Idle");

        yield return new WaitForSeconds(2f);

        if (canNormalPatrol == true || canPointPatrol == true)
        {
            isPatorling = true;
        }
        else
        {
           Idle();
        }                
    }

    void Idle()
    {
        enemyAnimator.SetBool("Run", false);
    }

    void NormalPatrol()
    {
        enemyAnimator.SetBool("Run", true);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        int changeToGoIdle = 25;

        RaycastHit2D CornerInfo = Physics2D.Raycast(cornerDetection.position, Vector2.down, cornerDetectRange);

        if (CornerInfo.collider == false)
        {
            changeToGoIdle = Random.Range(1, 25);
            if (changeToGoIdle < 10)
            {
                isPatorling = false;
            }

            Flip();
        }
        else if (CornerInfo.collider == true && CornerInfo.transform.gameObject.tag != "Ground")
        {
            changeToGoIdle = Random.Range(1, 25);
            if (changeToGoIdle < 10)
            {
                isPatorling = false;
            }

            Flip();
        }
    }

    void PointPatrol()
    {
        // Debug.Log("State: " + "Patrol");

        enemyAnimator.SetBool("Run", true);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        int changeToGoIdle = 25;
        
        if (Vector3.Distance(transform.position,currentPatrolPoint.position) < 1f )
        {
            changeToGoIdle=Random.Range(1, 25);
            if (changeToGoIdle<10)
            {
                isPatorling = false;         
            }            

            if (currentPatrolIndex +1<patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }

            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;

        if(patrolPointDir.x<0f)
        {
            Flip();
        }

        if (patrolPointDir.x > 0f)
        {
            Flip();
        }
    }

    void Chasing()
    {
        // Debug.Log("distanceX: " + distanceToTargetX + " distanceY: " + distanceToTargetY);      
        //Debug.Log("State: " + "Chase");

        RaycastHit2D CornerInfo = Physics2D.Raycast(cornerDetection.position, Vector2.down, cornerDetectRange);

        if (CornerInfo.collider == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            enemyAnimator.SetBool("Run", true);
        }
        else if (CornerInfo.collider == true && CornerInfo.transform.gameObject.tag != "Ground")
        {
            enemyAnimator.SetBool("Run", false);
        }

        //if (isGrounded == true)
        //{
        //    transform.Translate(Vector2.right * speed * Time.deltaTime);
        //    enemeyAnimator.SetBool("Run", true);
        //}
        //else
        //{
        //    enemeyAnimator.SetBool("Run", false);
        //}

        if ((target.position.x < transform.position.x) && isChasing==true && facingRight==true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = false;
        }

        if ((target.position.x > transform.position.x) && isChasing == true && facingRight == false)
        {           
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
        }
    }

    void Flip()
    {
        if(isGrounded==true)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("JumpTrigger") && canJump == false)
        {
            int jumpChance = Random.Range(1, 25);
            JumpTriggerTest jumpTriggerScript = other.GetComponent<JumpTriggerTest>();

            if(jumpChance<12 && isChasing==false)
            {
                isPatorling = false;
                canJump = true;
                Debug.Log("Can Jump");
                rb2d.velocity = Vector3.zero;
                rb2d.velocity = Vector2.up * jumpTriggerScript.jumpPowerY;
                rb2d.velocity = new Vector2(transform.right.x * jumpTriggerScript.jumpPowerX, rb2d.velocity.y);
                StartCoroutine(JumpState());
            }  
        }
    }
 
    IEnumerator JumpState()
    {
        yield return new WaitForSeconds(4f);
        canJump = false;     
    }
}
