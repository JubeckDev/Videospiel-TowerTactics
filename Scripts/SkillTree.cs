using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    public GameObject skillTree;
    public GameObject home;

    public GameObject upgradePanel;

    public Vector3 upgradePanelOffset;

    public GameObject currentSkillTreeButton;

    public GameObject achievements;

    public TextMeshProUGUI pointsText;

    public bool[] achievementUnlocked;
    public float[] achievementIncreases;

    public GameObject gameUI;

    public TextMeshProUGUI cannonPriceText;
    public TextMeshProUGUI iceDragonPriceText;

    public GameObject towerPanel;

    public GameObject waves;

    public AudioSource buttonClickSound;
    public AudioSource claimAchievementSound;

    public GameObject notifierSkillTree;

    public Button[] skillTreeButtons;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePointTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSkillTreeButton != null && Input.GetKeyDown(KeyCode.Return))
        {
            if(achievements.GetComponent<Achievements>().points >= currentSkillTreeButton.GetComponent<SkillTreeButton>().pointCost)
            {
                Purchase();
            }
        }
    }
    public void BackToHome()
    {
        home.SetActive(true);
        upgradePanel.SetActive(false);
        currentSkillTreeButton.GetComponent<SkillTreeButton>().selectionFrame.SetActive(false);
        skillTree.SetActive(false);
        buttonClickSound.Play();
    }
    public void LightUpClaimableSkillTreeButtons()
    {
        for (int i = 0; i < skillTreeButtons.Length; i++)
        {
            if(skillTreeButtons[i].interactable && skillTreeButtons[i].GetComponent<SkillTreeButton>().icon.GetComponent<Image>().color != Color.white)
            {
                skillTreeButtons[i].GetComponent<SkillTreeButton>().halo.SetActive(achievements.GetComponent<Achievements>().points >= skillTreeButtons[i].GetComponent<SkillTreeButton>().pointCost);
            }
        }
    }
    public void OpenUpgradePanel(GameObject skillTreeButton)
    {
        upgradePanel.SetActive(true);
        upgradePanel.GetComponent<UpgradePanel>().description.text = skillTreeButton.GetComponent<SkillTreeButton>().description;
        upgradePanel.GetComponent<UpgradePanel>().pointCostText.text = skillTreeButton.GetComponent<SkillTreeButton>().pointCost.ToString() + "PT";
        upgradePanel.GetComponent<UpgradePanel>().title.text = skillTreeButton.GetComponent<SkillTreeButton>().title.text;
        upgradePanel.GetComponent<UpgradePanel>().icon.sprite = skillTreeButton.GetComponent<SkillTreeButton>().icon.sprite;
        currentSkillTreeButton.GetComponent<SkillTreeButton>().selectionFrame.SetActive(false);
        skillTreeButton.GetComponent<SkillTreeButton>().selectionFrame.SetActive(true);
        currentSkillTreeButton = skillTreeButton;

        UpdateUpgradePanelPurchaseObjects();

        buttonClickSound.Play();
    }

    public void UpdateUpgradePanelPurchaseObjects()
    {
        upgradePanel.GetComponent<UpgradePanel>().purchaseButton.interactable = achievements.GetComponent<Achievements>().points >= currentSkillTreeButton.GetComponent<SkillTreeButton>().pointCost;

        upgradePanel.GetComponent<UpgradePanel>().purchaseButton.gameObject.SetActive(!currentSkillTreeButton.GetComponent<SkillTreeButton>().hasBeenBought);
        upgradePanel.GetComponent<UpgradePanel>().pointCostText.gameObject.SetActive(!currentSkillTreeButton.GetComponent<SkillTreeButton>().hasBeenBought);
    }


    public void Purchase()
    {
        currentSkillTreeButton.GetComponent<SkillTreeButton>().hasBeenBought = true;
        currentSkillTreeButton.GetComponent<SkillTreeButton>().icon.color = Color.white;
        ChangePoints(-currentSkillTreeButton.GetComponent<SkillTreeButton>().pointCost);
        UpdateUpgradePanelPurchaseObjects();
        if (currentSkillTreeButton.GetComponent<SkillTreeButton>().nextButtons.Length > 0)
        {
            for (int i = 0; i < currentSkillTreeButton.GetComponent<SkillTreeButton>().nextButtons.Length; i++)
            {
                currentSkillTreeButton.GetComponent<SkillTreeButton>().nextButtons[i].GetComponent<SkillTreeButton>().lockObject.SetActive(false);
                currentSkillTreeButton.GetComponent<SkillTreeButton>().nextButtons[i].GetComponent<Button>().interactable = true;
            }
        }

        achievementUnlocked[currentSkillTreeButton.GetComponent<SkillTreeButton>().upgradeSelected] = true;


        if (currentSkillTreeButton.GetComponent<SkillTreeButton>().upgradeSelected == 25) CannonPrice();
        if (currentSkillTreeButton.GetComponent<SkillTreeButton>().upgradeSelected == 27) WaveGoldReward();
        if (currentSkillTreeButton.GetComponent<SkillTreeButton>().upgradeSelected == 28) Salesman();
        if (currentSkillTreeButton.GetComponent<SkillTreeButton>().upgradeSelected == 30) IceDragonPrice();

        if (achievements.GetComponent<Achievements>().points <= 0) notifierSkillTree.SetActive(false);

        currentSkillTreeButton.GetComponent<SkillTreeButton>().halo.SetActive(false);
        LightUpClaimableSkillTreeButtons();

        claimAchievementSound.Play();
    }

    public void ChangePoints(int pointChange)
    {
        achievements.GetComponent<Achievements>().points += pointChange;
        UpdatePointTexts();
    }
    public void UpdatePointTexts()
    {
        pointsText.text = achievements.GetComponent<Achievements>().points.ToString() + "PT";
        achievements.GetComponent<Achievements>().UpdatePointsText();
    }

    //Economy
    public void CannonPrice()
    {
        Debug.Log("CannonPrice");
        int cannonPrice = int.Parse(cannonPriceText.text);
        cannonPrice += Mathf.RoundToInt(achievementIncreases[25]);
        cannonPriceText.text = cannonPrice.ToString();
    }
    public void WaveGoldReward()
    {
        for (int i = 0; i < waves.GetComponent<Waves>().waveGoldRewards.Length; i++)
        {
            waves.GetComponent<Waves>().waveGoldRewards[i] += Mathf.RoundToInt(achievementIncreases[27]);
        }
    }
    public void Salesman()
    {
        towerPanel.GetComponent<TowerPanel>().sellPriceMultiplier += achievementIncreases[29];
    }
    public void IceDragonPrice()
    {
        Debug.Log("IceDragonPrice");
        int iceDragonPrice = int.Parse(iceDragonPriceText.text);
        iceDragonPrice += Mathf.RoundToInt(achievementIncreases[30]);
        iceDragonPriceText.text = iceDragonPrice.ToString();
    }
}
