using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDescription : MonoBehaviour
{
    public GameObject descriptionBox;
    public TMP_Text itemName;
    public TMP_Text itemDesc;
    public bool hasInfo;
    // Start is called before the first frame update
    void Start()
    {
        hasInfo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasInfo == true)
        {
            descriptionBox.SetActive(true);
        }
        else
        {
            descriptionBox.SetActive(false);
        }
    }
}
