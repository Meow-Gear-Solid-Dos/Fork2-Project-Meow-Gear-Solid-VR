using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool skippable;
    public void TriggerDialogue()
    {
        FindObjectOfType<NewDialogueManager>().StartEventDialogue(dialogue, skippable);
        FindObjectOfType<NewCodecTrigger>().pickupCall();
    }
}
