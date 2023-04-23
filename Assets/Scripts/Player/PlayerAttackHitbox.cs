using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    [HideInInspector]
    public int damage;

    public float knockbackDuration = 0.2f;
    public float knockbackPower = 3f;

    public void OnTriggerEnter2D(Collider2D hit)
    {
            if (hit.gameObject.tag == "Enemy" || hit.gameObject.tag == "EnemyHit")
            {
                Enemy enemyScript = hit.transform.parent.GetComponent<Enemy>();
                EnemyHealthSystem enemyHealthSystem = hit.GetComponent<EnemyHealthSystem>();

                if (enemyHealthSystem != null)
                {
                    hit.GetComponent<EnemyHealthSystem>().currentHealth -= damage;
                }

                if (enemyScript != null)
                {
                    StartCoroutine(enemyScript.Knockback(knockbackDuration, knockbackPower, this.gameObject.transform, enemyScript.isGrounded));
                    Debug.Log("Enemy Hit");
                }        
            }
    }
}
