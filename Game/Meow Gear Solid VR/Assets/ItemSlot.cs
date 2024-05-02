using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public GameObject itemSlot;
    public Inventory_Bag inventoryBag;
    public InventoryDisplay inventoryDisplay;
    public InventoryDescription InventoryDescription;
    //Item equpping
    public bool equipped = false;
    public Quaternion defaultRotation;
    public RawImage equipmentIcon;
    
    //Will link the item name to the name section in the actual inventory slot
    public GameObject itemPrefab;
    public TMP_Text itemNameText;
    public TMP_Text MaxAmmoText;
    public TMP_Text divided;
    public TMP_Text CurrentAmmoText;
    public string descriptionText;
    // Start is called before the first frame update
    void Start()
    {
        inventoryBag =  GameObject.FindWithTag("ItemBag").GetComponent<Inventory_Bag>();
        inventoryDisplay = GetComponentInParent<InventoryDisplay>();
        InventoryDescription = GetComponentInParent<InventoryDescription>();
    }

    // Update is called once per frame
    void Update()
    {
        inventoryDisplay.FillSlot(this, itemPrefab.GetComponent<Item_Parent>());
    }
    public void SpawnItem()
    {
        inventoryBag.CurrentItem = itemPrefab;
        inventoryBag.SpawnCurrentItem();
        InventoryDescription.hasInfo = true;
        InventoryDescription.itemName = itemNameText;
        InventoryDescription.itemDesc.SetText(descriptionText.ToString());
    }
    public void RemoveSlot()
    {
        Destroy(itemSlot);
    }
}
