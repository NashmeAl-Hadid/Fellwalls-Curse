using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueManger_test : MonoBehaviour
{
    private Queue<string> sentences;
    public Animator anim;


    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
       

    }


    public void StartDialogue(objDialog dialogue)
    {
        Debug.Log("starting convo" + dialogue.name);
        anim.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
      string sentence =   sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }
    void EndDialogue()
    {
        anim.SetBool("IsOpen", false);
    }
}
