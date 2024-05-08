using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Dog_Treats : Item_Parent{
    public AudioSource audioSource;
    public AudioClip impactSound;
    public bool gettingDestroyed;
    protected override void Awake()
    {
        hasBeenPickedUp = false;
    }
    public void Update()
    {

    }
    public override void Activate()
    {


    }
    public override void OnGrab(){
        ShowText(ItemPrefab);
        if(gettingDestroyed == true)
        {
            StopCoroutine("DelayedDestroy"); 
            gettingDestroyed = false;          
        }

        hasBeenPickedUp = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == 8) && hasBeenPickedUp == true)
        {
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
        gettingDestroyed = true;
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
