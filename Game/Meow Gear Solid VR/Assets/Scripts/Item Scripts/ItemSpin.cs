using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{
    public float spinSpeed = 50f; // Adjust the speed of the spin in the Inspector
    public Vector3 TargetScale = Vector3.one * .25f;
    Vector3 startScale;
    public bool grabbed;

    void Start()
    {
        startScale = transform.localScale;
    }
    void Update()
    {
        if(grabbed)
        {
            return;
        }
        else
        {
        // Rotate the GameObject around the Y axis
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        //Always makes sure object returns to being top right
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = new Quaternion (0.0F,transform.rotation.y,0.0F,transform.rotation.w);
        transform.rotation = Quaternion.Slerp(startRotation, endRotation, Time.deltaTime*2.0F);            
        }

    }
    public void WhenGrabbed()
    {
        transform.localScale = TargetScale;
        grabbed = true;
    }
    public void WhenLetGo()
    {
        transform.localScale = startScale;
        grabbed = false;
    }

}
