using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : Controller{
    public InputAction InputSystem;
    public CharacterController CharacterControllerReference;
    public Entity_Player PlayerReference;
    //public GameObject Cat;

    Player_Input PlayerInput;

    private void Awake(){
        //RigidBody = GetComponent<Rigidbody>();
        CharacterControllerReference = GetComponent<CharacterController>();
        PlayerReference = GetComponent<Entity_Player>();

        PlayerInput = new Player_Input();

        PlayerInput.Player.Enable();
        PlayerInput.Player.Jump.performed += Jump;
        PlayerInput.Player.Move.performed += Move;
        PlayerInput.Player.Grab.performed += Grab;
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

    public void Grab(InputAction.CallbackContext Context){
        if (Context.performed){
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
