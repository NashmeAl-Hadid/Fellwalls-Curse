using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public objDialog dialogue;

    public void TriggerDialogue()
    {
     //   if(transform.parent.GetComponent<NPC>().dialogueInProgress==false)
      //  {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
       // }      
    }
}
