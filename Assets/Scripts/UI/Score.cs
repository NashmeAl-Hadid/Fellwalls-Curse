using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public AudioSource soundEffect;

    void Start()
    {
        soundEffect =GetComponent<AudioSource> ();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 1;
            soundEffect.Play();
        }
    }
}    

