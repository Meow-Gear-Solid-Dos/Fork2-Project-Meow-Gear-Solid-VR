using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Keycard : Item_Parent{
    int ID = 0;

    protected override void Awake(){
        base.Awake();

        Description = "A keycard";
        Name = "Keycard";
    }
}
