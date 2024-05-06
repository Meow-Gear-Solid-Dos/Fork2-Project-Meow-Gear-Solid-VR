using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Cardboard_Box : Item_Parent
{
    public bool equipped;
    public GameObject HUDCanvas;
    public GameObject boxOverlay;
    public GameObject instantiatedOverlay;
    public float overlaySpeed = 10f;
    protected override void Awake(){
    }
    void Start()
    {
        HUDCanvas = GameObject.FindGameObjectWithTag("HUD");
        instantiatedOverlay = Instantiate(boxOverlay, HUDCanvas.transform);
        instantiatedOverlay.SetActive(false);
    }
    public override void Activate(){
        if(equipped)
        {
            equipped = false;
            //StartCoroutine("BoxUp");
            instantiatedOverlay.SetActive(false);
        }
        else
        {
            equipped = true;
            instantiatedOverlay.SetActive(true);
            
            RectTransform rectTransform = instantiatedOverlay.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0f,-95f);
            //StartCoroutine(BoxDown(rectTransform));

        }

    }
    
    public override void OnRelease(){
        equipped = false;
        instantiatedOverlay.SetActive(false);
        //inventory.AddToInventory(ItemPrefab, 1);
        //ShowText(ItemPrefab);

    }
    public IEnumerator BoxUp()
    {
        if(instantiatedOverlay.transform.localPosition.y <= 10)
        {
            instantiatedOverlay.transform.Translate(0f, overlaySpeed * Time.deltaTime, 0f);

        }
        yield return new WaitForEndOfFrame();
    }
    public IEnumerator BoxDown(RectTransform rectTransform)
    {
            //rectTransform.anchoredPosition = new Vector2(0f,-95f);
            
            if(rectTransform.anchoredPosition.y >= -95f)
            {
                rectTransform.anchoredPosition += new Vector2(0f, -overlaySpeed * Time.deltaTime);
            }
             yield return new WaitForEndOfFrame();
    }
}
