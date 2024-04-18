using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Player : MonoBehaviour{
    public Camera CameraReference;
    public Entity_Statistics EntityStatistics;
    public Inventory InventoryReference;
    //public Transform Transform;
    //public GameObject Cat;
    // Start is called before the first frame update

    public GameObject ItemReference;

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

    public void ToggleEquippedItem(){
        if (ItemReference != null){
            InventoryReference.AddToInventory(ItemReference, 1);

            Destroy(ItemReference);

            Debug.Log(InventoryReference.InventoryReference[0].Value);

            ItemEquipped = false;
        }
        else{
            if (InventoryReference.InventoryReference[0].Key != null){
                ItemReference = Instantiate(InventoryReference.InventoryReference[0].Key, transform.position, Quaternion.identity);

                ItemEquipped = true;

                Debug.Log("Item Spawned Somewhere");
            }
            else{
                Debug.Log("No Items in inventory");
            }
        }
    }
}
