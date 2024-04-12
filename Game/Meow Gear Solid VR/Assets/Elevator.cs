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
    //Lines down here are for path traversal
    public float elevatorSpeed = 3f;
    public Transform path;
    public  List<Transform> myNodes;
    Vector3 nodePosition;
    public Transform myCurrentNode;
    public int index;
    //keep track of current nodes
    private int currentNode = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerIsHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere == true)
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
    }
}
