using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Animator anim;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public bool inProgress;

    void Start()
    {
        sentences = new Queue<string>();
        inProgress = false;
    }

    public void StartDialogue(objDialog dialogue)
    {
        Debug.Log("starting convo" + dialogue.name);
        anim.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        inProgress = true;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
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
    public void EndDialogue()
    {
        inProgress = false;
        anim.SetBool("IsOpen", false);
    }
}
