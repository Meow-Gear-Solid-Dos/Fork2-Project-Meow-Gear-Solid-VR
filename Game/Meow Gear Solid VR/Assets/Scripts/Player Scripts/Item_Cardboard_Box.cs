using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Cardboard_Box : Item_Parent
{
    public bool equipped;
    public GameObject codecCanvas;
    public GameObject boxOverlay;
    protected override void Awake(){
    }
    void Start()
    {
        codecCanvas = GameObject.FindGameObjectWithTag("CodecCanvas");
        boxOverlay.SetActive(false);
    }
    public override void Activate(){
        if(equipped)
        {
            equipped = false;
            boxOverlay.SetActive(false);
        }
        else
        {
            equipped = true;
            boxOverlay.SetActive(true);
        }

    }
}
