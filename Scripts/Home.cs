using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject home;

    public GameObject skillTree;
    public GameObject skillTreeContent;
    public GameObject maps;
    public GameObject achievements;
    public GameObject prestige;

    public GameObject options;

    public AudioSource buttonClickSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Options();
        }
    }
    public void Options()
    {
        if (options.activeSelf)
        {
            CloseOptions();
            return;
        }

        options.SetActive(true);

        buttonClickSound.Play();
    }
    public void CloseOptions()
    {
        options.SetActive(false);

        buttonClickSound.Play();
    }
    public void SkillTree()
    {
        skillTreeContent.SetActive(true);
        home.SetActive(false);

        skillTree.GetComponent<SkillTree>().UpdateUpgradePanelPurchaseObjects();
        skillTree.GetComponent<SkillTree>().LightUpClaimableSkillTreeButtons();

        buttonClickSound.Play();
    }
    public void Maps()
    {
        maps.SetActive(true);
        home.SetActive(false);

        buttonClickSound.Play();
    }
    public void Achievements()
    {
        achievements.SetActive(true);
        home.SetActive(false);

        achievements.GetComponent<Achievements>().UpdateNotifierVisibility();

        buttonClickSound.Play();
    }
    public void Prestige()
    {
        prestige.SetActive(true);
        home.SetActive(false);

        prestige.GetComponent<Prestige>().UpdatePurachseButtons();

        buttonClickSound.Play();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
