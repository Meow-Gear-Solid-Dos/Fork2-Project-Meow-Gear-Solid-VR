using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLure : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //only trigger event when completely laying on the floor
        //for testing purposes, will test with a key. 
        if (Input.GetKeyDown(KeyCode.G))
        {
            ReleasingSound();
        }
    }

    //ask professor again
    public void ReleasingSound()
    {
        EventBus.Instance.HearingSound(transform.position);
    }
}
