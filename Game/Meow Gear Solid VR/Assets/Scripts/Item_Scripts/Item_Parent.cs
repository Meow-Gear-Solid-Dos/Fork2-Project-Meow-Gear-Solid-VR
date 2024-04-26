using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Parent : MonoBehaviour, InteractInterface{
    public int Amount;
    public ItemCategories Category;
    public string Description;
    public ItemInventoryClass InventoryClass;
    public string Name;
    public int StackSize;
    public Entity_Player PlayerReference;

    protected virtual void Awake(){
        Amount = 1;
        Description = "";
        InventoryClass = ItemInventoryClass.Instanced;
        Name = "";
        StackSize = 1;
    }

    protected virtual void Start(){
        
    }

    protected virtual void Update(){
        
    }

    public virtual void Activate(){
    }

    protected virtual void InteractWithTarget(){
    }
}
