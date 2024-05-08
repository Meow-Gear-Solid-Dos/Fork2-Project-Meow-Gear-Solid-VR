using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Item_Ammo : MonoBehaviour
{
    public string itemName;
    public GameObject floatingTextBox;
    public bool hasBeenPickedUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnGrab()
    {
        ShowText();
        hasBeenPickedUp = true;
    }
    public void ShowText()
    {
        string itemNameText = itemName;
        Transform itemPosition = gameObject.GetComponent<Transform>();
        if(floatingTextBox)
        {
            GameObject prefab = Instantiate(floatingTextBox, itemPosition.position, Quaternion.Euler(0, 360, 0), itemPosition);
            prefab.GetComponentInChildren<TMP_Text>().text = itemNameText;
            Destroy(prefab, .5f);
        }
    }
}
