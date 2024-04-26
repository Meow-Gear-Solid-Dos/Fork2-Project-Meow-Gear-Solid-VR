using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    // Handles inventory basics - names, descriptions, etc.
    [SerializeField] public Transform inventoryContainer; //Will spawn an item slot here.
    //Deals with item slots and item slot list
    [SerializeField] private GameObject itemSlotPrefab;
    public ItemSlot itemSlot;
    [SerializeField] private List<GameObject> itemSlotList;
    [SerializeField] private List<string> itemList;

    // Handles equiping and unequiping of the gun
    public ItemData equipedItem;
    public GameObject spawnedItem;

    public AudioSource source;
    public AudioClip selectSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddSlot(GameObject ItemAdded)
    {
        bool isInInventory = false;
        Item_Parent item = ItemAdded.GetComponent<Item_Parent>();        
        for (int i = 0; i < itemList.Count; i++)
        {
            if ((itemList[i] == item.itemName))
            {
                Debug.Log("Increment: " + i);
                item.hasBeenPickedUp = true;
                isInInventory = true;
                return;
            }
            
        }
        if(isInInventory == false)
        {
            GameObject newItemSlot = Instantiate(itemSlotPrefab, inventoryContainer); 
            itemSlot = newItemSlot.GetComponent<ItemSlot>();
            FillSlot(itemSlot, item);               
            itemSlotList.Add(newItemSlot);
            itemList.Add(item.itemName);
        }

    }

    public void FillSlot(ItemSlot itemSlot, Item_Parent item)
    {
        itemSlot.itemNameText.SetText(item.itemName);
        itemSlot.MaxAmmoText.SetText(item.maxAmmo.ToString());
        itemSlot.divided.SetText("/");
        itemSlot.CurrentAmmoText.SetText(item.currentAmmo.ToString());
        itemSlot.equipmentIcon.texture = item.itemIcon;
        itemSlot.itemPrefab = item.ItemPrefab;
        itemSlot.descriptionText = item.itemDesc;
    }

    public void RemoveSlot(GameObject ItemAdded)
    {
        Item_Parent item = ItemAdded.GetComponent<Item_Parent>();        
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == item.itemName)
            {
                Debug.Log("Destroying Item At: " + i);
                itemSlotList.RemoveAt(i);
                itemList.RemoveAt(i);
                
            }   
        }

    }   

    public void FillDescription()
    {

    }
}
