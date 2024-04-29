using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_Parent : MonoBehaviour, InteractInterface{
    [SerializeField] public Texture itemIcon;
    [SerializeField] public string itemName;
    [SerializeField] public string itemDesc;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;
    public ItemCategories Category;
    [SerializeField] public GameObject ItemPrefab;
    public int StackSize;
    public Entity_Player PlayerReference;
    public Inventory inventory;
    public InventoryDisplay inventoryDisplay;
    public bool hasBeenPickedUp;
    public bool KeyExists;
    [SerializeField] public GameObject floatingTextBox;
    protected Item_Parent(){
    }

    protected virtual void Awake()
    {
        PlayerReference = GameObject.FindWithTag("Player").GetComponent<Entity_Player>();
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        inventoryDisplay = GameObject.FindWithTag("Player").GetComponent<InventoryDisplay>();
    }

    protected virtual void Start(){
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
