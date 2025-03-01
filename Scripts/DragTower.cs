using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragTower : MonoBehaviour, IPointerDownHandler
{
    public int towerNumber;
    public GameObject towerMenu;

    public TextMeshProUGUI goldCost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        towerMenu.GetComponent<SpawnTower>().SpawnNewTower(towerNumber, true);
    }
}
