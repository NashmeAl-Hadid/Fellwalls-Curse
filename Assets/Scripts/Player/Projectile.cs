using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    Rigidbody2D rigBody;
   public GameObject player;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");


        if(player.transform.localEulerAngles.y == 180)
        {
            projectileSpeed = projectileSpeed * -1;
        }
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    
    }
     void Update()
    {
        rigBody.velocity = new Vector2(projectileSpeed, 0);
        Destroy(gameObject, 2f);
    }

}
