using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LookAtMe : MonoBehaviour
{
    public Transform lookAtThis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(lookAtThis.GetComponent<Transform>().position);
        Vector3 dir = lookAtThis.GetComponent<Transform>().position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
