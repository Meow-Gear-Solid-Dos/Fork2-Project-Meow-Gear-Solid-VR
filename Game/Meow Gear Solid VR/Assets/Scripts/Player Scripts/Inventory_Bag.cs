using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Bag : MonoBehaviour{
    public Player_Controller PlayerControllerReference;
    public GameObject spawnHere;
    public Inventory inventory;
    public GameObject CurrentItem;
    public bool canSpawn;
    private GameObject OverlappingItem;


    //Lines below deal with sound
    public AudioSource source;
    public AudioClip soundClip;


    // Start is called before the first frame update
    void Start(){
        PlayerControllerReference = this.transform.parent.gameObject.GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnTriggerEnter(Collider OtherCollider){
        if (OtherCollider.gameObject.GetComponent<Item_Parent>() != null){
            OverlappingItem = OtherCollider.gameObject;
            CurrentItem = OverlappingItem;

            //Debug.Log("Overlapping item");
        }
        canSpawn = true;
    }

    void OnTriggerExit(Collider OtherCollider){
        if (OtherCollider.gameObject.GetComponent<Item_Parent>() != null){
            OverlappingItem = null;

            //Debug.Log("No Longer Overlapping item");
        }
        canSpawn = false;
    }

    public void DestroyOverlappingItem(){
        if (OverlappingItem != null){
            CurrentItem.SetActive(false);
            OverlappingItem = null;
        }
    }

    public GameObject GetOverlappingItem(){
        return OverlappingItem;
    }

    public void SpawnCurrentItem(){
        //GameObject Item = Instantiate(CurrentItem, transform.position, Quaternion.identity);
        CurrentItem.SetActive(true);
        CurrentItem.transform.position = spawnHere.transform.position;
        //CurrentItem.transform.position = spawnHere.transform.position;
        CurrentItem.transform.rotation = Quaternion.identity;
        //Debug.Log("Item Spawned");
        source.PlayOneShot(soundClip, .75f);
    }
    public void DeactivateOverlappingItem()
    {
        CurrentItem.SetActive(false);
    }
}
