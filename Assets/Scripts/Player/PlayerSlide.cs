using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public bool Slide = false;

    public PlayerMovement controller;

    public Rigidbody2D rig;

    public Animator anim;

    public BoxCollider2D regularColl;
    public BoxCollider2D slideColl;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask ground;

    public float slideSpeed = 5f;

    public float coolDown = 1;
    public float coolDownTimer;

    private void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

        if (coolDownTimer < 0)
        {
            coolDownTimer = 0;
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.LeftShift) && coolDownTimer == 0)
        {
            performSlide();
            coolDownTimer = coolDown;
        }
    }

    private void performSlide()
    {
        Slide = true;

        anim.SetBool("Slide", true);

        regularColl.enabled = false;
        slideColl.enabled = true;
        StartCoroutine("iFrame");

        if (controller.facingRight == true)
        {
            rig.AddForce(Vector2.right * slideSpeed);
        }
        else
        {
            rig.AddForce(Vector2.left * slideSpeed);
        }

        StartCoroutine("stopSlide");
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.6f);
        anim.Play("Idle");
        anim.SetBool("Slide", false);
        regularColl.enabled = true;
        slideColl.enabled = false;
        Slide = false;
    }

    IEnumerator iFrame()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
