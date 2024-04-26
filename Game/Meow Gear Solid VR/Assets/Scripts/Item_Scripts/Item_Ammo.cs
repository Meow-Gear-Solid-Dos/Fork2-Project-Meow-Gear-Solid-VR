using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ammo : Item_Parent{
    protected override void Awake(){
        base.Awake();

        Amount = 10;
        Description = "Ammo for your gun";
        InventoryClass = ItemInventoryClass.Static;
        Name = "Ammo";
    }
}
