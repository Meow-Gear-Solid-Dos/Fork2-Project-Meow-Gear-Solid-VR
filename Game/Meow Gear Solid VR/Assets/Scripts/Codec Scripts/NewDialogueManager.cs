using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewDialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public int defKey = 0;
    public string eventKey;

    // Dialogue Box pop-up animation
    public Animator dialogueAnimator;

    // Puts all sentences we are going to display into queue
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Display dialogue box and start displaying default dialogue
    public void StartDefaultDialogue (Dialogue dialogue)
    {
        // Open dialogue box
        dialogueAnimator.SetBool("dialogueIsOpen", true);

        nameText.text = dialogue.callerName;

        // Clears any previous sentences
        sentences.Clear();

        // Get new sentences
        List<string> eventSentences = dialogue.defDialogue[defKey];
        Debug.Log("sentences" + eventSentences);

        // Loop through sentences and add to queue
        foreach (string sentence in eventSentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }



    // Display dialogue box and start displaying event dialogue
    public void StartEventDialogue (Dialogue dialogue)
    {
        defKey += 1;
        // Open dialogue box
        dialogueAnimator.SetBool("dialogueIsOpen", true);

        nameText.text = dialogue.callerName;

        // Clears any previous sentences
        sentences.Clear();

        // Get new sentences
        List<string> eventSentences = dialogue.eventDialogue[eventKey];
        Debug.Log("sentences" + eventSentences);

        // Loop through sentences and add to queue
        foreach (string sentence in eventSentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // StartDialogue without dictionary, used string array that was modified in the scene
/*    public void StartDialogue(Dialogue dialogue)
    {
        // Open dialogue box
        dialogueAnimator.SetBool("dialogueIsOpen", true);

        nameText.text = dialogue.callerName;

        // Clears any previous sentences
        sentences.Clear();

        // Loop through sentences and add to queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }*/

    public void DisplayNextSentence()
    {
        // If there is no sentences in queue, end dialogue
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        // If user starts new sentence while TypeSentence is running, stop and start new sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // Type out dialogue letter by letter
    IEnumerator TypeSentence (string sentence) 
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Closes dialogue box if there are no more sentences to display
    void EndDialogue()
    {
        dialogueAnimator.SetBool("dialogueIsOpen", false);
    }
}
