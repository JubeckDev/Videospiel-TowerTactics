using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{
    public int pointCost;
    public string description;
    public TextMeshProUGUI title;
    public Image icon;
    public GameObject[] nextButtons;
    public GameObject lockObject;
    public bool hasBeenBought;
    public int upgradeSelected;
    public GameObject halo;
    public GameObject selectionFrame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
