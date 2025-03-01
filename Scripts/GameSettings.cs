using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public GameObject settingsPanel;

    public GameObject gameUI;
    public GameObject homePanel;

    public GameObject waves;

    public GameObject tutorial;

    public Toggle toggleAutoWave;
    public bool autoWaveIsOn;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject infoPanel;
    public GameObject heroPanel;
    public GameObject removeObjectPanel;
    public GameObject ressurrectTowerPanel;

    public GameObject music;
    public AudioSource buttonClickSound;

    public Button[] mapButtons;
    public GameObject[] medals;
    public GameObject maps;

    public GameObject hero;

    public GameObject environment;

    public GameObject speedUpButton;

    public GameObject abilityPanel;

    public GameObject achievements;

    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf)
            {
                CloseWindow();
            }
            else
            {
                OpenWindow();
            }
        }
    }
    public void OpenWindow()
    {
        if (settingsPanel.activeSelf)
        {
            CloseWindow();
            return;
        }

        settingsPanel.SetActive(true);
        buttonClickSound.Play();
    }
    public void CloseWindow()
    {
        settingsPanel.SetActive(false);
        buttonClickSound.Play();
    }
    public void BackToMenu(bool hasWon)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);

        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in towers)
            Destroy(tower);

        GameObject[] abilityButtons = GameObject.FindGameObjectsWithTag("AbilityButton");
        foreach (GameObject abilityButton in abilityButtons)
            Destroy(abilityButton);

        waves.GetComponent<Waves>().StopWave();

        winPanel.GetComponent<GameEndPanel>().ClosePanel();
        losePanel.GetComponent<GameEndPanel>().ClosePanel();
        infoPanel.GetComponent<InfoPanel>().CloseWindow();
        ressurrectTowerPanel.GetComponent<RessurrectTowerPanel>().CloseWindow();
        //Destroy(heroPanel.GetComponent<SpawnHeroBanner>().bannerSpawned);
        Destroy(heroPanel.GetComponent<PlaceHero>().banner);
        cam.GetComponent<ClickOnObjects>().canClick = true;
        heroPanel.GetComponent<HeroPanel>().CloseWindow();
        removeObjectPanel.GetComponent<RemoveObjectPanel>().CloseWindow();

        homePanel.SetActive(true);
        gameUI.SetActive(false);
        CloseWindow();

        if (hasWon)
        {
            if(mapButtons.Length > maps.GetComponent<Maps>().mapSelected + 1)
            {
                mapButtons[maps.GetComponent<Maps>().mapSelected + 1].interactable = true;
            }
            medals[maps.GetComponent<Maps>().mapSelected].transform.GetChild(maps.GetComponent<Difficulty>().difficulty).gameObject.SetActive(true);


            if(maps.GetComponent<Difficulty>().difficulty == 0)achievements.GetComponent<Achievements>().SetNewProgress(12, achievements.GetComponent<Achievements>().progress[12] + 1);
            if (maps.GetComponent<Difficulty>().difficulty == 1) achievements.GetComponent<Achievements>().SetNewProgress(13, achievements.GetComponent<Achievements>().progress[13] + 1);
            if (maps.GetComponent<Difficulty>().difficulty == 2) achievements.GetComponent<Achievements>().SetNewProgress(14, achievements.GetComponent<Achievements>().progress[14] + 1);

            maps.GetComponent<Maps>().mapsBeaten[maps.GetComponent<Maps>().mapSelected] = true;

            int mapsBeaten = 0;
            for (int i = 0; i < maps.GetComponent<Maps>().mapsBeaten.Length; i++)
            {
                if (maps.GetComponent<Maps>().mapsBeaten[i]) mapsBeaten++;
            }
            achievements.GetComponent<Achievements>().SetNewProgress(15, mapsBeaten);
        }

        waves.GetComponent<Waves>().waveCanEnd = false;

        hero.GetComponent<HeroMovement>().ResetSwordCoroutine();
        hero.SetActive(false);
        environment.SetActive(false);

        heroPanel.GetComponent<HeroPanel>().CloseWindow();

        if (!speedUpButton.GetComponent<SpeedUpButton>().isNormalSpeed) speedUpButton.GetComponent<SpeedUpButton>().SpeedUp();

        Time.timeScale = 1;

        abilityPanel.GetComponent<AbilityPanel>().abilityButtons = new List<GameObject>();

        music.GetComponent<MusicPlayer>().SwitchToMenuMusic();
    }
    public void OpenTutorial()
    {
        tutorial.SetActive(true);
        CloseWindow();
        buttonClickSound.Play();
    }
    public void ToggleAutoWave()
    {
        autoWaveIsOn = toggleAutoWave.isOn;
        buttonClickSound.Play();
    }
}