using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Parent : MonoBehaviour, InteractInterface{
    //These data slots are for item slot instantiation. They hold the item's dat
    [SerializeField] public Texture itemIcon;
    [SerializeField] public string itemName;
    [SerializeField] public string itemDesc;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;
    [SerializeField] public GameObject ItemPrefab;   
    [SerializeField] public GameObject floatingTextBox;
    //Metadata for the item - what type is it, what's the stack size 
    public ItemCategories Category;
    public int Amount;
    public int StackSize;
    //References outside of the items.
    public Entity_Player PlayerReference;
    public Inventory inventory;
    public InventoryDisplay inventoryDisplay;
    //Bools to keep track of item status
    public bool hasBeenPickedUp;
    public bool KeyExists;
    protected Item_Parent(){
    }

    protected virtual void Awake()
    {
    }

    protected virtual void Start(){
        PlayerReference = GameObject.FindWithTag("Player").GetComponent<Entity_Player>();
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        inventoryDisplay = GameObject.FindWithTag("Player").GetComponent<InventoryDisplay>();
    }

    protected virtual void Update(){
        
    }

    public virtual void Activate(){
    }
    public virtual void OnGrab(){
        Debug.Log("Item has been grabbed");
        //inventory.AddToInventory(ItemPrefab, 1);
        ShowText(ItemPrefab);

    }
    public virtual void OnRelease(){
        Debug.Log("Item has been grabbed");
        //inventory.AddToInventory(ItemPrefab, 1);
        //ShowText(ItemPrefab);

    }
    protected virtual void InteractWithTarget(){
    }
    void ShowText(GameObject ItemPrefab)
    {
        string itemNameText = ItemPrefab.GetComponent<Item_Parent>().itemName;
        Transform itemPosition = ItemPrefab.GetComponent<Transform>();
        if(floatingTextBox)
        {
            GameObject prefab = Instantiate(floatingTextBox, itemPosition.position, Quaternion.Euler(0, 360, 0), itemPosition);
            prefab.GetComponentInChildren<TMP_Text>().text = itemNameText;
            Destroy(prefab, .5f);
        }
    }
}
