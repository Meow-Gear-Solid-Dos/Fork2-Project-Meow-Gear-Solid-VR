using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script will be in the object the player throws
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
        //Ethan will take care of this part

        if (Input.GetKeyDown(KeyCode.G))
        {
            ReleasingSound();
        }
    }

    //ask professor again
    public void ReleasingSound()
    {
        //publish the "position" parameter of the dummy sound object to whoever subscribe to the event.
        EventBus.Instance.HearingSound(transform.position);
    }
}
