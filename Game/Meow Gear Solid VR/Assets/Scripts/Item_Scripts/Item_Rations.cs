using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Rations : Item_Parent{
    protected override void Awake(){
        base.Awake();

        Description = "Doesn't sound very appetizing";
        Name = "Rations";
        StackSize = 3;
    }

    public override void Activate(){
        //heal player
    }
}
