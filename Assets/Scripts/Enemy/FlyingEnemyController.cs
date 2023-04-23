using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class FlyingEnemyController : MonoBehaviour
{
    private AIPath aiPath;
    
    void Start()
    {
        aiPath = this.GetComponentInParent<AIPath>();
        aiPath.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            aiPath.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(chasePlayer());
        }
    }

    public IEnumerator chasePlayer()
    {
        yield return new WaitForSecondsRealtime(2f);
        aiPath.enabled = false;
    }
}
