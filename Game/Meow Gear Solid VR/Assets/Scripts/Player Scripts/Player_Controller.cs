using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class Player_Controller : Controller{
    public InputAction InputSystem;
    public CharacterController CharacterControllerReference;
    public Inventory_Bag BagReference;
    public Inventory inventory;
    public Entity_Player PlayerReference;
    public PauseMenu pause;
    public NewCodecTrigger codec;
    public GameObject heldItem;
    public GameObject floatingTextBox;
    public AudioClip pickUpSound;
    //public GameObject Cat;

    private bool ItemHeld;

    public Player_Input PlayerInput;

    private void Awake(){
        //RigidBody = GetComponent<Rigidbody>();
        CharacterControllerReference = GetComponent<CharacterController>();
        PlayerReference = GetComponent<Entity_Player>();
        inventory = GetComponent<Inventory>();

        PlayerInput = new Player_Input();

        ItemHeld = false;

        PlayerInput.Player.Enable();
        PlayerInput.Player.Spawn.performed += EquipItem;
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
        if (Context.canceled){
            //Debug.Log("Grab End");
            heldItem = null;
            PlayerReference.SetItemEquipped(false);

            if (BagReference.GetOverlappingItem() != null){
                BagReference.CurrentItem = BagReference.GetOverlappingItem();
                PlayerReference.InventoryReference.AddToInventory(BagReference.CurrentItem.GetComponent<Item_Parent>().ItemPrefab, 1);

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
            Debug.Log("Grab");

            ItemHeld = true;
            if(heldItem != null)
            {
                AudioSource.PlayClipAtPoint(pickUpSound, transform.position, .5f);
                inventory.AddToInventory(heldItem, 1);
                ShowText(heldItem);
            }

        }
    }

    public void EquipItem(InputAction.CallbackContext Context){
        if (Context.performed){
            Debug.Log("Secondary Pressed");
            PlayerReference.ToggleEquippedItem();
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

    void OnTriggerEnter(Collider OtherCollider){
        if (OtherCollider.gameObject.GetComponent<Item_Parent>() != null){
            heldItem = OtherCollider.gameObject;

            //Debug.Log("Overlapping item");
        }
    }
    //Allows for the display of text in floating text box.
    void ShowText(GameObject heldItem)
    {
        string itemNameText = heldItem.GetComponent<Item_Parent>().itemName;
        Transform itemPosition = heldItem.GetComponent<Transform>();
        if(floatingTextBox)
        {
            GameObject prefab = Instantiate(floatingTextBox, itemPosition.position, Quaternion.Euler(0, -90, 0), itemPosition);
            prefab.GetComponentInChildren<TMP_Text>().text = itemNameText;
            Destroy(prefab, .5f);
        }
    }
}
