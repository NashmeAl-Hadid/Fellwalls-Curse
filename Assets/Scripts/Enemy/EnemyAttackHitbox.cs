using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public float knockbackPower = 3f;
    public float knockbackDuration = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            PlayerControllerTest playerController = collision.GetComponent<PlayerControllerTest>();

            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

            if (playerController != null)
            {
                StartCoroutine(playerController.Knockback(knockbackDuration, knockbackPower, this.gameObject.transform,playerController.isGrounded));
            }

            else if (playerMovement != null)
            {
                StartCoroutine(playerMovement.Knockback(knockbackDuration, knockbackPower, this.gameObject.transform, playerMovement.isGrounded));
            }
        }
    }

}
