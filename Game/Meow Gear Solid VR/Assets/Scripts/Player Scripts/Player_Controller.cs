using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : Controller{
    public InputAction InputSystem;
    public CharacterController CharacterControllerReference;
    public Inventory_Bag BagReference;
    public Entity_Player PlayerReference;
    public PauseMenu pause;
    public NewCodecTrigger codec;
    //public GameObject Cat;

    private bool ItemHeld;

    public Player_Input PlayerInput;

    private void Awake(){
        //RigidBody = GetComponent<Rigidbody>();
        CharacterControllerReference = GetComponent<CharacterController>();
        PlayerReference = GetComponent<Entity_Player>();

        PlayerInput = new Player_Input();

        ItemHeld = false;

        PlayerInput.Player.Enable();
        //PlayerInput.Player.Jump.performed += Jump;
        PlayerInput.Player.Move.performed += Move;
        PlayerInput.Player.Grab.canceled += GrabEnd;
        PlayerInput.Player.Grab.performed += GrabStart;
        PlayerInput.Player.Paws.performed += pause.TPause;
        PlayerInput.Player.Codec.performed += codec.StartCodec;
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
        /*if (Context.canceled){
            Debug.Log("Grab End");

            ItemHeld = false;

            if (BagReference.GetOverlappingItem() != null){
                PlayerReference.InventoryReference.AddToInventory(BagReference.GetOverlappingItem().GetComponent<Item_Parent>(), 1);

                BagReference.CurrentItem = BagReference.GetOverlappingItem();
            }
        }*/
    }

    public void GrabStart(InputAction.CallbackContext Context){
        if (Context.performed){
            Debug.Log("Grab");

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
}
