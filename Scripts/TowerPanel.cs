using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour
{
    public TextMeshProUGUI towerType;
    public TextMeshProUGUI sellPrice;

    public GameObject towerSelected;

    public int currentSellPrice;
    public float sellPriceMultiplier = 0.8f;

    public Button[] upgradeButtons;
    public Button[] variationButtons;
    public Button[] specialButtons;

    public TextMeshProUGUI[] upgradeNameTexts;
    public TextMeshProUGUI[] variationNameTexts;
    public TextMeshProUGUI[] specialNameTexts;

    public Image[] upgradeIcons;
    public Image[] variationIcons;
    public Image[] specialIcons;

    public TextMeshProUGUI cost;

    public TextMeshProUGUI pointsLeft;

    public GameObject[] pages;
    public Button[] pageSelectors;

    public string[] towerTypes;
    public int selectedTowerNumber;

    public Image towerPreview;
    public Sprite[] towerPreviewsDefault;
    public Sprite[] towerPreviewsUpgrade;
    public Sprite[] towerPreviewsVariation;
    public Sprite[] towerPreviewsSpecial;

    public GameObject gameUI;

    public Color progressLocked;
    public Color progressUnlocked;

    public Color iconColorDefault;
    public Color iconColorDarkened;

    public GameObject achievements;

    public Sprite[] upgradeSprites;

    public GameObject sellTowerSound;
    public AudioSource buttonClickSound;

    public TextMeshProUGUI targetMethodSelectedText;
    public string[] targetMethods;
    public int targetMethodMax = 3;

    public Color colorNoArtifacttargetMethodButton;
    public Color colorNoArtifactPreviewFrame;
    public Color colorArtifactTargetMethodButton;
    public Color colorArtifactPreviewFrame;
    public Image previewFrame;
    public Image targetMethodButton;

    public GameObject hoverInfoPanel;

    public GameObject artifactInfoButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonInteractibilityAndIconColor();

        if (Input.GetKeyDown(KeyCode.Backspace)) SellTower();
    }

   

    public void OpenWindow(GameObject towerSelected)
    {
        StopAllCoroutines();

        //deactivate range visualizer of previously selected tower
        ChangeRangeVisualizerState(false);

        //Activate Panel
        gameObject.SetActive(true);

        //Assign new tower
        this.towerSelected = towerSelected;
        ChangeRangeVisualizerState(true);


        UpdateUpgradeProgressBars();
        UpdateVariationProgressBars();
        UpdateSpecialProgressBars();
        SelectPageBasedOnThingsBought();
        UpdateUpgradeAndVariationNames();
        SetupTowerInformation();
        UpdateTargetMethodSelectedText();
        UpdateArtifactUI();
    }
    public void UpdateArtifactUI()
    {
        bool hasArtifact = towerSelected.GetComponent<Stats>().hasArtifact;

        previewFrame.color = hasArtifact ? colorArtifactPreviewFrame : colorNoArtifactPreviewFrame;
        targetMethodButton.color = hasArtifact ? colorArtifactTargetMethodButton : colorNoArtifacttargetMethodButton;
        artifactInfoButton.SetActive(hasArtifact);
    }
    void SetupTowerInformation()
    {
        towerType.text = towerSelected.GetComponent<Stats>().towerType;

        int amountOfTowerUpgradesBought = 0;
        for (int i = 0; i < towerSelected.GetComponent<Enhancements>().upgradeLevels.Length; i++)
        {
            amountOfTowerUpgradesBought += towerSelected.GetComponent<Enhancements>().upgradeLevels[i];
        }

        if(towerSelected.GetComponent<Enhancements>().hasBoughtVariation) cost.text = towerSelected.GetComponent<Enhancements>().goldCosts[4].ToString();
        else if(amountOfTowerUpgradesBought == 3) cost.text = towerSelected.GetComponent<Enhancements>().goldCosts[3].ToString();
        else if (amountOfTowerUpgradesBought == 2) cost.text = towerSelected.GetComponent<Enhancements>().goldCosts[2].ToString();
        else if (amountOfTowerUpgradesBought == 1) cost.text = towerSelected.GetComponent<Enhancements>().goldCosts[1].ToString();
        else cost.text = towerSelected.GetComponent<Enhancements>().goldCosts[0].ToString();

        SetTowerPreview();
    }
	
    public void UpdateIcons()
    {
        for (int i = 0; i < upgradeIcons.Length; i++)
        {
            upgradeIcons[i].sprite = upgradeSprites[towerSelected.GetComponent<Enhancements>().upgradeOptions[i]];
        }
        for (int i = 0; i < variationIcons.Length; i++)
        {
            variationIcons[i].sprite = towerSelected.GetComponent<Enhancements>().variationSprites[i];
        }
        if(towerSelected.GetComponent<Enhancements>().variationBought < 3)
        {
            for (int i = 0; i < specialIcons.Length; i++)
            {
                specialIcons[i].sprite = towerSelected.GetComponent<Enhancements>().specialSprites[towerSelected.GetComponent<Enhancements>().variationBought * 2 + i];
            }
        }
        else
        {
            for (int i = 0; i < specialIcons.Length; i++)
            {
                specialIcons[i].sprite = null;
            }
        }
    }
    void UpdateUpgradeAndVariationNames()
    {
        if (towerSelected.GetComponent<Stats>().towerType == "Cannon")
            towerSelected.GetComponent<VariationEffectsCannon>().SetVariationInfoTexts();

        if (towerSelected.GetComponent<Stats>().towerType == "Crystal Tower")
            towerSelected.GetComponent<VariationEffectsCrystalTower>().SetVariationInfoTexts();

        if (towerSelected.GetComponent<Stats>().towerType == "Ice Dragon")
            towerSelected.GetComponent<VariationEffectsIceDragon>().SetVariationInfoTexts();

        if (towerSelected.GetComponent<Stats>().towerType == "Phoenix")
            towerSelected.GetComponent<VariationEffectsPhoenix>().SetVariationInfoTexts();

        if (towerSelected.GetComponent<Stats>().towerType == "Ship")
            towerSelected.GetComponent<VariationEffectsShip>().SetVariationInfoTexts();

        if (towerSelected.GetComponent<Stats>().towerType == "Magic Tower")
            towerSelected.GetComponent<VariationEffectsMagicTower>().SetVariationInfoTexts();

        if (towerSelected.GetComponent<Stats>().towerType == "Ballista")
            towerSelected.GetComponent<VariationEffectsBallista>().SetVariationInfoTexts();

        if (towerSelected.GetComponent<Stats>().towerType == "Cauldron")
            towerSelected.GetComponent<VariationEffectsCauldron>().SetVariationInfoTexts();

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeNameTexts[i].text = towerSelected.GetComponent<Enhancements>().upgradeOptionPossibilities[towerSelected.GetComponent<Enhancements>().upgradeOptions[i]];
            variationNameTexts[i].text = towerSelected.GetComponent<Enhancements>().variationNames[i];
        }
    }
    public void UpdateUpgradeProgressBars()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (towerSelected.GetComponent<Enhancements>().upgradeLevels[i] == 0)
            {
                upgradeButtons[i].transform.GetChild(0).GetComponent<Image>().color = progressLocked;
                upgradeButtons[i].transform.GetChild(1).GetComponent<Image>().color = progressLocked;
                upgradeButtons[i].interactable = true;
            }
            else if (towerSelected.GetComponent<Enhancements>().upgradeLevels[i] == 1)
            {
                upgradeButtons[i].transform.GetChild(0).GetComponent<Image>().color = progressUnlocked;
                upgradeButtons[i].transform.GetChild(1).GetComponent<Image>().color = progressLocked;
                upgradeButtons[i].interactable = true;
            }
            else
            {
                upgradeButtons[i].transform.GetChild(0).GetComponent<Image>().color = progressUnlocked;
                upgradeButtons[i].transform.GetChild(1).GetComponent<Image>().color = progressUnlocked;
                upgradeButtons[i].interactable = false;
            }
        }
    }
    public void UpdateVariationProgressBars()
    {
        for (int i = 0; i < variationButtons.Length; i++)
        {
            if (towerSelected.GetComponent<Enhancements>().variationBought != i)
            {
                variationButtons[i].transform.GetChild(0).GetComponent<Image>().color = progressLocked;
                variationButtons[i].interactable = true;
            }
            else
            {
                variationButtons[i].transform.GetChild(0).GetComponent<Image>().color = progressUnlocked;
                variationButtons[i].interactable = false;
            }
        }
    }
    public void UpdateSpecialProgressBars()
    {
        for (int i = 0; i < specialButtons.Length; i++)
        {
            if (towerSelected.GetComponent<Enhancements>().specialBought - (towerSelected.GetComponent<Enhancements>().variationBought * 2) != i)
            {
                specialButtons[i].transform.GetChild(0).GetComponent<Image>().color = progressLocked;
                specialButtons[i].interactable = true;
            }
            else
            {
                specialButtons[i].transform.GetChild(0).GetComponent<Image>().color = progressUnlocked;
                specialButtons[i].interactable = false;
            }
        }
    }
    public void SelectPageBasedOnThingsBought()
    {
        if (towerSelected.GetComponent<Enhancements>().upgradePointsLeft != 0)
        {
            SelectPage(0);
        }
        else if (!towerSelected.GetComponent<Enhancements>().hasBoughtVariation)
        {
            SelectPage(1);
        }
        else
        {
            SelectPage(2);
        }
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
        hoverInfoPanel.SetActive(false);
        ChangeRangeVisualizerState(false);
        if(towerSelected) if(towerSelected.GetComponent<Stats>().hasArtifact) artifactInfoButton.GetComponent<ArtifactInfo>().CloseWindow();
    }
    public void SellTower()
    {
        Instantiate(sellTowerSound, towerSelected.transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);

        Destroy(towerSelected);
        CloseWindow();
        gameUI.GetComponent<Gold>().ChangeGold(currentSellPrice);
    }
    public void BuyUpgrade(int number)
    {
        gameUI.GetComponent<Gold>().ChangeGold(-towerSelected.GetComponent<Enhancements>().goldCosts[3 - towerSelected.GetComponent<Enhancements>().upgradePointsLeft]);
        towerSelected.GetComponent<Stats>().totalTowerCost += towerSelected.GetComponent<Enhancements>().goldCosts[towerSelected.GetComponent<Enhancements>().upgradePointsMax - towerSelected.GetComponent<Enhancements>().upgradePointsLeft];

        towerSelected.GetComponent<Enhancements>().upgradeLevels[number]++;

        upgradeButtons[number].transform.GetChild(towerSelected.GetComponent<Enhancements>().upgradeLevels[number]-1).GetComponent<Image>().color = progressUnlocked;


        towerSelected.GetComponent<Enhancements>().upgradePointsLeft--;
        UpdatePointRemainingText();

        /*float num = towerSelected.GetComponent<Enhancements>().goldCosts[towerSelected.GetComponent<Enhancements>().upgradePointsMax - towerSelected.GetComponent<Enhancements>().upgradePointsLeft];

        cost.text = num.ToString();*/

        SelectPage(0);

        //upgraded to max
        if (towerSelected.GetComponent<Enhancements>().upgradePointsLeft == 0)
        {
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                upgradeButtons[i].interactable = false;
            }
            UpdateTowerSkins(1);
            towerSelected.GetComponent<Shoot>().currentActiveSkin = 1;
            towerSelected.GetComponent<Stats>().rangeDisplay.GetComponent<Range>().currentActiveSkin = 1;

            hoverInfoPanel.SetActive(false);

            StartCoroutine((UpgradeDelay(1)));
        }
        towerSelected.GetComponent<UpgradeEffects>().SetUpgradeEffect(number);

        achievements.GetComponent<Achievements>().SetNewProgress(0, number + 1);

        UpdateUpgradeAndVariationNames();

        buttonClickSound.Play();
    }
    public void UpdateTowerSkins(int activeTowerSkin)
    {
        for (int i = 0; i < towerSelected.GetComponent<TowerLooks>().looks.Length; i++)
        {
            towerSelected.GetComponent<TowerLooks>().looks[i].SetActive(activeTowerSkin == i);
        }
        towerSelected.GetComponent<Shoot>().cannonTops[activeTowerSkin].transform.rotation = towerSelected.GetComponent<Shoot>().cannonTops[activeTowerSkin - 1].transform.rotation;
    }
    IEnumerator UpgradeDelay(int number)
    {
        yield return new WaitForSeconds(1);
        SelectPage(number);
    }
    public void BuyVariation(int number)
    {
        gameUI.GetComponent<Gold>().ChangeGold(-towerSelected.GetComponent<Enhancements>().goldCosts[towerSelected.GetComponent<Enhancements>().upgradePointsMax - towerSelected.GetComponent<Enhancements>().upgradePointsLeft]);
        towerSelected.GetComponent<Stats>().totalTowerCost += towerSelected.GetComponent<Enhancements>().goldCosts[3];

        towerSelected.GetComponent<Enhancements>().variationBought = number;

        variationButtons[number].transform.GetChild(0).GetComponent<Image>().color = progressUnlocked;

        for (int i = 0; i < variationButtons.Length; i++)
        {
            variationButtons[i].interactable = false;
        }

        towerSelected.GetComponent<Enhancements>().hasBoughtVariation = true;
        SelectPage(1);
        StartCoroutine((UpgradeDelay(2)));

        SetVariationEffectForTower(number);

        UpdateTowerSkins(2);
        towerSelected.GetComponent<Shoot>().currentActiveSkin = 2;
        towerSelected.GetComponent<Stats>().rangeDisplay.GetComponent<Range>().currentActiveSkin = 2;

        hoverInfoPanel.SetActive(false);

        achievements.GetComponent<Achievements>().SetNewProgress(0, 4);

        UpdateUpgradeAndVariationNames();

        buttonClickSound.Play();
    }
    public void BuySpecial(int number)
    {
        gameUI.GetComponent<Gold>().ChangeGold(-towerSelected.GetComponent<Enhancements>().goldCosts[4]);
        towerSelected.GetComponent<Stats>().totalTowerCost += towerSelected.GetComponent<Enhancements>().goldCosts[4];

        towerSelected.GetComponent<Enhancements>().specialBought = towerSelected.GetComponent<Enhancements>().variationBought * 2 + number;

        specialButtons[number].transform.GetChild(0).GetComponent<Image>().color = progressUnlocked;

        for (int i = 0; i < specialButtons.Length; i++)
        {
            specialButtons[i].interactable = false;
        }

        towerSelected.GetComponent<Enhancements>().hasBoughtSpecial = true;

        SelectPage(2);

        SetSpecialEffectForTower(number);

        UpdateTowerSkins(3);
        towerSelected.GetComponent<Shoot>().currentActiveSkin = 3;
        towerSelected.GetComponent<Stats>().rangeDisplay.GetComponent<Range>().currentActiveSkin = 3;

        hoverInfoPanel.SetActive(false);

        achievements.GetComponent<Achievements>().SetNewProgress(0, 5);

        UpdateUpgradeAndVariationNames();

        buttonClickSound.Play();
    }
    public void SetVariationEffectForTower(int number)
    {
        if (towerSelected.GetComponent<Stats>().towerType == "Cannon")
            towerSelected.GetComponent<VariationEffectsCannon>().SetVariationEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Crystal Tower")
            towerSelected.GetComponent<VariationEffectsCrystalTower>().SetVariationEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Ice Dragon")
            towerSelected.GetComponent<VariationEffectsIceDragon>().SetVariationEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Phoenix")
            towerSelected.GetComponent<VariationEffectsPhoenix>().SetVariationEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Ship")
            towerSelected.GetComponent<VariationEffectsShip>().SetVariationEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Magic Tower")
            towerSelected.GetComponent<VariationEffectsMagicTower>().SetVariationEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Ballista")
            towerSelected.GetComponent<VariationEffectsBallista>().SetVariationEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Cauldron")
            towerSelected.GetComponent<VariationEffectsCauldron>().SetVariationEffect(number);
    }
    public void SetSpecialEffectForTower(int number)
    {
        if (towerSelected.GetComponent<Stats>().towerType == "Cannon")
            towerSelected.GetComponent<SpecialEffectsCannon>().SetSpecialEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Crystal Tower")
            towerSelected.GetComponent<SpecialEffectsCrystalTower>().SetSpecialEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Ice Dragon")
            towerSelected.GetComponent<SpecialEffectsIceDragon>().SetSpecialEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Phoenix")
            towerSelected.GetComponent<SpecialEffectsPhoenix>().SetSpecialEffect(number);
              
        if (towerSelected.GetComponent<Stats>().towerType == "Ship")
            towerSelected.GetComponent<SpecialEffectsShip>().SetSpecialEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Magic Tower")
            towerSelected.GetComponent<SpecialEffectsMagicTower>().SetSpecialEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Ballista")
            towerSelected.GetComponent<SpecialEffectsBallista>().SetSpecialEffect(number);

        if (towerSelected.GetComponent<Stats>().towerType == "Cauldron")
            towerSelected.GetComponent<SpecialEffectsCauldron>().SetSpecialEffect(number);
    }
    public void SelectPage(int number)
    {
        SetActivePage(number);
        UpdateSellPrice();
        UpdateCostText(number);
        ToggleCostVisibility(number);
        UpdateSpecialNameTexts();
        UpdateButtonInteractibilityAndIconColor();
        UpdatePointRemainingText();
        SetTowerPreview();
        UpdateIcons();
    }
    void SetActivePage(int number)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == number);
            pageSelectors[i].interactable = i != number;
        }
    }
    void UpdateSellPrice()
    {
        currentSellPrice = Mathf.RoundToInt(towerSelected.GetComponent<Stats>().totalTowerCost * sellPriceMultiplier);
        sellPrice.text = currentSellPrice.ToString();
    }
    void UpdateCostText(int number)
    {
        float num = 0;
        if (number == 0)
            num = towerSelected.GetComponent<Enhancements>().goldCosts[towerSelected.GetComponent<Enhancements>().upgradePointsMax - towerSelected.GetComponent<Enhancements>().upgradePointsLeft];
        else if (number == 1)
            num = towerSelected.GetComponent<Enhancements>().goldCosts[3];
        else
            num = towerSelected.GetComponent<Enhancements>().goldCosts[4];

        cost.text = num.ToString();
    }
    void ToggleCostVisibility(int number)
    {
        //Don't show the cost if the things on the respective page can't be bought
        bool showCost = !((number == 0 && towerSelected.GetComponent<Enhancements>().upgradePointsLeft == 0) ||
                          (number == 1 && towerSelected.GetComponent<Enhancements>().hasBoughtVariation) ||
                          (number == 2 && towerSelected.GetComponent<Enhancements>().hasBoughtSpecial));

        cost.gameObject.SetActive(showCost);
    }
    void UpdateSpecialNameTexts()
    {
        if (towerSelected.GetComponent<Enhancements>().hasBoughtVariation)
        {
            specialNameTexts[0].text = towerSelected.GetComponent<Enhancements>().specialNames[towerSelected.GetComponent<Enhancements>().variationBought * 2];
            specialNameTexts[1].text = towerSelected.GetComponent<Enhancements>().specialNames[towerSelected.GetComponent<Enhancements>().variationBought * 2 + 1];
        }
        else
        {
            for (int i = 0; i < specialNameTexts.Length; i++)
            {
                specialNameTexts[i].text = "?";
            }
        }
    }
    void UpdateButtonInteractibilityAndIconColor()
    {
        // Upgrade Buttons
        //Enough gold? enough upgrade points? not bought twice?
        bool canUpgrade = gameUI.GetComponent<Gold>().gold >= towerSelected.GetComponent<Enhancements>().goldCosts[towerSelected.GetComponent<Enhancements>().upgradePointsMax - towerSelected.GetComponent<Enhancements>().upgradePointsLeft] &&
                          towerSelected.GetComponent<Enhancements>().upgradePointsLeft > 0;

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            bool boughtTwice = towerSelected.GetComponent<Enhancements>().upgradeLevels[i] == 2;

            upgradeButtons[i].interactable = canUpgrade && !boughtTwice;
            upgradeIcons[i].color = canUpgrade && !boughtTwice ? iconColorDefault : iconColorDarkened;
        }

        // Variation Buttons
        //Enough gold? used upgrade points? hasn't bought variation already?
        bool canBuyVariation = gameUI.GetComponent<Gold>().gold >= towerSelected.GetComponent<Enhancements>().goldCosts[3] &&
                               towerSelected.GetComponent<Enhancements>().upgradePointsLeft == 0 &&
                               !towerSelected.GetComponent<Enhancements>().hasBoughtVariation;

        for (int i = 0; i < variationButtons.Length; i++)
        {
            variationButtons[i].interactable = canBuyVariation;
            variationIcons[i].color = canBuyVariation ? iconColorDefault : iconColorDarkened;
        }

        // Special Buttons
        //Enough gold? has bought variation? hasn't bought special?
        bool canBuySpecial = gameUI.GetComponent<Gold>().gold >= towerSelected.GetComponent<Enhancements>().goldCosts[4] &&
                             towerSelected.GetComponent<Enhancements>().hasBoughtVariation &&
                             !towerSelected.GetComponent<Enhancements>().hasBoughtSpecial;

        for (int i = 0; i < specialButtons.Length; i++)
        {
            specialButtons[i].interactable = canBuySpecial;
            specialIcons[i].color = canBuySpecial ? iconColorDefault : iconColorDarkened;
        }
    }
    public void ChangeRangeVisualizerState(bool toActive)
    {
        if (towerSelected)
        {
            towerSelected.GetComponent<InitializeMeshes>().rangeVisualizer.GetComponent<MeshRenderer>().enabled = toActive;
        }
    }
    public void SetTowerPreview()
    {

        //Check where the name of the selected tower and the sprite match
        for (int i = 0; i < towerTypes.Length; i++)
        {
            if (towerSelected.GetComponent<Stats>().towerType == towerTypes[i])
            {
                selectedTowerNumber = i;
            }
        }

        if(towerSelected.GetComponent<Enhancements>().hasBoughtSpecial)
            towerPreview.sprite = towerPreviewsSpecial[selectedTowerNumber];
        else if (towerSelected.GetComponent<Enhancements>().hasBoughtVariation)
            towerPreview.sprite = towerPreviewsVariation[selectedTowerNumber];
        else if (towerSelected.GetComponent<Enhancements>().upgradePointsLeft == 0)
            towerPreview.sprite = towerPreviewsUpgrade[selectedTowerNumber];
        else
            towerPreview.sprite = towerPreviewsDefault[selectedTowerNumber];
    }
    public void UpdatePointRemainingText()
    {
        pointsLeft.text = "upgrades remaining: " + towerSelected.GetComponent<Enhancements>().upgradePointsLeft.ToString();
    }
    public void UpdateTargetMethodSelectedText()
    {
        targetMethodSelectedText.text = targetMethods[towerSelected.GetComponent<Stats>().rangeDisplay.GetComponent<Range>().targetMethod];
    }
    public void ChangeTargetMethodSelected(bool toRight)
    {
        Debug.Log("d");

        var targetMethod = towerSelected.GetComponent<Stats>().rangeDisplay.GetComponent<Range>().targetMethod;

        if (toRight) targetMethod++;
        else targetMethod--;

        if (targetMethod < 0) targetMethod = targetMethodMax;
        else if (targetMethod > targetMethodMax) targetMethod = 0;

        towerSelected.GetComponent<Stats>().rangeDisplay.GetComponent<Range>().targetMethod = targetMethod;

        UpdateTargetMethodSelectedText();
    }
}
