using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
    public GameObject[] towerCategory;

    public Image towerCategorieIcon;
    public Sprite[] towerCategorieSprites;

    public int towerCategorySelected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectTowerCategory(int number)
    {
        //add or subtract
        if(number == 1)towerCategorySelected++;
        else towerCategorySelected--;

        //if out of bounds set to other side of range
        if (towerCategorySelected >= towerCategory.Length) towerCategorySelected = 0;
        else if (towerCategorySelected < 0) towerCategorySelected = towerCategory.Length - 1;

        for (int i = 0; i < towerCategory.Length; i++)
        {
            towerCategory[i].SetActive(false);
        }
        towerCategory[towerCategorySelected].SetActive(true);
        towerCategorieIcon.sprite = towerCategorieSprites[towerCategorySelected];
    }
}
