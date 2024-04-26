using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.ComponentModel;
using TMPro;

public class FakeCodec : MonoBehaviour
{
    //For reference, the general pipeline is this -> a call is generated -> NewDialogueTrigger can pick up the call -> We go back to NewCodecTrigger and NewDialogueManager.
    //Lines stolen from Dialogue Manager
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public int defKey = 0;
    public string eventKey;
    public Image CallerImage;

    // Dialogue Box pop-up animation
    public Animator dialogueAnimator;
    public AudioClip talkingSFX;
    public AudioSource audioSource;
    // Puts all sentences we are going to display into queue
    private Queue<string> sentences;

    //Bool to let the manager know text is up
    public bool isOpen;
    //Lines stolen from NewCodecTrigger
    public bool isCalling;
    public AudioClip callSound;
    public AudioClip callingSound;
    public AudioClip pickupSound;
    public AudioSource source;
    public GameObject callButton;
    public Dialogue dialogue;
    public FakeTrigger eventTrigger;

    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Instance.LevelLoadStart();
        wall.SetActive(true);
        eventTrigger = FindObjectOfType<FakeTrigger>();
        isOpen = false;
        sentences = new Queue<string>();
        isCalling = false;
        if(isCalling == false)
        {
            isCalling = true;
            StartCoroutine("RingtoneTimeout");
            callButton.SetActive(true);
            //Increments the plot important dialogue. Need to find a way to not increment when it's a side call
            defKey += 0;      
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void StartCodec(InputAction.CallbackContext Context)
    {
        if(isOpen == false && isCalling == true)
        {
            eventTrigger.TriggerDialogue();
        }
    }

    public void pickupCall()
    {
        isCalling = false;
        source.PlayOneShot(pickupSound, 1f);
        StopCoroutine("RingtoneTimeout");
        callButton.SetActive(false);
    }

    // Plays codec call 3 times
    IEnumerator RingtoneTimeout()
    {
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(1.2f);
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(.5f);
        eventTrigger.TriggerDialogue();
    }

    public void StartDefaultDialogue (Dialogue dialogue)
    {
        // Show Caller Image
        //CallerImage.enabled = !CallerImage.enabled;

        // Open dialogue box
        dialogueAnimator.SetBool("dialogueIsOpen", true);
        isOpen = true;
        nameText.text = "Liquid Cat";

        // Clears any previous sentences
        sentences.Clear();

        // Get new sentences
        List<string> eventSentences = dialogue.finalDialogue[defKey];
        Debug.Log("sentences" + eventSentences);

        // Loop through sentences and add to queue
        foreach (string sentence in eventSentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        // If there is no sentences in queue, end dialogue
        if (sentences.Count == 0) 
        {
            StopAllCoroutines();
            EndDialogue();
            wall.SetActive(false);
            EventBus.Instance.LevelLoadEnd();
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
            audioSource.PlayOneShot(talkingSFX, .15f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Closes dialogue box if there are no more sentences to display
    public void EndDialogue()
    {
        dialogueAnimator.SetBool("dialogueIsOpen", false);
        isOpen = false;
        //CallerImage.enabled = !CallerImage.enabled;
    }
}
