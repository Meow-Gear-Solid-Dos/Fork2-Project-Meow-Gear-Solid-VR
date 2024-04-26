using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : Item_Parent{
    protected override void Awake(){
        base.Awake();

        Category = ItemCategories.Weapon;
    }

    protected override void Start(){
        
    }

    protected override void Update(){
        
    }
}
