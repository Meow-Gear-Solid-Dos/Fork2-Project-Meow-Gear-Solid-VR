using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ranged_Weapon_SOCOM : Item_Ranged_Weapon{
    protected override void Awake(){
        base.Awake();

        Description = "This is Alec's Job";
        Name = "SOCOM";
    }
}
