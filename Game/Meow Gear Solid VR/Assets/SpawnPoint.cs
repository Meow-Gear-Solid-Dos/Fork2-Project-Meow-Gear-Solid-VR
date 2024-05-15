using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform player;
    public bool hasTeleported;
    // Start is called before the first frame update
    void Start()
    {
        hasTeleported = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasTeleported)
        {
            return;
        }
        else
        {
            player.transform.position = gameObject.transform.position;
            hasTeleported = true;
        }
    }
}
