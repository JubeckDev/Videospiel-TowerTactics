using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public int level;
    public float xp;
    public float xpRemaining;
    public float xpRemainingMultiplier;

    public TextMeshProUGUI levelText;
    public Slider levelSlider;

    public Color levelSliderFull;
    public Color levelSliderNotFull;

    public AudioSource levelUp;

    public GameObject prestigeButton;

    public GameObject achievements;

    public GameObject artifactPanel;

    public GameObject prestigeConfirmationPanel;
    public TextMeshProUGUI tokenRewardText;

    public int[] tokenRewards;
    public GameObject prestigePanel;

    public TextMeshProUGUI tokenText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHeroLevelSlider();
        UpdateHeroLevelText();
        UpdateTokenText();

        for (int i = 0; i < 100; i++)
        {
            tokenRewards[i] = i + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EarnXP(float xpEarned)
    {
        xp += xpEarned;
        if (xp >= xpRemaining)
        {
            xp = xpRemaining;
            levelSlider.fillRect.GetComponent<Image>().color = levelSliderFull;
            prestigeButton.SetActive(true);
        }
        UpdateHeroLevelSlider();
    }
    public void Prestige()
    {
        prestigePanel.GetComponent<Prestige>().tokens += tokenRewards[level];
        UpdateTokenText();

        prestigeButton.SetActive(false);

        xp = 0;
        level++;
        levelUp.Play();

        xpRemaining *= xpRemainingMultiplier;

        UpdateHeroLevelSlider();
        UpdateHeroLevelText();

        prestigeConfirmationPanel.SetActive(false);

        artifactPanel.GetComponent<ArtifactPanel>().ResetArtifacts();

        achievements.GetComponent<Achievements>().SetNewProgress(16, achievements.GetComponent<Achievements>().progress[16] + 1);
        achievements.GetComponent<Achievements>().SetNewProgress(17, achievements.GetComponent<Achievements>().progress[17] + 1);
    }
    public void CloseWindow()
    {
        prestigeConfirmationPanel.SetActive(false);
    }
    public void UpdateHeroLevelText()
    {
        levelText.text = "Level: " + level;
    }
    public void UpdateHeroLevelSlider()
    {
        levelSlider.maxValue = xpRemaining;
        levelSlider.value = xp;
    }
    public void OpenPrestigeConfirmationPanel()
    {
        prestigeConfirmationPanel.SetActive(true);

        UpdateTokenRewardText();
    }
    public void UpdateTokenRewardText()
    {
        tokenRewardText.text = "+" + tokenRewards[level] + " Tokens";
    }
    public void UpdateTokenText()
    {
        tokenText.text = prestigePanel.GetComponent<Prestige>().tokens + " Tokens";
    }
}
