using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Projectile : Item_Weapon{
    Rigidbody RigidBodyReference;
    protected override void Start(){
        base.Start();

        RigidBodyReference = GetComponent<Rigidbody>();

        RigidBodyReference.velocity *= 5.0f;
    }

    protected override void Update(){
    }
}
