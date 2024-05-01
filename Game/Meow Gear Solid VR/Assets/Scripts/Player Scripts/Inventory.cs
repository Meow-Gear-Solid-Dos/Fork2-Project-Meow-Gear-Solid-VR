using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{

    public InventoryDisplay inventoryDisplay;
    public List<KeyValuePair<GameObject, int>> InstancedInventory = new List<KeyValuePair<GameObject, int>>();
    public List<KeyValuePair<string, int>> StaticInventory = new List<KeyValuePair<string, int>>();

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void AddToInventory(GameObject ItemAdded, int Quantity){
        bool KeyExists = false;

        for (int i = 0; i < InstancedInventory.Count; i++){
            if (InstancedInventory[i].Key.GetType() == ItemAdded.GetType()){
                KeyExists = true;

                InstancedInventory[i] = new KeyValuePair<GameObject, int>(ItemAdded, (InstancedInventory[i].Value + Quantity));
            }
        }

        if (!KeyExists){
            InstancedInventory.Add(new KeyValuePair<GameObject, int>(ItemAdded, Quantity));
        }

        AddToSlot(ItemAdded);
    }



    public int Find(GameObject Item){
        for (int i = 0; i < InstancedInventory.Count; i++){
            if (InstancedInventory[i].Key.GetType() == Item.GetType()){
                return InstancedInventory[i].Value;
            }
        }

        return 0;
    }

    public void RemoveFromInventory(GameObject ItemRemoved, int Quantity){
        for (int i = 0; i < InstancedInventory.Count; i++){
            if (InstancedInventory[i].Key.GetType() == ItemRemoved.GetType()){
                if (InstancedInventory[i].Value >= Quantity){
                    InstancedInventory[i] = new KeyValuePair<GameObject, int>(ItemRemoved, (InstancedInventory[i].Value - Quantity));

                    if (InstancedInventory[i].Value == 0){
                        InstancedInventory.RemoveAt(i);
                    }
                }
                else{
                    Debug.Log("Invalid operation. Not enough of that item to remove the specified quantity.");
                }
            }
        }
    }
    
    public void AddToSlot(GameObject ItemAdded)
    {
        inventoryDisplay.AddSlot(ItemAdded);
    }
    
}
