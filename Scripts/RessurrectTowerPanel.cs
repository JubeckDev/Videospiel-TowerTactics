using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RessurrectTowerPanel : MonoBehaviour
{
    public GameObject content;

    public TextMeshProUGUI ressurrectCostText;

    public GameObject currentTower;

    public float resurrectionCostFraction;

    public int currentRessurectionCost;

    public GameObject gameUI;

    public Button ressurrectButton;

    public GameObject towerMenu;
    public GameObject towerPanel;

    public GameObject towerSpawned;

    public Vector3 ressurrectTowerOffset;

    public GameObject ressurrectTowerSound;
    public GameObject ressurrectTowerEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (content.activeSelf)
        {
            ressurrectButton.interactable = gameUI.GetComponent<Gold>().gold >= currentRessurectionCost;
        }
    }
    public void OpenWindow(GameObject tower)
    {
        content.SetActive(true);

        currentTower = tower;

        currentRessurectionCost = Mathf.RoundToInt(currentTower.GetComponent<Stats>().totalTowerCost * resurrectionCostFraction);
        ressurrectCostText.text = currentRessurectionCost.ToString();

        currentTower.GetComponent<Stats>().rangeDisplay.GetComponent<MeshRenderer>().enabled = true;
        currentTower.GetComponent<Stats>().UpdateRange();
    }
    public void CloseWindow()
    {
        if (currentTower) currentTower.GetComponent<Stats>().rangeDisplay.GetComponent<MeshRenderer>().enabled = false;

        content.SetActive(false);
    }
    public void RessurectTower()
    {
        gameUI.GetComponent<Gold>().ChangeGold(-currentRessurectionCost);

        currentTower.SetActive(false);

        SpawnRessurrectedTower();

        Instantiate(ressurrectTowerSound, towerSpawned.transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        Instantiate(ressurrectTowerEffect, towerSpawned.transform.position, transform.rotation, GameObject.Find("Particles").transform);

        CloseWindow();
    }
    public void SpawnRessurrectedTower()
    {
        towerMenu.GetComponent<SpawnTower>().SpawnNewTower(System.Array.IndexOf(towerPanel.GetComponent<TowerPanel>().towerTypes, currentTower.GetComponent<Stats>().towerType), false);
        towerSpawned = towerMenu.GetComponent<PlaceTower>().tower;
        gameUI.GetComponent<Gold>().ChangeGold(int.Parse(towerMenu.GetComponent<PlaceTower>().goldCostTexts[towerMenu.GetComponent<PlaceTower>().towerSelectedNumber].text));
        towerMenu.GetComponent<PlaceTower>().PlaceCurrentTower();

        towerSpawned.transform.position = currentTower.transform.position + ressurrectTowerOffset;

        towerSpawned.GetComponent<TowerLooks>().looks[0].SetActive(false);

        if (currentTower.GetComponent<Enhancements>().hasBoughtSpecial)
        {
            towerSpawned.GetComponent<TowerLooks>().looks[3].SetActive(true);
            towerSpawned.GetComponent<Shoot>().currentActiveSkin = 3;
        }
        else if (currentTower.GetComponent<Enhancements>().hasBoughtVariation)
        {
            towerSpawned.GetComponent<TowerLooks>().looks[2].SetActive(true);
            towerSpawned.GetComponent<Shoot>().currentActiveSkin = 2;
        }
        else if (currentTower.GetComponent<Enhancements>().upgradePointsLeft == 0)
        {
            towerSpawned.GetComponent<TowerLooks>().looks[1].SetActive(true);
            towerSpawned.GetComponent<Shoot>().currentActiveSkin = 1;
        }
        else
        {
            towerSpawned.GetComponent<TowerLooks>().looks[0].SetActive(true);
            towerSpawned.GetComponent<Shoot>().currentActiveSkin = 0;
        }

        towerSpawned.GetComponent<Stats>().rangeDisplay.GetComponent<MeshRenderer>().enabled = false;

        /*towerPanel.GetComponent<TowerPanel>().OpenWindow(towerSpawned);
        for (int i = 0; i < currentTower.GetComponent<Enhancements>().upgradeLevels[i]; i++)
        {
            for (int j = 0; j < currentTower.GetComponent<Enhancements>().upgradeLevels[j]; j++)
            {
                towerPanel.GetComponent<TowerPanel>().BuyUpgrade(j);
            }
        }*/
        towerSpawned.GetComponent<Enhancements>().upgradePointsMax = currentTower.GetComponent<Enhancements>().upgradePointsLeft;
        for (int i = 0; i < towerSpawned.GetComponent<Enhancements>().upgradeLevels.Length; i++)
        {
            towerSpawned.GetComponent<Enhancements>().upgradeLevels[i] = currentTower.GetComponent<Enhancements>().upgradeLevels[i];
            for (int j = 0; j < currentTower.GetComponent<Enhancements>().upgradeLevels[i]; j++)
            {
                towerSpawned.GetComponent<UpgradeEffects>().SetUpgradeEffect(i);
            }
        }
        towerSpawned.GetComponent<Enhancements>().variationBought = currentTower.GetComponent<Enhancements>().variationBought;
        towerSpawned.GetComponent<Enhancements>().specialBought = currentTower.GetComponent<Enhancements>().specialBought;
        towerSpawned.GetComponent<Enhancements>().hasBoughtVariation = currentTower.GetComponent<Enhancements>().hasBoughtVariation;
        towerSpawned.GetComponent<Enhancements>().hasBoughtSpecial = currentTower.GetComponent<Enhancements>().hasBoughtSpecial;
        if (towerSpawned.GetComponent<Enhancements>().hasBoughtVariation)
        {
            if (towerSpawned.GetComponent<Stats>().towerType == "Cannon")
                towerSpawned.GetComponent<VariationEffectsCannon>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Crystal Tower")
                towerSpawned.GetComponent<VariationEffectsCrystalTower>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Ice Dragon")
                towerSpawned.GetComponent<VariationEffectsIceDragon>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Phoenix")
                towerSpawned.GetComponent<VariationEffectsPhoenix>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Ship")
                towerSpawned.GetComponent<VariationEffectsShip>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Magic Tower")
                towerSpawned.GetComponent<VariationEffectsMagicTower>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Ballista")
                towerSpawned.GetComponent<VariationEffectsBallista>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Cauldron")
                towerSpawned.GetComponent<VariationEffectsCauldron>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);
        }
        if (towerSpawned.GetComponent<Enhancements>().hasBoughtSpecial)
        {
            if (towerSpawned.GetComponent<Stats>().towerType == "Cannon")
                towerSpawned.GetComponent<VariationEffectsCannon>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Crystal Tower")
                towerSpawned.GetComponent<VariationEffectsCrystalTower>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Ice Dragon")
                towerSpawned.GetComponent<VariationEffectsIceDragon>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Phoenix")
                towerSpawned.GetComponent<VariationEffectsPhoenix>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Ship")
                towerSpawned.GetComponent<VariationEffectsShip>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Magic Tower")
                towerSpawned.GetComponent<VariationEffectsMagicTower>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Ballista")
                towerSpawned.GetComponent<VariationEffectsBallista>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);

            if (towerSpawned.GetComponent<Stats>().towerType == "Cauldron")
                towerSpawned.GetComponent<VariationEffectsCauldron>().SetVariationEffect(towerSpawned.GetComponent<Enhancements>().variationBought);
        }

        towerSpawned.GetComponent<Stats>().rangeDisplay.GetComponent<Range>().targetMethod = currentTower.GetComponent<Stats>().rangeDisplay.GetComponent<Range>().targetMethod;

        towerSpawned.GetComponent<Stats>().totalTowerCost = currentRessurectionCost;
    }
}
