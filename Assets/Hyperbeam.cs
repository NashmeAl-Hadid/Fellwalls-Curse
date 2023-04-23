using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyperbeam : MonoBehaviour
{
    public LineRenderer laserbeam;
    public bool canLaser = true;
    public Transform shotPos;
    public Transform endPos;
    public GameObject startVFX;
     public GameObject endVFX;

    private List<ParticleSystem> particles = new List<ParticleSystem>();
    
    



    private void Start()
    {
        laserbeam.enabled = false;
        FillLists();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L) && canLaser)
        {
            canLaser = false;
            laserbeam.enabled = true;
            StartCoroutine(LaserDown());
            StartCoroutine(AttackCooldown());
         

            for (int i = 0; i < particles.Count; i++)
                particles[i].Play();
            
        }
        if (laserbeam.enabled == true)
        {
            laserbeam.SetPosition(0, new Vector3(0,0,0));
            laserbeam.SetPosition(1, new Vector3(10,0,0));

            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, new Vector2 (1,0), 10);
            if(hit.collider == true)
            {
                Debug.Log("Laser hit");
            }
           
        }
    }


    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(30.0f);
        canLaser = true;
    }
    IEnumerator LaserDown()
    {
        yield return new WaitForSeconds(5.0f);
        laserbeam.enabled = false;
        for (int i = 0; i < particles.Count; i++)
            particles[i].Stop();

    }
    void FillLists()
    {
        for(int i=0; i<startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
                particles.Add(ps);
        }
        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
                particles.Add(ps);
        }
    }
}

