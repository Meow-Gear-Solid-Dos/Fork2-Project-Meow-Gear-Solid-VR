using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.UI;

public class NewCodecTrigger : MonoBehaviour
{
    public bool isCalling;
    public AudioClip callSound;
    public AudioClip pickupSound;
    public AudioSource source;
    public GameObject callButton;
    public Dialogue dialogue;
    public NewDialogueManager dialogueManager;    

    // Start is called before the first frame update
    void Start()
    {
        isCalling = false;
        callButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // For default dialogue, press specified key at any moment to access
        if(Input.GetButtonDown("Codec") && dialogueManager.isOpen == false)
        {
            dialogueManager.StartDefaultDialogue(dialogue);
        }
    }

    // When player collides with trigger
    private void OnTriggerEnter(Collider collision)
    {
        // Compares tag of collision object with key in eventDialogue dictionary
        // If collision object's tag is a key value, trigger codec
        if(dialogue.eventDialogue.ContainsKey(collision.gameObject.tag))
        //if (collision.gameObject.CompareTag("CodecTrigger"))
        {
            if(isCalling == false)
            {
                isCalling = true;
                StartCoroutine("RingtoneTimeout");
                callButton.SetActive(true);

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
    }

}
