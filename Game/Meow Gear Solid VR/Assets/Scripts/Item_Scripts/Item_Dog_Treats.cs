using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Dog_Treats : Item_Parent{
    protected override void Awake(){
        base.Awake();

        Description = "Whatever";
        Name = "Dog Treats";
        StackSize = int.MaxValue;
    }
}
