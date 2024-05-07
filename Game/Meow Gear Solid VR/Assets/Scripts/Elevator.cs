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
    public bool playerIsHere;
    public bool canMove;
    //Lines down here are for path traversal
    public float elevatorSpeed = 3f;
    public Transform path;
    public  List<Transform> myNodes;
    Vector3 nodePosition;
    public Transform myCurrentNode;
    public int index;
    //keep track of current nodes
    private int currentNode = 0;
    //Audio Stuff
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        playerIsHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere == true && canMove == true)
        {
            if(elevatorFloor.transform.position.y < myNodes.ElementAt(1).position.y)
            {
                elevatorFloor.transform.Translate(0f, elevatorSpeed * Time.deltaTime, 0f);
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
            playerIsHere = true;
            StartCoroutine("PlayerIsHere");
            StopCoroutine("PlayerNotHere");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == "Player"))
        {
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
