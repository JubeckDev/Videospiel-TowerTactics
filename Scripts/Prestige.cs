using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prestige : MonoBehaviour
{
    public GameObject home;

    public AudioSource buttonClickSound;

    public int tokens;

    public GameObject[] prestigeButtons;
    public Button[] purchaseButtons;
    public int[] pointCosts;

    public AudioSource claimPrestigeSound;

    public GameObject waves;
    public GameObject artifactPanel;
    public GameObject maps;

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
    public void UpdatePurachseButtons()
    {
        for (int i = 0; i < purchaseButtons.Length; i++)
        {
            purchaseButtons[i].interactable = tokens >= pointCosts[i];
        }
    }
    public void Purchase(int buttonNumber)
    {
        tokens -= pointCosts[buttonNumber];

        prestigeButtons[buttonNumber].GetComponent<PrestigeButton>().purchaseUI.SetActive(false);
        prestigeButtons[buttonNumber].GetComponent<PrestigeButton>().boughtUI.SetActive(true);

        claimPrestigeSound.Play();

        if (buttonNumber == 0) GlobalConfusion();
        if (buttonNumber == 1) GlobalPoison();
        if (buttonNumber == 2) GlobalBurn();
        if (buttonNumber == 3) MapRewards();
        if (buttonNumber == 4) DifficultyRewards();
        if (buttonNumber == 5) ArtifactDropRate();
    }
    public void GlobalConfusion()
    {
        waves.GetComponent<GlobalEffects>().StartConfusion();
    }
    public void GlobalPoison()
    {
        waves.GetComponent<GlobalEffects>().StartPoison();
    }
    public void GlobalBurn()
    {
        waves.GetComponent<GlobalEffects>().StartBurn();
    }
    public void MapRewards()
    {
        for (int i = 0; i < maps.GetComponent<Maps>().mapsRewards.Length; i++)
        {
            maps.GetComponent<Maps>().mapsRewards[i] += 20;
        }
    }
    public void DifficultyRewards()
    {
        maps.GetComponent<Difficulty>().difficultyRewards[1] += 50;
        maps.GetComponent<Difficulty>().difficultyRewards[2] += 100;
    }
    public void ArtifactDropRate()
    {
        artifactPanel.GetComponent<ArtifactPanel>().artifactDropRate *= Mathf.RoundToInt(0.8f);
    }
}
