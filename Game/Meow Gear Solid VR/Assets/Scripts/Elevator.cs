using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Elevator : MonoBehaviour
{
    //Handles player detection
    public GameObject elevatorFloor;
    public Transform playerParent;
    public bool playerIsHere;
    public Rigidbody rigidBody;
    public bool canMove;
    //Lines down here are for path traversal
    public float elevatorSpeed = 3f;
    public Transform path;
    public  List<Transform> myNodes;
    Vector3 nodePosition;
    public Transform myCurrentNode;
    public int index;
    //Audio Stuff
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        playerIsHere = false;
        rigidBody = elevatorFloor.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerIsHere == true && canMove == true)
        {
            if(elevatorFloor.transform.position.y < myNodes.ElementAt(1).position.y)
            {
                //elevatorFloor.transform.Translate(0f, elevatorSpeed * Time.deltaTime, 0f);
                Vector3 move = new Vector3(0, 1, 0);
                move = move.normalized * elevatorSpeed * Time.deltaTime;
                rigidBody.MovePosition(elevatorFloor.transform.position + move);
            }
        
        }

        else
        {
            if(elevatorFloor.transform.position.y > myNodes.ElementAt(0).position.y)
            {
                elevatorFloor.transform.Translate(0f, -elevatorSpeed * Time.deltaTime, 0f);
            }
        }
            
    }

    private void OnTriggerEnter(Collider col)
    {
        if((col.gameObject.tag == "Player"))
        {
            //playerParent = col.transform.parent;
            //col.transform.SetParent(elevatorFloor.transform);
            playerIsHere = true;
            StartCoroutine("PlayerIsHere");
            StopCoroutine("PlayerNotHere");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == "Player"))
        {
            //col.transform.SetParent(playerParent);
            StartCoroutine("PlayerNotHere");
        }
    }
    IEnumerator PlayerNotHere()
    {
        yield return new WaitForSeconds(5f);
        playerIsHere = false;
        canMove = false;
    }
    IEnumerator PlayerIsHere()
    {
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(audioClip, 1f);
        canMove = true;
    }
}
