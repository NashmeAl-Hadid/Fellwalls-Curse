using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private float inputHorizontal;
    private float inputVertical;
    private float move;
    public float ladderDistance;
    public LayerMask whatIsLadder;
    private bool isClimbing;

    // Start is called before the first frame Upd
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        PlayerPlatformerController playerScript = player.GetComponent<PlayerPlatformerController>();
    }

    void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, ladderDistance, whatIsLadder);

        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }

            if (isClimbing == true)
            {
                move = Input.GetAxis("Vertical");
                //rb.velocity = new Vector2(rb.position.x, move * maxSpeed);
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 5;
            }
        }
    }
}
