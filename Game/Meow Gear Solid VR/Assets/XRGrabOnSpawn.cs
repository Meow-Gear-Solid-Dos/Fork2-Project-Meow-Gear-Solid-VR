using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabOnSpawn : XRBaseInteractable
{
    [SerializeField]
    private Transform transformToInstantiate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {   
        gameObject.transform.parent = transformToInstantiate;
        // Get grab interactable from prefab
        XRGrabInteractable objectInteractable = gameObject.GetComponent<XRGrabInteractable>();
        
        // Select object into same interactor
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);
        
        base.OnSelectEntered(args);
    }
}
