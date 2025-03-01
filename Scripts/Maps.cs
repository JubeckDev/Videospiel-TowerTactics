using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Maps : MonoBehaviour
{
    public GameObject gameUI;

    public GameObject home;

    public float[] camPosX;
    public float[] camPosZ;

    public float[] camMinX;
    public float[] camMaxX;
    public float[] camMinZ;
    public float[] camMaxZ;

    public float rangeX = 35;
    public float rangeZ = 40;

    public GameObject gameCam;

    public GameObject towerPanel;
    public GameObject waves;

    public int mapSelected;

    public GameObject music;

    public AudioSource buttonClickSound;
    public AudioSource selectLevel;

    public GameObject[] removableObjects;
    public GameObject[] ressurrectableTowers;

    public GameObject heroPanel;
    public GameObject heroUpgrades;
    public Transform[] bannerSpawnPoints;

    public bool[] isNightTime;

    public Material skyboxDay;
    public Material skyboxNight;
    public bool[] shouldRotateCam;
    public bool camHasbeenRotated;
    public float sunIntensityDay;
    public float sunIntensityNight;
    public float environmentLightingLevelDay;
    public float environmentLightingLevelNight;

    public GameObject environment;

    public float[] mapsRewards;

    public bool[] mapsBeaten;

    public Light sun;

    public TextMeshProUGUI[] rewardInfoTexts;

    // Start is called before the first frame update
    void Start()
    {
        //Calculate the area the camera can move in for each position/map
        for (int i = 0; i < camPosX.Length; i++)
        {
            camMinX[i] = camPosX[i] - rangeX / 2;
            camMaxX[i] = camPosX[i] + rangeX / 2;

            camMinZ[i] = camPosZ[i] - rangeZ / 2;
            camMaxZ[i] = camPosZ[i] + rangeZ / 2;
        }

        UpdateRewardInfoTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectMap(int number)
    {
        home.SetActive(false);
        towerPanel.SetActive(false);
        gameUI.SetActive(true);

        gameCam.transform.position = new Vector3(camPosX[number], gameCam.GetComponent<Zoom>().maxHeight, camPosZ[number]);
        //gameCam.transform.eulerAngles = new Vector3(gameCam.GetComponent<Zoom>().maxRotation, gameCam.transform.rotation.y, gameCam.transform.rotation.z);

        //Set range of cam
        gameCam.GetComponent<Zoom>().minX = camMinX[number];
        gameCam.GetComponent<Zoom>().maxX = camMaxX[number];

        gameCam.GetComponent<Zoom>().minZ = camMinZ[number];
        gameCam.GetComponent<Zoom>().maxZ = camMaxZ[number];

        gameUI.GetComponent<Gold>().SetGold();
        gameUI.GetComponent<Lives>().SetLives();

        //Assign path to waves
        waves.GetComponent<Waves>().currentWaypointParent = waves.GetComponent<Waves>().waypointParents[number];
        waves.GetComponent<Waves>().waveCount = 1;
        waves.GetComponent<Waves>().SetWaveGoal();
        waves.GetComponent<Waves>().SetWaveCountText();
        mapSelected = number;

        music.GetComponent<MusicPlayer>().SwitchToGameMusic();
        selectLevel.Play();

        for (int i = 0; i < removableObjects.Length; i++)
        {
            removableObjects[i].SetActive(true);
        }
        for (int i = 0; i < ressurrectableTowers.Length; i++)
        {
            if(ressurrectableTowers[i] != null) ressurrectableTowers[i].SetActive(true);
        }

        if (heroPanel.GetComponent<PlaceHero>().bannerPlaced) heroPanel.GetComponent<PlaceHero>().bannerPlaced.transform.position = bannerSpawnPoints[number].position;
        environment.SetActive(true);
        heroPanel.GetComponent<PlaceHero>().hero.SetActive(true);
        heroPanel.GetComponent<PlaceHero>().hero.GetComponent<StatsHero>().ResetHeroStats();
        heroUpgrades.GetComponent<HeroUpgrades>().ResetHeroUpgrades();

        heroPanel.GetComponent<PlaceHero>().hero.GetComponent<NavMeshAgent>().enabled = false;
        if(heroPanel.GetComponent<PlaceHero>().bannerPlaced) heroPanel.GetComponent<SpawnHeroBanner>().SetHeroPosition();
        heroPanel.GetComponent<PlaceHero>().hero.GetComponent<NavMeshAgent>().enabled = true;

        //mirror cam if necessary
        if (shouldRotateCam[number] != camHasbeenRotated)
        {
            camHasbeenRotated = !camHasbeenRotated;
            gameCam.transform.Rotate(0, 180, 0);
            gameCam.GetComponent<Zoom>().movementSpeed *= -1;
        }
        //set daytime or night time
        if (isNightTime[number])
        {
            RenderSettings.skybox = skyboxNight;
            sun.intensity = sunIntensityNight;
            RenderSettings.ambientIntensity = environmentLightingLevelNight;
        }
        else
        {
            RenderSettings.skybox = skyboxDay;
            sun.intensity = sunIntensityDay;
            RenderSettings.ambientIntensity = environmentLightingLevelDay;
        }

        for (int i = 0; i < rewardInfoTexts.Length; i++)
        {
            rewardInfoTexts[i].transform.parent.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
    public void UpdateRewardInfoTexts()
    {
        for (int i = 0; i < rewardInfoTexts.Length; i++)
        {

            float xpReward = 0;
            xpReward += mapsRewards[i];
            xpReward += GetComponent<Difficulty>().difficultyRewards[GetComponent<Difficulty>().difficulty];

            rewardInfoTexts[i].text = "XP Reward:\n" + xpReward;
        }
    }
    public void BackToHome()
    {
        home.SetActive(true);

        buttonClickSound.Play();

        gameObject.SetActive(false);
    }
}
