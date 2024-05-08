using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAI : MonoBehaviour
{
    public bool canSeePlayer;
    public FieldOfView fieldOfView;
    public bool hasBeenAlerted;
    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponentInChildren<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        hasBeenAlerted = EventBus.Instance.inAlertPhase;
        canSeePlayer = fieldOfView.canSeeTarget;
    }
}
