using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    float currentAmount = 0f;
    float maxAmount = 5f;

    public bool canTime = true;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && canTime)
        {

            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.3f;

            else

                Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

           
           

        }

        PlayerControllerTest pct = gameObject.GetComponent<PlayerControllerTest>();
        pct.speed = 10.0f;

        canTime = true;

        StartCoroutine(AttackCooldown());
       


        if (Time.timeScale == 0.03f)
        {

            currentAmount += Time.deltaTime;
        }

        if (currentAmount > maxAmount)
        {

            currentAmount = 0f;
            Time.timeScale = 1.0f;

        }

        IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(20.0f);
            canTime = true;
        }
    }
}

