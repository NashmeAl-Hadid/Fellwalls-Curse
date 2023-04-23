using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueTrigger diagTrigger;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {    
        if (dialogueManager != null)
        {
            if (dialogueManager.inProgress == false)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (diagTrigger != null)
                    {
                        diagTrigger.TriggerDialogue();
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    dialogueManager.DisplayNextSentence();
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
       if (other.gameObject.GetComponent<DialogueTrigger>() != null)
       {
          diagTrigger = other.gameObject.GetComponent<DialogueTrigger>();   
       }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(diagTrigger!=null)
        {
            diagTrigger = null;
        }
           
        if (other.gameObject.GetComponent<DialogueTrigger>() != null)
        {
            dialogueManager.EndDialogue();
        }
    }
}
