using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;

  

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
            {
            Instantiate(projectile, firePosition.position, firePosition.rotation);
        }
    }
}
