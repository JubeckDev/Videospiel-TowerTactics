using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArtifactPanel : MonoBehaviour
{
    public int[] artifactDropChances;
    public int artifactDropRate;

    public GameObject content;

    public string currentTowerType;

    public GameObject speedUpButton;

    public GameObject heroPanel;
    public GameObject towerPanel;
    public GameObject removeObjectPanel;
    public GameObject towerMenu;

    public GameObject cam;

    public string[] towerTypes;
    public Sprite[] icons;
    public int towerTypeNumber;
    public int currentBuffTowerNumber;
    public string[] towerNames;
    public string[] descriptions;

    public bool[] hasArtifact;

    public Image preview;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI description;

    // Start is called before the first frame update
    void Start()
    {
        SetDescriptions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropArtifactChance(int enemyInitialStage, string towerType)
    {
        if (towerType == null) return;

        currentTowerType = towerType;

        for (int i = 0; i < towerTypes.Length; i++)
        {
            if(towerTypes[i] == towerType)
            {
                towerTypeNumber = i;
                break;
            }
        }

        if (hasArtifact[towerTypeNumber]) return;

        int dropNumber = Random.Range(1, artifactDropRate + 1);
        if (artifactDropChances[enemyInitialStage] >= dropNumber) DropArtifact();
        //Debug.Log(artifactDropChances[enemyInitialStage] + " " + dropNumber);
    }
    public void DropArtifact()
    {
        heroPanel.GetComponent<HeroPanel>().CloseWindow();
        towerPanel.GetComponent<TowerPanel>().CloseWindow();
        removeObjectPanel.GetComponent<RemoveObjectPanel>().CloseWindow();
        towerMenu.GetComponent<PlaceTower>().CancelPlacingTower();
        cam.GetComponent<Zoom>().canMove = false;

        nameText.text = towerNames[towerTypeNumber];
        preview.sprite = icons[towerTypeNumber];
        description.text = descriptions[towerTypeNumber];

        if (heroPanel.GetComponent<PlaceHero>().banner)
        {
            Destroy(heroPanel.GetComponent<PlaceHero>().banner);
            cam.GetComponent<ClickOnObjects>().canClick = true;

        }

        GetComponent<Image>().enabled = true;
        content.SetActive(true);

        Time.timeScale = 0;
    }
    public void ClaimArtifact()
    {
        GetComponent<Image>().enabled = false;
        content.SetActive(false);

        cam.GetComponent<Zoom>().canMove = true;

        hasArtifact[towerTypeNumber] = true;

        Time.timeScale = speedUpButton.GetComponent<SpeedUpButton>().isNormalSpeed ? 1 : speedUpButton.GetComponent<SpeedUpButton>().speedUpValue;
    }
    public void ResetArtifacts()
    {
        for (int i = 0; i < hasArtifact.Length; i++)
        {
            hasArtifact[i] = false;
        }
    }
    public void SetDescriptions()
    {
        //float buffIncrease = towerMenu.GetComponent<SpawnTower>().towers[towerTypeNumber].GetComponent<ArtifactBuff>().buffIncrease;

        descriptions[0] = "When active, increases the damage by " + towerMenu.GetComponent<SpawnTower>().towers[0].GetComponent<ArtifactBuff>().buffIncrease + ".";
        descriptions[1] = "When active, increases the piercing by " + towerMenu.GetComponent<SpawnTower>().towers[1].GetComponent<ArtifactBuff>().buffIncrease + ".";
        descriptions[2] = "When active, increases the duration of all effects by " + towerMenu.GetComponent<SpawnTower>().towers[2].GetComponent<ArtifactBuff>().buffIncrease + "s.";
        descriptions[3] = "When active, increases the damage by " + towerMenu.GetComponent<SpawnTower>().towers[3].GetComponent<ArtifactBuff>().buffIncrease + ".";
        descriptions[4] = "When active, increases the bullet speed by " + towerMenu.GetComponent<SpawnTower>().towers[4].GetComponent<ArtifactBuff>().buffIncrease + ".";
        descriptions[5] = "When active, increases the bullet speed by " + towerMenu.GetComponent<SpawnTower>().towers[5].GetComponent<ArtifactBuff>().buffIncrease + ".";
        descriptions[6] = "When active, increases the range by " + towerMenu.GetComponent<SpawnTower>().towers[6].GetComponent<ArtifactBuff>().buffIncrease + ".";
        descriptions[7] = "When active, increases the range by " + towerMenu.GetComponent<SpawnTower>().towers[7].GetComponent<ArtifactBuff>().buffIncrease + ".";
        descriptions[8] = "When active, increases the attack speed by " + heroPanel.GetComponent<PlaceHero>().hero.GetComponent<ArtifactBuffHero>().buffIncrease * 100 + "%.";
    }
}
