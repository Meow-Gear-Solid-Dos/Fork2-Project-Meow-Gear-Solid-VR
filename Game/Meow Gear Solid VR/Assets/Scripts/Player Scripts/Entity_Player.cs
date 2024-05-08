using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Player : MonoBehaviour{
    public Camera CameraReference;
    public Entity_Statistics EntityStatistics;
    public Inventory InventoryReference;
    public GameObject RightHandReference;
    public GameObject BagReference;
    public Inventory_Bag InventoryBagReference;
    //public Transform Transform;
    //public GameObject Cat;
    // Start is called before the first frame update

    private bool ItemEquipped;

    void Awake(){
    }

    void Start(){
        InventoryReference = GetComponent<Inventory>();
        InventoryBagReference = GetComponent<Inventory_Bag>();

        ItemEquipped = false;
    }

    // Update is called once per frame
    void Update(){
        Quaternion Rotation = Quaternion.Euler(0.0f, CameraReference.transform.localEulerAngles.y, 0.0f);

        //transform.rotation = Rotation;
    }

    public bool GetItemEquipped(){
        return ItemEquipped;
    }

    public void SetItemEquipped(bool Equipped){
        ItemEquipped = Equipped;
    }

}
