using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Bag : MonoBehaviour{
    public Player_Controller PlayerControllerReference;

    public GameObject CurrentItem;

    private GameObject OverlappingItem;
    // Start is called before the first frame update
    void Start(){
        PlayerControllerReference = GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider OtherCollider){
        if (OtherCollider.gameObject.GetComponent<Item_Ranged_Weapon>() != null){
            OverlappingItem = OtherCollider.gameObject;
        }
        else{
            Debug.Log("Not an item");
        }
    }

    void OnTriggerExit(Collider OtherCollider){
        if (OtherCollider.gameObject.GetComponent<Item_Ranged_Weapon>() != null){
            OverlappingItem = null;
        }
        else{
            Debug.Log("Not an item exit");
        }
    }

    public GameObject GetOverlappingItem(){
        return OverlappingItem;
    }

    public void SpawnCurrentItem(){
        GameObject Item = Instantiate(CurrentItem, transform.position, Quaternion.identity);
    }
}
