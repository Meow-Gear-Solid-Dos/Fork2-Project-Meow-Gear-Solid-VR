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
        EntityStatistics = new Entity_Statistics();

        CameraReference = Camera.main;

        EntityStatistics.Jumps = 1;
        EntityStatistics.MovementSpeed = 5.0f;

        //Transform = Cat.GetComponent<Transform>();
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

    public void ToggleEquippedItem(){
        if (ItemEquipped){
            Debug.Log("Cant spawn, holding item");
        }
        else{
            if (InventoryReference.InstancedInventory.Count != 0){
                GameObject ItemReference = InventoryReference.InstancedInventory[0];

                ItemReference.SetActive(true);
                ItemReference.transform.position = RightHandReference.transform.position;
                ItemReference.transform.rotation = Quaternion.identity;

                Debug.Log("Item should be spawned");

                InventoryReference.RemoveFromInventory(ItemReference);
            }
            else{
                Debug.Log("No Items in inventory");
            }
        }
    }
}
