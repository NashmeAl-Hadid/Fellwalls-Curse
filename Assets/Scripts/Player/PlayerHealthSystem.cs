using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    
    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            //Destroy(transform.parent.gameObject);
        }
    }
}
