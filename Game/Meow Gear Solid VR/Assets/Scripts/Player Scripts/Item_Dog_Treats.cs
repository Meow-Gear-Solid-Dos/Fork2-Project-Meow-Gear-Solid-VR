using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Dog_Treats : Item_Parent{
    public AudioSource audioSource;
    public AudioClip impactSound;
    protected override void Awake(){
    }
    public void Update()
    {

    }
    public override void Activate()
    {


    }
    public override void OnGrab(){
        ShowText(ItemPrefab);
        StopCoroutine("DelayedDestroy");
        hasBeenPickedUp = true;

    }
    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == 8) && hasBeenPickedUp == true)
        {
            ReleasingSound();
            StartCoroutine("DelayedDestroy");
        }
    }
    public void ReleasingSound()
    {
        //publish the "position" parameter of the dummy sound object to whoever subscribe to the event.
        EventBus.Instance.HearingSound(transform.position);
        audioSource.PlayOneShot(impactSound, .5f);
    }
    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
