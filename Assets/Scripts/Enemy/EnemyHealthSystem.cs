using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public GameObject deathParticleEffect;
    //public PlayerCombat playerCombat;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        if (currentHealth <= 0)
        {
            if(deathParticleEffect != null)
            {
                Instantiate(deathParticleEffect, transform.position, transform.rotation);
            }

            transform.parent.GetComponent<Enemy>().isDead = true;
            Destroy(transform.parent.gameObject);
        }
    }
}
