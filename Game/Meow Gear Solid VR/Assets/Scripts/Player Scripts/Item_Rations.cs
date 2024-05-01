using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Rations : Item_Parent{
    public AudioSource source;
    public AudioClip shootingSound;
    public PlayerHealth playerHealth;
    public GameObject healthPack;
    public bool isUsed;
    protected override void Awake(){
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        maxAmmo = StackSize;
    }

    public override void Activate(){
        if((playerHealth.currentHealth < playerHealth.maxHealth) && currentAmmo > 0)
        {
            playerHealth.HealHealth(25f);
            source.PlayOneShot(shootingSound, .5f);
            Amount--;
            inventory.RemoveFromInventory(this.GetComponent<GameObject>(), 1);
            //inventoryDisplay.RemoveSlot(healthPack);
        }

    }
    public void Update()
    {
        if(isUsed == true)
        {
            healthPack.SetActive(false);
        }
        currentAmmo = Amount;
    }
    public void refill()
    {
        currentAmmo++;
    }
}
