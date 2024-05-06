using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    [SerializeField] private Camera MiniMapCamera;
    public float PrevCameraSize;
    void Start()
    {
        PrevCameraSize = MiniMapCamera.orthographicSize;
    }
    public void ZoomToggle()
    {
        if(MiniMapCamera.orthographicSize == PrevCameraSize)
        {
            MiniMapCamera.orthographicSize = PrevCameraSize*2;         
        }
        else
        {
            MiniMapCamera.orthographicSize = PrevCameraSize;            
        }

    }
}
