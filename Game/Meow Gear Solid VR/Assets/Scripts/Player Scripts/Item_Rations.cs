using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Rations : Item_Parent{
    public AudioSource source;
    public AudioClip shootingSound;
    public PlayerHealth playerHealth;
    public bool isUsed;
    protected override void Awake(){
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        maxAmmo = StackSize;
    }
    public void Start()
    {
    }

    public override void Activate()
    {
        if((playerHealth.currentHealth < playerHealth.maxHealth) && currentAmmo > 0)
        {
            playerHealth.HealHealth(25f);
            source.PlayOneShot(shootingSound, .5f);
            Amount--;
            //inventoryDisplay.RemoveSlot(healthPack);
        }

    }
    public void Update()
    {
        currentAmmo = Amount;
        if(Amount <= 0)
        {
            inventory.RemoveFromInventory(gameObject);
            gameObject.SetActive(false);
        }
    }
    public void refill()
    {
        currentAmmo++;
    }
}
