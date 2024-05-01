using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardFunction : MonoBehaviour
{
    public Transform box;
    public float velocityRequirement = 3f;
    public bool isMoving;
    public float speed;
    public bool alertPhase;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BoxSpeed(box));
        
    }

    // Update is called once per frame
    void Update()
    {
        alertPhase = EventBus.Instance.inAlertPhase;
        if(isMoving)
        {
            transform.gameObject.layer = 6;
            isMoving = true;
        }
        else
        {
            if(alertPhase == true)
            {
                transform.gameObject.layer = 6;

            }
            else
            {
                transform.gameObject.layer = 8;              
            }

        }
    }
    public IEnumerator BoxSpeed(Transform box)
    {
        while(true)
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

            yield return new WaitForEndOfFrame();
        }
    }
}
