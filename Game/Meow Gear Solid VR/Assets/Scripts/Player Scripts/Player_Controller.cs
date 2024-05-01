using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : Controller{
    public InputAction InputSystem;
    public CharacterController CharacterControllerReference;
    public Inventory_Bag BagReference;
    public Entity_Player PlayerReference;
    //public GameObject Cat;

    Player_Input PlayerInput;

    private void Awake(){
        //RigidBody = GetComponent<Rigidbody>();
        CharacterControllerReference = GetComponent<CharacterController>();
        PlayerReference = GetComponent<Entity_Player>();

        PlayerInput = new Player_Input();

        PlayerInput.Player.Enable();
        PlayerInput.Player.ItemInteract.performed += EquipItem;
        PlayerInput.Player.MainMenu.performed += MainMenu;
        PlayerInput.Player.Move.performed += Move;
        PlayerInput.Player.Grab.canceled += GrabEnd;
        PlayerInput.Player.Grab.performed += GrabStart;
        PlayerInput.Player.Punch.performed += Punch;
    }

    void Start(){
    }

    private void OnEnable(){
        //InputSystem.Enable();
    }

    private void OnDisable(){
        //InputSystem.Disable();
    }

    void Update(){
        //Direction = InputSystem.ReadValue<Vector2>();
    }

    private void FixedUpdate(){
        PlayerInput.Player.Move.ReadValue<Vector2>();
    }

    public void GrabEnd(InputAction.CallbackContext Context){
        if (Context.canceled){
            //Debug.Log("Grab End");

            PlayerReference.SetItemEquipped(false);

            if (BagReference.GetOverlappingItem() != null){
                BagReference.CurrentItem = BagReference.GetOverlappingItem();

                Item_Parent Item = BagReference.CurrentItem.GetComponent<Item_Parent>();

                switch (Item.InventoryClass){
                    case ItemInventoryClass.Instanced:
                        if ((PlayerReference.InventoryReference.Find(BagReference.CurrentItem) + 1) <= Item.StackSize){
                            PlayerReference.InventoryReference.AddToInventory(BagReference.CurrentItem);
                        }
                        else{
                            return;
                        }

                        break;

                    case ItemInventoryClass.Static:
                        if ((PlayerReference.InventoryReference.Find(Item.Name) + Item.Amount) <= Item.StackSize){
                            PlayerReference.InventoryReference.AddToInventory(Item.Name, Item.Amount);
                        }
                        else{
                            return;
                        }

                        break;

                    default:
                        break;
                }

                BagReference.DeactivateOverlappingItem();
            }
            else{
            }
        }
    }

    public void GrabStart(InputAction.CallbackContext Context){
        if (Context.performed){
            //Debug.Log("Grab");

            PlayerReference.SetItemEquipped(true);
        }
    }

    public void MainMenu(InputAction.CallbackContext Context){
        if (Context.performed){
            //RigidBody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext Context){
        Vector2 Input = Context.ReadValue<Vector2>();
        CharacterControllerReference.Move((Quaternion.Euler(0, PlayerReference.CameraReference.transform.eulerAngles.y, 0) * (new Vector3(Input.x, 0.0f, Input.y) * PlayerReference.EntityStatistics.MovementSpeed) * Time.deltaTime));
    }

    public void Punch(InputAction.CallbackContext Context){
        //it might be bound to the wrong button cuz this version of the plugin seems to be missing bindings

        //Should just need to set IsPunching or whatever to true. If not I'lll have to chage the context
    }

    public void EquipItem(InputAction.CallbackContext Context){
        if (Context.performed){
            PlayerReference.ToggleEquippedItem();
        }
    }
}
