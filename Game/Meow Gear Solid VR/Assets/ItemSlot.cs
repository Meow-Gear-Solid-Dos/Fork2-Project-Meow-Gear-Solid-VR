using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    //Item equpping
    public bool equipped = false;
    public Quaternion defaultRotation;
    public RawImage equipmentIcon;

    //Will link the item name to the name section in the actual inventory slot
    public GameObject itemPrefab;
    public TMP_Text itemNameText;
    public TMP_Text MaxAmmoText;
    public TMP_Text divided;
    public TMP_Text CurrentAmmoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
