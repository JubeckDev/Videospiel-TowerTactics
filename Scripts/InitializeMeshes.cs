using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeMeshes : MonoBehaviour
{
    public GameObject meshDefault;
    public GameObject meshCanPlace;
    public GameObject meshCantPlace;

    public GameObject rangeVisualizer;

    public GameObject towerMenu;


    // Start is called before the first frame update
    void Start()
    {
        if(!towerMenu) towerMenu = GameObject.Find("Canvas/GameUI/TowerMenu");

        towerMenu.GetComponent<PlaceTower>().meshDefault = meshDefault;
        towerMenu.GetComponent<PlaceTower>().meshCanPlace = meshCanPlace;
        towerMenu.GetComponent<PlaceTower>().meshCantPlace = meshCantPlace;

        towerMenu.GetComponent<PlaceTower>().rangeVisualizer = rangeVisualizer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
