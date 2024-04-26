using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool skippable;
    public void TriggerDialogue()
    {
        FindObjectOfType<FakeCodec>().StartDefaultDialogue (dialogue);
        FindObjectOfType<FakeCodec>().pickupCall();
    }
}
