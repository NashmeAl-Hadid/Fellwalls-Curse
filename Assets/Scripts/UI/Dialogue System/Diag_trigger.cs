using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diag_trigger : MonoBehaviour
{

    public objDialog dialogue;


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManger_test>().StartDialogue(dialogue);
    }
}
