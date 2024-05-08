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
            FindFirstObjectByType<NewDialogueManager>().StartSpecialDialogue(dialogue);   
        }
        else
        {
            FindFirstObjectByType<NewDialogueManager>().StartEventDialogue(dialogue);                 
        }

        FindFirstObjectByType<NewCodecTrigger>().pickupCall();
    }
}
