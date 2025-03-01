using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public int hero;

    public Button[] heroButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHero(int number)
    {
        hero = number;

        for (int i = 0; i < heroButtons.Length; i++)
        {
            heroButtons[i].interactable = true;
        }
        heroButtons[number].interactable = false;
    }
}
