using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrestigeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject purchaseUI;

    public GameObject boughtUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(purchaseUI != null && !boughtUI.activeSelf) purchaseUI.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (purchaseUI != null && !boughtUI.activeSelf) purchaseUI.SetActive(false);
    }
}
