using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Projectile : Item_Weapon{
    Rigidbody RigidBodyReference;
    protected override void Start(){
        base.Start();

        RigidBodyReference = GetComponent<Rigidbody>();
    }

    protected override void Update(){
        RigidBodyReference.velocity *= 2.0f;
    }
}
