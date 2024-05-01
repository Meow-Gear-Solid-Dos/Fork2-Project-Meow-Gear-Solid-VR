using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardFunction : MonoBehaviour
{
    public Transform box;
    public float velocityRequirement = .01f;
    public bool isMoving;
    public float speed;
    public bool alertPhase;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BoxSpeed(box));
        
        alertPhase = GameObject.FindGameObjectWithTag("AlertPhaseSystem").GetComponent<AlertPhase>().inAlertPhase;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = 6;
            }
            isMoving = true;
        }
        else
        {
            if(alertPhase == true)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.layer = 6;
                }
            }
            else
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.layer = 8;
                }
                isMoving = false;                
            }

        }
    }
    public IEnumerator BoxSpeed(Transform box)
    {
        var previous = box.position;
        previous.y = 0;
        yield return new WaitForSeconds(.001f);
        var current = box.position;
        current.y = 0;
        speed = ((current - previous).magnitude) / Time.deltaTime;
        if((speed >= velocityRequirement))
        {
            isMoving = true;
        }
        else
            isMoving = false;
    }
}
