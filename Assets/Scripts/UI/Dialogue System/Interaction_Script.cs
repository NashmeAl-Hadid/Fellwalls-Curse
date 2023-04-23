using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in trigger");
        if (collision.gameObject.transform.CompareTag("NPC"))
        {
            Debug.Log("npc detected");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("key pressed");
                if (collision.gameObject.GetComponent<DialogueTrigger>() != null)
                {
                    collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                    Debug.Log("diagactive");
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("in trigger");
        if (collision.gameObject.transform.CompareTag("NPC"))
        {
            Debug.Log("npc detected");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("key pressed");
                if (collision.gameObject.GetComponent<DialogueTrigger>() != null)
                {
                    collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                    Debug.Log("diagactive");
                }
            }

        }
    }

}
