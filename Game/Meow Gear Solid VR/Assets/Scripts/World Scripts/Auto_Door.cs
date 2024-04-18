using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{

    public GameObject autoDoor;
    public AudioSource doorSound;
    public float maximumOpenning;
    private float minimumClosing;
    public float doorSpeed = 5f;
    public bool xMovement;

    public bool playerIsHere;
    // Start is called before the first frame update
    void Start()
    {
        doorSound = GetComponent<AudioSource>();
        playerIsHere = false;

        //User needs to set bool to whether the door moves on the x or z axis.
        if(xMovement == true)
        {
            maximumOpenning = autoDoor.transform.position.x + 4f;
            minimumClosing = autoDoor.transform.position.x;
        }

        else
        {
            maximumOpenning = autoDoor.transform.position.z + 4f;
            minimumClosing = autoDoor.transform.position.z;
        }       

    }

    // Update is called once per frame
    void Update()
    {
        if(xMovement == true)
        {
                    if(playerIsHere)
                {
                    if(autoDoor.transform.position.x < maximumOpenning)
                    {
                        autoDoor.transform.Translate(doorSpeed * Time.deltaTime, 0f, 0f);
                    }
                }

                else
                {
                    if (autoDoor.transform.position.x > minimumClosing)
                    {
                        autoDoor.transform.Translate(-doorSpeed * Time.deltaTime, 0f, 0f);
                    }
                }
        }
        else
        {
                if(playerIsHere)
                {
                    if(autoDoor.transform.position.z < maximumOpenning)
                    {
                        autoDoor.transform.Translate(0f, 0f, doorSpeed * Time.deltaTime);
                    }
                }

                else
                {
                    if (autoDoor.transform.position.z > minimumClosing)
                    {
                        autoDoor.transform.Translate(0f, 0f, -doorSpeed * Time.deltaTime);
                    }
                }
        }
            
    }

    private void OnTriggerEnter(Collider col)
    {
        if((col.gameObject.tag == "Player")||(col.gameObject.tag == "Enemy"))
        {
            playerIsHere = true;
            doorSound.Play();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == "Player")||(col.gameObject.tag == "Enemy"))
        {
            playerIsHere = false;
        }
    }
}
