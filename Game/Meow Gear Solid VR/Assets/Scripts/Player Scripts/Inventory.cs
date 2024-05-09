using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Inventory : MonoBehaviour{

    public InventoryDisplay inventoryDisplay;
    public List<GameObject> InstancedInventory = new List<GameObject>();
    public List<KeyValuePair<string, int>> StaticInventory = new List<KeyValuePair<string, int>>();
    public AudioClip tooManyItemsSound;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
        
    }

    public void AddToInventory(GameObject ItemAdded)
    {
        bool KeyExists = false;
        //Janky ammo solution. If item is ammo, will look for the gun in your inventory and add to its ammo.
        //Otherwise, if its not ammo, it will go through the inventory as normal
        if((ItemAdded.GetComponent<Item_Ammo>() != null))
        {
            for (int i = 0; i < InstancedInventory.Count; i++)
            {
                Debug.Log("Checking for Gun");
                if(InstancedInventory[i].GetComponent<Item_Ranged_Weapon>() != null)
                {
                    if(InstancedInventory[i].GetComponent<Item_Ranged_Weapon>().currentAmmo == 45)
                    {
                        return;
                    }
                    if((InstancedInventory[i].GetComponent<Item_Ranged_Weapon>().currentAmmo + 15) >= 45)
                    {
                        InstancedInventory[i].GetComponent<Item_Ranged_Weapon>().currentAmmo = InstancedInventory[i].GetComponent<Item_Ranged_Weapon>().maxAmmo;
                        ItemAdded.SetActive(false);
                    }
                    else
                    {
                        InstancedInventory[i].GetComponent<Item_Ranged_Weapon>().currentAmmo += 15;
                        ItemAdded.SetActive(false);
                    }
                }
            }
        }

        for (int i = 0; i < InstancedInventory.Count; i++)
        {
            //If item in slot has the same meta name as the item being added, we set KeyExists to be true.
            if (InstancedInventory[i].GetComponent<Item_Parent>().itemName == ItemAdded.GetComponent<Item_Parent>().itemName)
            {
                KeyExists = true;
                if(ItemAdded.GetComponent<Item_Parent>().hasBeenPickedUp == false)
                {
                    int StackSize = ItemAdded.GetComponent<Item_Parent>().StackSize;   
                    if((StackSize == 3))
                    {
                        if((InstancedInventory[i].GetComponent<Item_Parent>().Amount + 1) > StackSize)
                        {
                            source.PlayOneShot(tooManyItemsSound, .5f);
                            return;
                        }
                        else
                        {
                            InstancedInventory[i].GetComponent<Item_Parent>().Amount += 1;
                            InstancedInventory[i].GetComponent<Item_Parent>().currentAmmo = InstancedInventory[i].GetComponent<Item_Parent>().Amount;
                            ItemAdded.SetActive(false);
                        }

                    } 
                    if((StackSize == 1))
                    {
                        InstancedInventory[i].GetComponent<Item_Parent>().currentAmmo = InstancedInventory[i].GetComponent<Item_Parent>().maxAmmo;
                        ItemAdded.SetActive(false);
                    }                 
                }
                break;
                    
            }
            else
            {
                KeyExists = false;
            }
        }
        if (!KeyExists && (ItemAdded.GetComponent<Item_Parent>().hasBeenPickedUp == false))
        {
            AddToSlot(ItemAdded);
            EventBus.Instance.AddToStashedInventory(ItemAdded);
            InstancedInventory.Add(ItemAdded);
            DontDestroyOnLoad(ItemAdded);
            ItemAdded.GetComponent<Item_Parent>().hasBeenPickedUp = true;
        }
        

    }


    //Need tofinish this later
    public int Find(GameObject Item){
        for (int i = 0; i < InstancedInventory.Count; i++){
            if (InstancedInventory[i].GetType() == Item.GetType()){
                return i;
            }
        }
        return 0;

    }

    public void RemoveFromInventory(GameObject ItemRemoved)
    {
        for (int i = 0; i < InstancedInventory.Count; i++)
        {
            //If item in slot has the same meta name as the item being added, we set KeyExists to be true.
            if (InstancedInventory[i].GetComponent<Item_Parent>().itemName == ItemRemoved.GetComponent<Item_Parent>().itemName)
            {
                InstancedInventory.RemoveAt(i);
            }
        }
    }
    
    public void AddToSlot(GameObject ItemAdded)
    {
        inventoryDisplay.AddSlot(ItemAdded);
    }
    
}
