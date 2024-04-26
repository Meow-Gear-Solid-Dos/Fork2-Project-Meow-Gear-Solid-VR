using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        inventory.AddToInventory(ItemPrefab, 1);
    }
    protected virtual void InteractWithTarget(){
    }
}
