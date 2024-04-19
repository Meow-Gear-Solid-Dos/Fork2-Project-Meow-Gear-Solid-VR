using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Parent : MonoBehaviour, InteractInterface{
    public string Description;
    public string Name;
    public ItemCategories Category;
    public GameObject ItemPrefab;

    protected Item_Parent(){
    }

    protected virtual void Awake(){
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
