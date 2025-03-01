using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeMeshesBanner : MonoBehaviour
{
    public GameObject meshDefault;
    public GameObject meshCanPlace;
    public GameObject meshCantPlace;

    public GameObject rangeVisualizer;

    public GameObject heroPanel;

    // Start is called before the first frame update
    void Start()
    {
        heroPanel = GameObject.Find("Canvas/GameUI/HeroPanel");

        heroPanel.GetComponent<PlaceHero>().meshDefault = meshDefault;
        heroPanel.GetComponent<PlaceHero>().meshCanPlace = meshCanPlace;
        heroPanel.GetComponent<PlaceHero>().meshCantPlace = meshCantPlace;

        heroPanel.GetComponent<PlaceHero>().rangeVisualizer = rangeVisualizer;
        meshDefault.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
