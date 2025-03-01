using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievements : MonoBehaviour
{
    public GameObject home;

    public int[] pointBonus;
    public int points;
    public Button[] achievementButton;

    public float buttonFadeAlpha = 0.6f;

    public float[] progress;
    public float[] progressRequired;
    public bool[] achievementsClaimed;

    public TextMeshProUGUI pointsText;

    public Color fillCompletedColor;
    public Color achievementClaimedButtonColor;

    public AudioSource buttonClickSound;
    public AudioSource claimAchievementSound;

    public GameObject notifierAchievements;
    public GameObject notifierSkillTree;
    public int claimableAchievements;

    public GameObject skillTree;

    public GameObject achievementPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BackToHome()
    {
        home.SetActive(true);
        gameObject.SetActive(false);

        buttonClickSound.Play();
    }
    public void SetNewProgress(int achievementNumber, float newProgress)
    {
        if(newProgress > progress[achievementNumber] && !achievementsClaimed[achievementNumber])
        {
            progress[achievementNumber] = newProgress;
            float progressPortion = progress[achievementNumber] / progressRequired[achievementNumber];
            achievementButton[achievementNumber].gameObject.GetComponent<AchievementButton>().progressBar.transform.localScale = progressPortion < 1 ? new Vector3(1, progressPortion, 1) : new Vector3(1, 1, 1);
            if (progressPortion >= 1)//Achievement completed
            {
                achievementsClaimed[achievementNumber] = true;
                achievementButton[achievementNumber].interactable = true;
                achievementButton[achievementNumber].gameObject.GetComponent<AchievementButton>().progressBarFill.color = fillCompletedColor;
                notifierAchievements.SetActive(true);
                claimableAchievements++;
            }
        }
    }
    public void ClaimAchievement(int achievementNumber)
    {
        points += pointBonus[achievementNumber];
        skillTree.GetComponent<SkillTree>().UpdatePointTexts();

        achievementButton[achievementNumber].interactable = false;
        achievementButton[achievementNumber].GetComponent<Image>().color = achievementClaimedButtonColor;

        claimableAchievements--;
        if (claimableAchievements <= 0) notifierAchievements.SetActive(false);
        notifierSkillTree.SetActive(true);

        claimAchievementSound.Play();
    }
    public void UpdatePointsText()
    {
        pointsText.text = points + "PT";
    }
    public void UpdateNotifierVisibility()
    {
        bool[] achievementClaimable = new bool[] {false, false, false};
        for (int i = 0; i < achievementButton.Length; i++)
        {
            //Debug.Log(i / 6);
            if (achievementButton[i].interactable) achievementClaimable[i / 6] = true;
        }
        //set visibility of previous button halo
        if (achievementPanel.GetComponent<AchievementPageSelector>().currentPage > 0)
        {
            if (achievementClaimable[achievementPanel.GetComponent<AchievementPageSelector>().currentPage - 1])
            {
                achievementPanel.GetComponent<AchievementPageSelector>().previousButton.GetComponent<Image>().enabled = true;
                //Debug.Log(achievementClaimable[0] + " " + achievementClaimable[1] + " " + achievementClaimable[2] + achievementPanel.GetComponent<AchievementPageSelector>().currentPage);
            }
            else
            {
                achievementPanel.GetComponent<AchievementPageSelector>().previousButton.GetComponent<Image>().enabled = false;
            }
        }
        else
        {
            achievementPanel.GetComponent<AchievementPageSelector>().previousButton.GetComponent<Image>().enabled = false;
        }
        //set visibility of next button halo
        if (achievementPanel.GetComponent<AchievementPageSelector>().currentPage < achievementPanel.GetComponent<AchievementPageSelector>().pages.Length - 1)
        {
            if (achievementClaimable[achievementPanel.GetComponent<AchievementPageSelector>().currentPage + 1])
            {
                achievementPanel.GetComponent<AchievementPageSelector>().nextButton.GetComponent<Image>().enabled = true;
            }
            else
            {
                achievementPanel.GetComponent<AchievementPageSelector>().nextButton.GetComponent<Image>().enabled = false;
            }
        }
        else
        {
            achievementPanel.GetComponent<AchievementPageSelector>().nextButton.GetComponent<Image>().enabled = false;
        }
    }
}
