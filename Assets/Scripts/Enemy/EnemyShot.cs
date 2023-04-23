using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float speed = 5f;

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();

        Destroy(gameObject,10f);

        rb2d.velocity = transform.right * speed;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")==false && collision.gameObject.CompareTag("EnemyHit") == false)
        Destroy(gameObject);
    }
}
