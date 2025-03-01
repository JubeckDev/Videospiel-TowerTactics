using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanel : MonoBehaviour
{
    public GameObject heroSelected;
    public GameObject panel;

    public Image previewFrame;

    public Color colorNoArtifactPreviewFrame;
    public Color colorArtifactPreviewFrame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseWindow()
    {
        panel.SetActive(false);
        ChangeRangeVisualizerState(false);
    }
    public void ChangeRangeVisualizerState(bool toActive)
    {
        if (heroSelected)
        {
            heroSelected.GetComponent<InitializeMeshesBanner>().rangeVisualizer.GetComponent<MeshRenderer>().enabled = toActive;
        }
    }
    public void OpenWindow(GameObject heroSelected)
    {
        //deactivate range visualizer of previously selected tower
        ChangeRangeVisualizerState(false);

        //Activate Panel
        panel.SetActive(true);

        //Assign new tower
        this.heroSelected = heroSelected;
        ChangeRangeVisualizerState(true);

        UpdateArtifactUI();
    }
    public void UpdateArtifactUI()
    {
        previewFrame.color = GetComponent<SpawnHeroBanner>().hero.GetComponent<StatsHero>().hasArtifact ? colorArtifactPreviewFrame : colorNoArtifactPreviewFrame;
    }
}
