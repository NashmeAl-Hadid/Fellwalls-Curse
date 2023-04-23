using UnityEngine;

public class NPC : MonoBehaviour
{
    [HideInInspector]public Animator npcAnimator;
    [HideInInspector] public RaycastHit2D CornerInfo;
    public Transform cornerDetection;
    public float cornerDetectRange = 5f;
    public float patrolSpeed = 5f;
    public bool facingRight = true;
    private bool isGrounded;
    public Transform groundDetection;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private DialogueManager dialogueManager;
    public bool dialogueInProgress;
    [HideInInspector]public float idleDuration = 5f;
    private float changeToIdleRandomTimer = 0;
    private bool npcsColliding = false;

    private void Awake()
    {
        npcAnimator = this.GetComponent<Animator>();
        idleDuration = Random.Range(5, 10);
    }

    void Start()
    {       
        if(FindObjectOfType<DialogueManager>() != null)
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        CornerInfo = Physics2D.Raycast(cornerDetection.position, Vector2.down, cornerDetectRange);
        if(dialogueManager!= null)
        {
            dialogueInProgress = dialogueManager.inProgress;
        }
       
        GroundCheck();
    }

    public void NormalPatrol()
    {
        transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);
    }

    public bool NormalPatrolCheck()
    {
        int changeToIdleCorner = 0;
        int changeToIdleRandom = 0;

        //Debug.Log("TIMER: " + changeToIdleRandomTimer);
        if(changeToIdleRandomTimer <= 2f)
        {
            changeToIdleRandomTimer += Time.deltaTime;
        }
        else
        {
            changeToIdleRandom = Random.Range(1, 100);
            //Debug.Log("idlerandom: " + changeToIdleRandom);
            if (changeToIdleRandom <=20 && npcsColliding == false)
            {
                changeToIdleRandomTimer = 0;
                idleDuration = Random.Range(5, 10);
                return false;
            }
            changeToIdleRandomTimer = 0;
        }

        if (CornerInfo.collider == false)
        {
            Flip();
            changeToIdleCorner = Random.Range(1, 25);
            if (changeToIdleCorner < 10 && npcsColliding == false)
            {
                changeToIdleRandomTimer = 0;
                idleDuration = Random.Range(5, 10);
                return false;

            }
            return true;
        }
        //else if (CornerInfo.collider == true && CornerInfo.transform.gameObject.tag == "NPC")
        //{
        //    Flip();
        //    idleDuration = Random.Range(5, 10);
        //    return false;
        //}
        else if (CornerInfo.collider == true && CornerInfo.transform.gameObject.tag != "Ground" && CornerInfo.transform.gameObject.tag != "Player" && CornerInfo.transform.gameObject.tag != "NPC")
        {
            Flip();
            changeToIdleCorner = Random.Range(1, 25);
            if (changeToIdleCorner < 10 && npcsColliding == false)
            {
                changeToIdleRandomTimer = 0;
                idleDuration = Random.Range(5, 10);
                return false;
            }
            return true;
        }

        return true;
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundDetection.position, groundCheckRadius, whatIsGround);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            npcsColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            npcsColliding = false;
        }
    }
}
