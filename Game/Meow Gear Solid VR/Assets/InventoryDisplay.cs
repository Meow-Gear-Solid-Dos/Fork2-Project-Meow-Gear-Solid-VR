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
        GameObject newItemSlot = Instantiate(itemSlotPrefab, inventoryContainer);
        Item_Parent item = ItemAdded.GetComponent<Item_Parent>();
        itemSlot = newItemSlot.GetComponent<ItemSlot>();
        FillSlot(itemSlot, item);
        itemSlotList.Add(newItemSlot);
    }

    public void FillSlot(ItemSlot itemSlot, Item_Parent item)
    {
        itemSlot.itemNameText.SetText(item.itemName);
        itemSlot.MaxAmmoText.SetText(item.maxAmmo.ToString());
        itemSlot.divided.SetText("/");
        itemSlot.CurrentAmmoText.SetText(item.currentAmmo.ToString());
        itemSlot.equipmentIcon.texture = item.itemIcon;
        itemSlot.itemPrefab = item.ItemPrefab;
    }

    public void RemoveSlot()
    {

    }   
}
