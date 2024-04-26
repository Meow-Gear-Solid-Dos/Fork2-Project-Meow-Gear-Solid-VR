using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ranged_Weapon : Item_Weapon{
    public GameObject AmmoPrefab;
    public GameObject Projectile;
    public GameObject ProjectileSpawnPoint;

    //protected uint Ammo;
    protected uint ClipSize;

    protected override void Awake(){
        base.Awake();

        //Ammo = 0;
        ClipSize = 1;
        Description = "A gun";
        Name = "Gun";
    }
    protected override void Start(){
    }

    protected override void Update(){
    }

    /*public void AddAmmo(uint Amount){
        Ammo += Amount;
    }

    public virtual void RemoveAmmo(uint Amount){
        Ammo -= Amount;
    }*/
    
    public override void Activate(){
        if (PlayerReference.InventoryReference.Find("Ammo") > 0){
            GameObject ProjectileInstance = Instantiate(Projectile, ProjectileSpawnPoint.transform.position, transform.rotation);

            ProjectileInstance.GetComponent<Rigidbody>().velocity = transform.forward;

            PlayerReference.InventoryReference.RemoveFromInventory("Ammo", 1);
        }
        else{
            //Empty click noise
            Debug.Log("No Ammo");
        }
    }
}
