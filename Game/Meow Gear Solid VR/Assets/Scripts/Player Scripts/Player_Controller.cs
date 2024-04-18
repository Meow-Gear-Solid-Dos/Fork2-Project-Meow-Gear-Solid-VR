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

    private bool ItemHeld;

    Player_Input PlayerInput;

    private void Awake(){
        //RigidBody = GetComponent<Rigidbody>();
        CharacterControllerReference = GetComponent<CharacterController>();
        PlayerReference = GetComponent<Entity_Player>();

        PlayerInput = new Player_Input();

        ItemHeld = false;

        PlayerInput.Player.Enable();
        PlayerInput.Player.ItemInteract.performed += EquipItem;
        PlayerInput.Player.Jump.performed += Jump;
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

    public bool GetItemHeld(){
        return ItemHeld;
    }

    public void GrabEnd(InputAction.CallbackContext Context){
        if (Context.canceled){
            //Debug.Log("Grab End");

            ItemHeld = false;

            if (BagReference.GetOverlappingItem() != null){
                PlayerReference.InventoryReference.AddToInventory(BagReference.GetOverlappingItem(), 1);

                BagReference.CurrentItem = BagReference.GetOverlappingItem();

                Debug.Log("Valid Item dropped");

                BagReference.DestroyOverlappingItem();
            }
            else{
                Debug.Log("Invalid item dropped");
            }
        }
    }

    public void GrabStart(InputAction.CallbackContext Context){
        if (Context.performed){
            //Debug.Log("Grab");

            ItemHeld = true;
        }
    }

    public void Jump(InputAction.CallbackContext Context){
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
            Debug.Log("Secondary Pressed");
            PlayerReference.ToggleEquippedItem();
        }
    }
}
