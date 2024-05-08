using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool skippable;
    public void TriggerDialogue()
    {
        if(skippable)
        {
            FindObjectOfType<NewDialogueManager>().StartSpecialDialogue(dialogue);   
        }
        else
        {
            FindObjectOfType<NewDialogueManager>().StartEventDialogue(dialogue);                 
        }

        FindObjectOfType<NewCodecTrigger>().pickupCall();
    }
}
