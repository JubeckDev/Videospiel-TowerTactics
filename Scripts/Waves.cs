using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject[] waypointParents;
    public GameObject currentWaypointParent;

    public int waveCount = 1;
    public int waveGoal;

    public int[] waveGoals;

    public bool isInWave;

    public bool waveCanEnd;

    public TextMeshProUGUI waveCounterBetweenWaves;
    public TextMeshProUGUI waveCounterDuringWave;

    public GameObject waveUIBetweenWaves;
    public GameObject waveUIDuringWaves;

    public GameObject[] spawnCirclesDefault;
    public GameObject[] spawnCirclesSpawning;

    public GameObject enemiesParent;

    public GameObject achievements;
    public GameObject gameUI;

    public int[] waveGoldRewards;

    public GameObject winPanel;

    public GameObject settings;
    public AudioSource buttonClickSound;
    public AudioSource lastWaveSound;
    public AudioSource winSound;
    public AudioSource startWave;
    public GameObject balloonSpawnSound;
    public GameObject balloonSpawnEffect;

    public GameObject maps;

    public Vector3 waypointOffset = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        SetWaveGoal();
        SetWaveCountText();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (waveCanEnd && gameUI.GetComponent<Lives>().lives > 0)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)//Round completed
            {
                waveCanEnd = false;
                isInWave = false;
                gameUI.GetComponent<Gold>().ChangeGold(waveGoldRewards[waveCount]);

                achievements.GetComponent<Achievements>().SetNewProgress(9, waveCount);
                achievements.GetComponent<Achievements>().SetNewProgress(10, waveCount);
                achievements.GetComponent<Achievements>().SetNewProgress(11, waveCount);

                if (waveCount >= waveGoal)
                {
                    EndGame();

                }
                else
                {
                    waveCount++;
                    if (settings.GetComponent<GameSettings>().autoWaveIsOn)
                    {
                        SpawnWave(false);
                    }
                    ToggleWaveUI();
                    SetWaveCountText();
                }
            }
        }
    }
    public void SpawnWave(bool shouldPlayButtonSound)
    {
        StartCoroutine("SpawnWaveCoroutine", waveCount - 1);
        if(shouldPlayButtonSound) buttonClickSound.Play();

        if (waveCount == waveGoal)
        {
            lastWaveSound.Play();
        }
        else
        {
            startWave.Play();
        }
    }
    public void StopWave()
    {
        StopCoroutine("SpawnWaveCoroutine");
        if(waveUIDuringWaves.activeSelf) ToggleWaveUI();
        if(spawnCirclesSpawning[0].activeSelf) ToggleSpawnCirlce();
    }
    IEnumerator SpawnWaveCoroutine(int waveIndex)
    {
        isInWave = true;
        ToggleWaveUI();
        ToggleSpawnCirlce();
        for (int i = 0; i < gameObject.transform.GetChild(waveIndex).GetComponent<Wave>().waveEnemyLevels.Length; i++)
        {
            //Spawn enemy
            GameObject enemySpawned = Instantiate(gameObject.transform.GetChild(waveIndex).GetComponent<Wave>().waveEnemyType[i], currentWaypointParent.transform.GetChild(0).transform.position + waypointOffset, currentWaypointParent.transform.GetChild(0).transform.rotation) as GameObject;
            enemySpawned.transform.SetParent(enemiesParent.transform);

            //set stage of enemy
            enemySpawned.GetComponent<EnemyLives>().initialStage = gameObject.transform.GetChild(waveIndex).GetComponent<Wave>().waveEnemyLevels[i];

            //Instantiate(balloonSpawnSound, currentWaypointParent.transform.GetChild(0).transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
            Instantiate(balloonSpawnEffect, currentWaypointParent.transform.GetChild(0).transform.position, transform.rotation, GameObject.Find("Particles").transform);

            //Wait to spawn next enemy if the wave is not over
            if (i != gameObject.transform.GetChild(waveIndex).GetComponent<Wave>().waveEnemyLevels.Length - 1)
            {
                yield return new WaitForSeconds(gameObject.transform.GetChild(waveIndex).GetComponent<Wave>().waveDelays[i]);
            }
        }
        waveCanEnd = true;
        ToggleSpawnCirlce();
    }
    public void EndGame()
    {
        Debug.Log("All waves beaten.");
        winPanel.GetComponent<GameEndPanel>().OpenWindow();
        winSound.Play();
    }
    public void SetWaveGoal()
    {
        waveGoal = waveGoals[maps.GetComponent<Difficulty>().difficulty];
    }
    public void SetWaveCountText()
    {
        waveCounterBetweenWaves.text = waveCount.ToString() + "/" + waveGoal.ToString();
        waveCounterDuringWave.text = "Wave " + waveCount.ToString() + "/" + waveGoal.ToString();
    }
    public void ToggleWaveUI()
    {
        waveUIBetweenWaves.SetActive(!waveUIBetweenWaves.activeSelf);
        waveUIDuringWaves.SetActive(!waveUIDuringWaves.activeSelf);
    }
    public void ToggleSpawnCirlce()
    {
        for (int i = 0; i < spawnCirclesDefault.Length; i++)
        {
            spawnCirclesDefault[i].SetActive(!spawnCirclesDefault[i].activeSelf);
            spawnCirclesSpawning[i].SetActive(!spawnCirclesSpawning[i].activeSelf);
        }
    }
}
