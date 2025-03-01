using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InfoPanel : MonoBehaviour
{
    public GameObject towerPanel;
    public GameObject towerMenu;

    public string[] descriptions;

    public Slider[] statSliders;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI damageTypeText;
    public TextMeshProUGUI type;

    public int towerSelectedNumber;

    public float[] statsMax;

    public GameObject skillTree;

    public Button[] infoButtons;
    public Color infoButtonSelected;
    public Color infoButtonNotSelected;

    public Color[] sliderColors;

    float[] statApplied = new float[5];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) CloseWindow();
    }
    public void OpenWindow(int towerSelectedNumber)
    {
        if (gameObject.activeSelf && this.towerSelectedNumber == towerSelectedNumber)
        {
            CloseWindow();
            return;
        }
        
        this.towerSelectedNumber = towerSelectedNumber;

        gameObject.SetActive(true);

        SetTexts();
        SetInfoButtonColor();
        CalculateSliderValues();
        SetSliderColors();
    }
    public void SetInfoButtonColor()
    {
        for (int i = 0; i < infoButtons.Length; i++)
        {
            infoButtons[i].GetComponent<Image>().color = towerSelectedNumber == i ? infoButtonSelected : infoButtonNotSelected;
        }
    }
    public void SetTexts()
    {
        type.text = towerPanel.GetComponent<TowerPanel>().towerTypes[towerSelectedNumber];
        descriptionText.text = descriptions[towerSelectedNumber];
        damageTypeText.text = towerMenu.GetComponent<SpawnTower>().towers[towerSelectedNumber].GetComponent<Stats>().damageType;
    }
    public int CalculateMaxDamage()
    {
        int maxDamage = -1;
        int tempDamage = -1;

        for (int i = 0; i < towerMenu.GetComponent<SpawnTower>().towers.Length; i++)
        {
            tempDamage = towerMenu.GetComponent<SpawnTower>().towers[i].GetComponent<Stats>().damage;

            if (tempDamage > maxDamage)
            {
                maxDamage = tempDamage;
            }
        }
        return maxDamage;
    }
    public float CalculateMaxRange()
    {
        float maxRange = -1;
        float tempRange = -1;

        for (int i = 0; i < towerMenu.GetComponent<SpawnTower>().towers.Length; i++)
        {
            tempRange = towerMenu.GetComponent<SpawnTower>().towers[i].GetComponent<Stats>().range;

            if (towerMenu.GetComponent<SpawnTower>().towers[i].GetComponent<Stats>().towerType == "Ship" && skillTree.GetComponent<SkillTree>().achievementUnlocked[10]) tempRange += skillTree.GetComponent<SkillTree>().achievementIncreases[10];
            if (towerSelectedNumber >= 3 && skillTree.GetComponent<SkillTree>().achievementUnlocked[5]) tempRange += skillTree.GetComponent<SkillTree>().achievementIncreases[5];


            if (tempRange > maxRange)
            {
                maxRange = tempRange;
            }
        }
        return maxRange;
    }
    public float CalculateMaxAttackSpeed()
    {
        float maxAttackSpeed = 1000;
        float tempAttackSpeed = 1000;

        for (int i = 0; i < towerMenu.GetComponent<SpawnTower>().towers.Length; i++)
        {
            tempAttackSpeed = towerMenu.GetComponent<SpawnTower>().towers[i].GetComponent<Stats>().attackSpeed;

            if (towerSelectedNumber >= 3 && skillTree.GetComponent<SkillTree>().achievementUnlocked[6]) tempAttackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[6];
            if (3 < towerSelectedNumber && towerSelectedNumber >= 7 && skillTree.GetComponent<SkillTree>().achievementUnlocked[12]) tempAttackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[12];

            if (tempAttackSpeed < maxAttackSpeed)
            {
                maxAttackSpeed = tempAttackSpeed;
            }
        }
        return maxAttackSpeed;
    }
    public int CalculateMaxPiercing()
    {
        int maxPiercing = -1;
        int tempPiercing = -1;

        for (int i = 0; i < towerMenu.GetComponent<SpawnTower>().towers.Length; i++)
        {
            tempPiercing = towerMenu.GetComponent<SpawnTower>().towers[i].GetComponent<Stats>().piercing;

            if (tempPiercing > maxPiercing)
            {
                maxPiercing = tempPiercing;
            }
        }
        return maxPiercing;
    }
    public float CalculateMaxBulletSpeed()
    {
        float maxBulletSpeed = -1;
        float tempBulletSpeed = -1;

        for (int i = 0; i < towerMenu.GetComponent<SpawnTower>().towers.Length; i++)
        {
            tempBulletSpeed = towerMenu.GetComponent<SpawnTower>().towers[i].GetComponent<Stats>().bulletMovementSpeed;

            if (towerSelectedNumber >= 3 && skillTree.GetComponent<SkillTree>().achievementUnlocked[7]) tempBulletSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[7];
            if (3 < towerSelectedNumber && towerSelectedNumber >= 7 && skillTree.GetComponent<SkillTree>().achievementUnlocked[8]) tempBulletSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[8];

            if (tempBulletSpeed > maxBulletSpeed)
            {
                maxBulletSpeed = tempBulletSpeed;
            }
        }
        return maxBulletSpeed;
    }
    public void CalculateSliderValues()
    {
        //Calculate maxes
        statsMax[0] = CalculateMaxDamage();
        statsMax[1] = CalculateMaxRange();
        statsMax[2] = CalculateMaxAttackSpeed();
        statsMax[3] = CalculateMaxPiercing();
        statsMax[4] = CalculateMaxBulletSpeed();

        //Set slider maxes to calculated maxes
        for (int i = 0; i < statSliders.Length; i++)
        {
            statSliders[i].maxValue = statsMax[i];
        }

        //Set slider values to stat of tower
        statApplied[0] = towerMenu.GetComponent<SpawnTower>().towers[towerSelectedNumber].GetComponent<Stats>().damage;
        statApplied[1] = towerMenu.GetComponent<SpawnTower>().towers[towerSelectedNumber].GetComponent<Stats>().range;
        statApplied[2] = statsMax[2] / towerMenu.GetComponent<SpawnTower>().towers[towerSelectedNumber].GetComponent<Stats>().attackSpeed;
        statApplied[3] = towerMenu.GetComponent<SpawnTower>().towers[towerSelectedNumber].GetComponent<Stats>().piercing;
        statApplied[4] = towerMenu.GetComponent<SpawnTower>().towers[towerSelectedNumber].GetComponent<Stats>().bulletMovementSpeed;

        //add bonuses
        if (towerMenu.GetComponent<SpawnTower>().towers[towerSelectedNumber].GetComponent<Stats>().towerType == "Ship" && skillTree.GetComponent<SkillTree>().achievementUnlocked[10]) statApplied[1] += skillTree.GetComponent<SkillTree>().achievementIncreases[10];
        if (towerSelectedNumber >= 3 && skillTree.GetComponent<SkillTree>().achievementUnlocked[5]) statApplied[1] += skillTree.GetComponent<SkillTree>().achievementIncreases[5];
        if (towerSelectedNumber >= 3 && skillTree.GetComponent<SkillTree>().achievementUnlocked[6]) statApplied[2] += skillTree.GetComponent<SkillTree>().achievementIncreases[6];
        if (3 < towerSelectedNumber && towerSelectedNumber >= 7 && skillTree.GetComponent<SkillTree>().achievementUnlocked[12]) statApplied[2] += skillTree.GetComponent<SkillTree>().achievementIncreases[12];
        if (towerSelectedNumber >= 3 && skillTree.GetComponent<SkillTree>().achievementUnlocked[7]) statApplied[4] += skillTree.GetComponent<SkillTree>().achievementIncreases[7];
        if (3 < towerSelectedNumber && towerSelectedNumber >= 7 && skillTree.GetComponent<SkillTree>().achievementUnlocked[8]) statApplied[4] += skillTree.GetComponent<SkillTree>().achievementIncreases[8];
        
        for (int i = 0; i < statApplied.Length; i++)
        {
            statSliders[i].value = statApplied[i];
        }
    }
    public void SetSliderColors()
    {
        for (int i = 0; i < statSliders.Length; i++)
        {
            int portion = Mathf.RoundToInt(sliderColors.Length - (statsMax[i] / statApplied[i]));
            if (portion < 0) portion = 0;
            statSliders[i].fillRect.GetComponent<Image>().color = sliderColors[portion];
        }
    }
    public void CloseWindow()
    {
        towerSelectedNumber = -1;

        SetInfoButtonColor();

        gameObject.SetActive(false);
    }
}
