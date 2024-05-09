using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class Player_Controller : Controller{
    public InputAction InputSystem;
    public CharacterController CharacterControllerReference;
    public Item_Ranged_Weapon pistol;
    public Inventory_Bag BagReference;
    public PauseMenu pause;
    public NewCodecTrigger codec;
    public GameObject heldItem;
    public GameObject floatingTextBox;
    public Inventory InventoryReference;
    public Inventory_Bag InventoryBagReference;
    public AudioSource MusicSource;
    public AudioClip Music1;
    //public GameObject Cat;

    private bool ItemHeld;

    //Handles reading inputs from the occulus
    public Player_Input PlayerInput;

    private void Awake(){
        //RigidBody = GetComponent<Rigidbody>();
        CharacterControllerReference = GetComponent<CharacterController>();

        PlayerInput = new Player_Input();

        ItemHeld = false;

        PlayerInput.Player.Enable();
        //PlayerInput.Player.Spawn.performed += EquipItem;
        //PlayerInput.Player.Move.performed += Move;
        PlayerInput.Player.Grab.canceled += GrabEnd;
        PlayerInput.Player.Grab.performed += GrabStart;
        PlayerInput.Player.Paws.performed += pause.TPause;
        PlayerInput.Player.Codec.performed += codec.StartCodec;
    }

    void Start(){
        InventoryBagReference = GetComponent<Inventory_Bag>();
        InventoryReference = GetComponent<Inventory>();
        heldItem = null;
    }

    private void OnEnable(){
        //InputSystem.Enable();
    }

    private void OnDisable(){
        //InputSystem.Disable();
    }

    void Update(){
		if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }
    }

    private void FixedUpdate(){
        PlayerInput.Player.Move.ReadValue<Vector2>();
    }

    public bool GetItemHeld(){
        return ItemHeld;
    }

    public void GrabEnd(InputAction.CallbackContext Context){
        if (Context.canceled)
        {
            ItemHeld = false;
            if (BagReference.GetOverlappingItem() != null){
                BagReference.CurrentItem = BagReference.GetOverlappingItem();

                Item_Parent Item = BagReference.CurrentItem.GetComponent<Item_Parent>();

                //Debug.Log("Grab End");
                if ((InventoryReference.Find(BagReference.CurrentItem) + 1) <= Item.StackSize){
                    InventoryReference.AddToInventory(BagReference.CurrentItem);
                }
                else
                {
                    return;
                }
                BagReference.DeactivateOverlappingItem();
            }
            heldItem = null;
            pistol = null;
        }
    }

    //Adds item to hand
    public void GrabStart(InputAction.CallbackContext Context){
        if (Context.performed){
            Debug.Log("Grab");
            ItemHeld = true;
            InventoryReference.AddToInventory(heldItem);
            if(heldItem.GetComponent<Item_Ranged_Weapon>() != null)
            {
                pistol = heldItem.GetComponent<Item_Ranged_Weapon>();
                PlayerInput.Player.Spawn.performed += pistol.TReload;
            }
        }
    }

    public void EquipItem(InputAction.CallbackContext Context){
        if (Context.performed){
            Debug.Log("Secondary Pressed");
        }
    }

    //Checks for items in player hands. If it's part of the item parent family or ammo, it can get added.
    void OnTriggerEnter(Collider OtherCollider){
        if ((OtherCollider.gameObject.GetComponent<Item_Parent>() != null) || (OtherCollider.gameObject.GetComponent<Item_Ammo>() != null))
        {
            heldItem = OtherCollider.gameObject;

            //Debug.Log("Overlapping item");
        }
    }
}
