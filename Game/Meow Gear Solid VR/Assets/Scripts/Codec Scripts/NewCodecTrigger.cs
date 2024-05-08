using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NewCodecTrigger : MonoBehaviour
{
    public bool isCalling;
    public AudioClip callSound;
    public AudioClip callingSound;
    public AudioClip pickupSound;
    public AudioSource source;
    public GameObject callButton;
    public Dialogue dialogue;
    public NewDialogueManager dialogueManager;
    public NewDialogueTrigger eventTrigger;

    // Start is called before the first frame update
    void Start()
    {
        eventTrigger = FindFirstObjectByType<NewDialogueTrigger>();
        isCalling = false;
        callButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartCodec(InputAction.CallbackContext Context)
    {
        if(dialogueManager.isOpen == false && isCalling == false)
        {
            dialogueManager.StartDefaultDialogue(dialogue);
            source.PlayOneShot(callingSound, 1f);
        }

        if(dialogueManager.isOpen == false && isCalling == true)
        {
            eventTrigger.TriggerDialogue();
        }

        if(dialogueManager.isOpen == true && dialogueManager.started == false)
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    // When player collides with trigger
    private void OnTriggerEnter(Collider collision)
    {
        // Compares tag of collision object with key in eventDialogue dictionary
        // If collision object's tag is a key value, trigger codec
        if(dialogue.eventDialogue.ContainsKey(collision.gameObject.tag))
        {
            if(isCalling == false)
            {
                isCalling = true;
                StartCoroutine("RingtoneTimeout");
                callButton.SetActive(true);
                //Increments the plot important dialogue. Need to find a way to not increment when it's a side call
                dialogueManager.defKey += 1;
                // Set eventKey to collision object tag for finding value in dictionary
                dialogueManager.eventKey = collision.gameObject.tag;
                Debug.Log("key: " + dialogueManager.eventKey);                
            }

        }
        //Completely optional dialogue, does not advance game's base dialogue
        if(dialogue.specialDialogue.ContainsKey(collision.gameObject.tag))
        {
            if(isCalling == false)
            {
                eventTrigger.skippable = true;
                isCalling = true;
                StartCoroutine("RingtoneTimeout");
                callButton.SetActive(true);
                //Increments the plot important dialogue. Need to find a way to not increment when it's a side call
                // Set eventKey to collision object tag for finding value in dictionary
                dialogueManager.eventKey = collision.gameObject.tag;
                Debug.Log("key: " + dialogueManager.eventKey);                
            }
        }
    }

    public void pickupCall()
    {
        isCalling = false;
        source.PlayOneShot(pickupSound, 1f);
        StopCoroutine("RingtoneTimeout");
        callButton.SetActive(false);
        eventTrigger.skippable = false;
    }

    // Plays codec call 3 times
    IEnumerator RingtoneTimeout()
    {
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(1.2f);
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(1.2f);
        source.PlayOneShot(callSound, 1f);
        yield return new WaitForSeconds(3f);
        callButton.SetActive(false);
        isCalling = false;
        eventTrigger.skippable = false;
    }


}
