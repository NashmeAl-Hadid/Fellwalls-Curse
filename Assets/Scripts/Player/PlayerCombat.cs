using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public PlayerAttackHitbox hitBoxScript;

    public Rigidbody2D rig;
    public float attackForceStill;
    public float attackForceRun;

    public float coolDown = 1;
    public float coolDownTimer;

    [HideInInspector]
    public bool attackOne = false;
    [HideInInspector]
    public bool attackTwo = false;
    [HideInInspector]
    public bool attackThree = false;
    [HideInInspector]
    public bool attackHold = false;
    [HideInInspector]
    public bool attackHoldRelease = false;
    [HideInInspector]
    public bool attackHoldCancel = false;
    //[HideInInspector]
    public bool runToAttack = false;
    [HideInInspector]
    public float charge = 0f;

    public int damageAK1;
    public int damageAK2;
    public int damageAK3;
    public int damageAKH;

    public bool isAttacking = false;

    public GameObject hitBox;
    public GameObject particleEffect;

    void Start()
    {
        //hitBox.SetActive(false);
        PlayerAttackHitbox hitBoxScript = GameObject.Find("Main_Player").GetComponent<PlayerAttackHitbox>();
    }

    void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

        if (coolDownTimer < 0)
        {
            coolDownTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && coolDownTimer == 0)
        {
            if (movement.isMoving == false)
            {
                if (movement.facingRight == true)
                {
                    rig.AddForce(Vector2.right * attackForceStill);
                    coolDownTimer = coolDown;
                }
                else
                {
                    rig.AddForce(Vector2.left * attackForceStill);
                    coolDownTimer = coolDown;
                }
            }
            if (movement.isMoving == true)
            {
                animator.SetBool("RunToAttackBool", true);

                if (movement.facingRight == true)
                {
                    rig.AddForce(Vector2.right * attackForceRun);
                    coolDownTimer = coolDown;
                }
                else
                {
                    rig.AddForce(Vector2.left * attackForceRun);
                    coolDownTimer = coolDown;
                }
            }

            if (attackOne == true)
            {
                animator.SetTrigger("Attack1");
                isAttacking = true;

                if (particleEffect != null)
                {
                    Instantiate(particleEffect, transform.position, transform.rotation);
                }
            }
            if (attackTwo == true)
            {
                animator.SetTrigger("Attack2");
                isAttacking = true;
            }
            if (attackThree == true)
            {
                animator.SetTrigger("Attack3");
                isAttacking = true;
            }
        }
        if (movement.isMoving == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                charge = Time.time;
                Debug.Log("Hold");
                animator.SetTrigger("AttackHold");
            }
            if (Input.GetMouseButtonUp(1))
            {
                float delta = Time.time - charge;
                if (delta > 2f)
                {
                    attackHoldCancel = false;
                    Debug.Log("Release");
                    animator.SetTrigger("AttackHoldRelease");
                }
                else
                {
                    attackHoldCancel = true;
                    if (attackHoldCancel == true)
                    {
                        Debug.Log("Cancel");
                        animator.SetTrigger("AttackHoldCancel");
                        movement.movementSpeed = 9f;
                    }
                }
            }
        }
    }

    public void ResetDamage()
    {
        hitBoxScript.damage = damageAK1;
    }
    public void AttackOne()
    {
        hitBox.SetActive(true);
        hitBoxScript.damage = damageAK1;
    }
    public void AttackTwo()
    {
        hitBox.SetActive(true);
        hitBoxScript.damage = damageAK2;
    }
    public void AttackThree()
    {
        hitBox.SetActive(true);
        hitBoxScript.damage = damageAK3;
    }
    public void AttackHoldRelease()
    {
        hitBox.SetActive(true);
        hitBoxScript.damage = damageAKH;
    } 
}
