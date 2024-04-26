using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Cardboard_Box : Item_Parent{
    protected override void Awake(){
        base.Awake();

        Description = "A cardboard box";
        Name = "Cardboard Box";
    }
}
