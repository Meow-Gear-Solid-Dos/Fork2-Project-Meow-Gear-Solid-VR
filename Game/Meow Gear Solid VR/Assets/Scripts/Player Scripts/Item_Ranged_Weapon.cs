using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ranged_Weapon : Item_Weapon{
    public GameObject Projectile;
    public GameObject ProjectileSpawnPoint;
    protected Item_Ranged_Weapon(){
        Description = "A gun";
        Name = "Gun";
    }
    protected override void Start(){
    }

    protected override void Update(){
    }
    
    public override void Activate(){
        GameObject ProjectileInstance = Instantiate(Projectile, ProjectileSpawnPoint.transform, true);

        ProjectileInstance.GetComponent<Rigidbody>().velocity = transform.forward;

        Debug.Log("Shoot");
    }
}
