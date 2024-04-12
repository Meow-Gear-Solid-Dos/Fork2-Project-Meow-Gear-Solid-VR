using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue ()
    {
        FindObjectOfType<NewDialogueManager>().StartEventDialogue(dialogue);
        //FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
}
