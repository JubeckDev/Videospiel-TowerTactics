using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinPanel : MonoBehaviour
{
    public GameObject maps;
    public GameObject homePanel;

    public TextMeshProUGUI completedText;
    public TextMeshProUGUI xpEarnedText;

    public string[] difficulties;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTexts()
    {
        UpdateCompletedText();
        UpdateXpEarnedText();
    }
    public void UpdateXpEarnedText()
    {
        float xpEarned = 0;
        xpEarned += maps.GetComponent<Maps>().mapsRewards[maps.GetComponent<Maps>().mapSelected];
        xpEarned += maps.GetComponent<Difficulty>().difficultyRewards[maps.GetComponent<Difficulty>().difficulty];
        homePanel.GetComponent<PlayerLevel>().EarnXP(xpEarned);
        xpEarnedText.text = "XP Earned: " + xpEarned;
    }
    public void UpdateCompletedText()
    {
        int currentMapNumber = maps.GetComponent<Maps>().mapSelected + 1;
        //completedText.text = "You have completed all waves on Map " + currentMapNumber + " on " + difficulties[maps.GetComponent<Difficulty>().difficulty] + ".";
        completedText.text = "You have completed all waves, congratulations!";
    }
}
