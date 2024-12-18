using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : Controller{
    public InputAction InputSystem;
    public CharacterController CharacterControllerReference;
    public Inventory_Bag BagReference;
    public Entity_Player PlayerReference;
    public Rigidbody RigidBodyReference;
    //public GameObject Cat;

    Player_Input PlayerInput;

    Vector2 MovementVelocity;
    Vector2 ControlRotation;

    const string MouseX = "Mouse X";
    const string MouseY = "Mouse Y";

    private void Awake(){
        RigidBodyReference = GetComponent<Rigidbody>();
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
        PlayerInput.Player.Move.canceled += StopMoving;
    }

    void Start(){
        MovementVelocity = new Vector2(0.0f, 0.0f);
        ControlRotation = new Vector2(0.0f, 0.0f);
    }

    private void OnEnable(){
        //InputSystem.Enable();
    }

    private void OnDisable(){
        //InputSystem.Disable();
    }

    void Update(){
        ControlRotation.x += (Input.GetAxis(MouseX) * PlayerReference.PlayerSettings.LookSpeedX);
        ControlRotation.y += (Input.GetAxis(MouseY) * PlayerReference.PlayerSettings.LookSpeedY);
        ControlRotation.y = Mathf.Clamp(ControlRotation.y, -90.0f, 90.0f);

        Quaternion XQuaternion = Quaternion.AngleAxis(ControlRotation.x, Vector3.up);
        Quaternion YQuaternion = Quaternion.AngleAxis(ControlRotation.y, Vector3.left);

        PlayerReference.CameraReference.transform.localRotation = (XQuaternion * YQuaternion);
    }

    private void FixedUpdate(){
        //PlayerInput.Player.Look.ReadValue<Vector2>();

        Vector3 Movement = PlayerReference.CameraReference.transform.forward * MovementVelocity.y + PlayerReference.CameraReference.transform.right * MovementVelocity.x;

        Movement.y = 0.0f;

        RigidBodyReference.AddForce(Movement.normalized * PlayerReference.EntityStatistics.MovementSpeed, ForceMode.VelocityChange);
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
        MovementVelocity = Context.ReadValue<Vector2>();
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

    public void StopMoving(InputAction.CallbackContext Context)
    {
        MovementVelocity = new Vector2(0.0f, 0.0f);
    }
}
