using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gold : MonoBehaviour
{
    public int initialGold = 700;
    public int gold;

    public int[] startingGold;

    public TextMeshProUGUI goldText;

    public Button[] purchaseTower;

    public Color imageColorDefault;
    public Color imageColorFaded;

    public GameObject maps;

    public GameObject towerMenu;

    public GameObject achievements;

    public GameObject skillTree;

    // Start is called before the first frame update
    void Start()
    {
        SetGold();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeGold(int gold)
    {
        this.gold += gold;
        UpdateGoldText();
        CheckIfEnoughGoldForTower();

        achievements.GetComponent<Achievements>().SetNewProgress(4, this.gold);
    }
    public void SetGold()
    {
        initialGold = startingGold[maps.GetComponent<Difficulty>().difficulty];
        //Add gold if achievement unlocked< startingGold.Length; i++)
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[24]) initialGold += Mathf.RoundToInt(skillTree.GetComponent<SkillTree>().achievementIncreases[24]);
        ResetGold();
    }
    public void ResetGold()
    {
        gold = initialGold;
        UpdateGoldText();
        CheckIfEnoughGoldForTower();
    }
    public void UpdateGoldText()
    {
        goldText.text = gold.ToString();
    }
    public void CheckIfEnoughGoldForTower()
    {
        for(int i = 0; i < purchaseTower.Length; i++)
        {
            if (gold >= int.Parse(purchaseTower[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text))
            {
                purchaseTower[i].interactable = true;
                purchaseTower[i].transform.GetChild(0).GetComponent<Image>().color = imageColorDefault;
            }
            else
            {
                purchaseTower[i].interactable = false;
                purchaseTower[i].transform.GetChild(0).GetComponent<Image>().color = imageColorFaded;
            }
        }
    }
}