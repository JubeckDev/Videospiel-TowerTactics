using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverInfoPanel;
    public TextMeshProUGUI hoverInfoPanelText;
    public GameObject towerPanel;

    public bool isUpgradeButton;
    public bool isVariationButton;
    public bool isSpecialButton;

    public int buttonNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject towerSelected = towerPanel.GetComponent<TowerPanel>().towerSelected;
        int selectedTowerNumber = towerPanel.GetComponent<TowerPanel>().selectedTowerNumber;

        if (isSpecialButton && towerSelected.GetComponent<Enhancements>().variationBought > 2) return;

        hoverInfoPanel.SetActive(true);
        hoverInfoPanel.transform.position = new Vector3(transform.position.x, hoverInfoPanel.transform.position.y, hoverInfoPanel.transform.position.z);

        if (isUpgradeButton)
        {
            if (towerSelected.GetComponent<Enhancements>().upgradeOptions[buttonNumber] == 0)
                hoverInfoPanelText.text = "Damage: +" + towerSelected.GetComponent<Enhancements>().statIncreases[buttonNumber];
            if (towerSelected.GetComponent<Enhancements>().upgradeOptions[buttonNumber] == 1)
                hoverInfoPanelText.text = "Range: +" + towerSelected.GetComponent<Enhancements>().statIncreases[buttonNumber];
            if (towerSelected.GetComponent<Enhancements>().upgradeOptions[buttonNumber] == 2)
                hoverInfoPanelText.text = "Attack Speed: " + towerSelected.GetComponent<Enhancements>().statIncreases[buttonNumber] + "s";
            if (towerSelected.GetComponent<Enhancements>().upgradeOptions[buttonNumber] == 3)
                hoverInfoPanelText.text = "Piercing: +" + towerSelected.GetComponent<Enhancements>().statIncreases[buttonNumber];
            if (towerSelected.GetComponent<Enhancements>().upgradeOptions[buttonNumber] == 4)
                hoverInfoPanelText.text = "Bullet Speed: +" + towerSelected.GetComponent<Enhancements>().statIncreases[buttonNumber];
        }
        else if (isVariationButton)
        {
            if (selectedTowerNumber == 0)
                hoverInfoPanelText.text = towerSelected.GetComponent<VariationEffectsCannon>().variationInfoTexts[buttonNumber];
            if (selectedTowerNumber == 1)
                hoverInfoPanelText.text = towerSelected.GetComponent<VariationEffectsCrystalTower>().variationInfoTexts[buttonNumber];
            if (selectedTowerNumber == 2)
                hoverInfoPanelText.text = towerSelected.GetComponent<VariationEffectsCauldron>().variationInfoTexts[buttonNumber];
            if (selectedTowerNumber == 3)
                hoverInfoPanelText.text = towerSelected.GetComponent<VariationEffectsBallista>().variationInfoTexts[buttonNumber];
            if (selectedTowerNumber == 4)
                hoverInfoPanelText.text = towerSelected.GetComponent<VariationEffectsShip>().variationInfoTexts[buttonNumber];
            if (selectedTowerNumber == 5)
                hoverInfoPanelText.text = towerSelected.GetComponent<VariationEffectsMagicTower>().variationInfoTexts[buttonNumber];
            if (selectedTowerNumber == 6)
                hoverInfoPanelText.text = towerSelected.GetComponent<VariationEffectsIceDragon>().variationInfoTexts[buttonNumber];
            if (selectedTowerNumber == 7)
                hoverInfoPanelText.text = towerSelected.GetComponent<VariationEffectsPhoenix>().variationInfoTexts[buttonNumber];
        }
        else
        {
            int textNumber = buttonNumber + towerSelected.GetComponent<Enhancements>().variationBought * 2;

            if (selectedTowerNumber == 0)
                hoverInfoPanelText.text = towerSelected.GetComponent<SpecialEffectsCannon>().specialInfoTexts[textNumber];
            if (selectedTowerNumber == 1)
                hoverInfoPanelText.text = towerSelected.GetComponent<SpecialEffectsCrystalTower>().specialInfoTexts[textNumber];
            if (selectedTowerNumber == 2)
                hoverInfoPanelText.text = towerSelected.GetComponent<SpecialEffectsCauldron>().specialInfoTexts[textNumber];
            if (selectedTowerNumber == 3)
                hoverInfoPanelText.text = towerSelected.GetComponent<SpecialEffectsBallista>().specialInfoTexts[textNumber];
            if (selectedTowerNumber == 4)
                hoverInfoPanelText.text = towerSelected.GetComponent<SpecialEffectsShip>().specialInfoTexts[textNumber];
            if (selectedTowerNumber == 5)
                hoverInfoPanelText.text = towerSelected.GetComponent<SpecialEffectsMagicTower>().specialInfoTexts[textNumber];
            if (selectedTowerNumber == 6)
                hoverInfoPanelText.text = towerSelected.GetComponent<SpecialEffectsIceDragon>().specialInfoTexts[textNumber];
            if (selectedTowerNumber == 7)
                hoverInfoPanelText.text = towerSelected.GetComponent<SpecialEffectsPhoenix>().specialInfoTexts[textNumber];
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverInfoPanel.SetActive(false);
    }
}
