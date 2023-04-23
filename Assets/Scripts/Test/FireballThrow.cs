﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballThrow : MonoBehaviour
{
    private Vector3 target;
    public GameObject player;
    public GameObject crosshairs;
    public GameObject projectile;

    public float bulletSpeed = 60.0f;
    public bool canAttack = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetKeyDown(KeyCode.Y) && canAttack)
        {
            canAttack = false;
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireProjectile(direction, rotationZ);
            StartCoroutine(AttackCooldown());

        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        canAttack = true;
    }
    void fireProjectile(Vector2 direction, float rotationZ)
    {

        GameObject b = Instantiate(projectile) as GameObject;
        b.transform.position = player.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
