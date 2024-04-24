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
