using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.UI;

public class NewCodecTrigger : MonoBehaviour
{
    public bool isCalling;
    public AudioClip callSound;
    public AudioSource source;
    public GameObject callButton;

    // Start is called before the first frame update
    void Start()
    {
        isCalling = false;
        callButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isCalling == true)
        {
            isCalling = false;
        }
    }

    // When player collides with trigger
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("CodecTrigger") )
        {
            isCalling = true;
            StartCoroutine("Timeout");
            callButton.SetActive(true);
        }
    }

    // Plays codec call 3 times
    IEnumerator Timeout()
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
