using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceSettings : MonoBehaviour
{
    public Terrain[] terrains;

    public Button[] qualityButtons;
    public Button[] terrainGrassButtons;
    public Button[] particleCountButtons;

    public AudioSource buttonClickSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeQuality(int number)
    {
        /*for (int i = 0; i < qualityButtons.Length; i++)
        {
            qualityButtons[i].interactable = i != number;
        }*/
        for (int i = 0; i < qualityButtons.Length; i++)
        {
            qualityButtons[i].interactable = true;
        }
        switch (number)
        {
            case 0:
                qualityButtons[0].interactable = false;
                break;
            case 2:
                qualityButtons[1].interactable = false;
                break;
            case 3:
                qualityButtons[2].interactable = false;
                break;
            default:
                qualityButtons[3].interactable = false;
                break;
        }

        string[] names = QualitySettings.names;

        QualitySettings.SetQualityLevel(number, true);

        buttonClickSound.Play();
    }
    public void ChangeTerrainGrass(bool toActive)
    {
        terrainGrassButtons[0].interactable = toActive;
        terrainGrassButtons[1].interactable = !toActive;

        for (int i = 0; i < terrains.Length; i++)
        {
            terrains[i].drawTreesAndFoliage = toActive;
        }

        buttonClickSound.Play();
    }
    public void ChangeParticeCount(bool toHigh)
    {
        terrainGrassButtons[0].interactable = toHigh;
        terrainGrassButtons[1].interactable = !toHigh;

        //Change particle count

        buttonClickSound.Play();
    }
}
