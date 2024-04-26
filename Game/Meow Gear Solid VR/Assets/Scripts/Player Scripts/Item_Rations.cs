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
    }

    public override void Activate(){
        if((playerHealth.currentHealth < playerHealth.maxHealth) && currentAmmo > 0)
        {
            playerHealth.HealHealth(25f);
            source.PlayOneShot(shootingSound, .5f);
            currentAmmo--;
            isUsed = true;
            inventoryDisplay.RemoveSlot(healthPack);
        }

    }
    public void Update()
    {
        if(isUsed == true)
        {
            healthPack.SetActive(false);
        }
    }
    public void refill()
    {
        currentAmmo++;
    }
}
