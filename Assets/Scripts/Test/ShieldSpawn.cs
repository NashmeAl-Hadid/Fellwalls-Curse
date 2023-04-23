using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpawn : MonoBehaviour
{

    // public bool canAttack = true;

    public GameObject shield;
   public bool canShield = true;


    private void Start()
    {
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.O) && canShield)
        {
            canShield = false;
            shield.SetActive(true);
            StartCoroutine(ShieldDown());
            StartCoroutine(AttackCooldown());

        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(20.0f);
        canShield = true;
    }
    IEnumerator ShieldDown()
    {
        yield return new WaitForSeconds(10.0f);
        shield.SetActive(false);
    }
}
//    
