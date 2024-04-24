using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{
    public List<KeyValuePair<GameObject, int>> InventoryReference = new List<KeyValuePair<GameObject, int>>();
    public InventoryDisplay inventoryDisplay;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void AddToInventory(GameObject ItemAdded, int Quantity){
        bool KeyExists = false;

        for (int i = 0; i < InventoryReference.Count; i++){
            if (InventoryReference[i].Key.GetType() == ItemAdded.GetType()){
                KeyExists = true;

                InventoryReference[i] = new KeyValuePair<GameObject, int>(ItemAdded, (InventoryReference[i].Value + Quantity));
            }
        }

        if (!KeyExists){
            InventoryReference.Add(new KeyValuePair<GameObject, int>(ItemAdded, Quantity));
            inventoryDisplay.AddSlot(ItemAdded);
        }
    }
}
