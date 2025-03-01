using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArtifactInfo : MonoBehaviour
{
    public GameObject artifactInfoPanel;
    public TextMeshProUGUI description;
    public GameObject artifactPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ArtifactInfoButton()
    {
        Debug.Log(artifactInfoPanel.activeSelf);
        if (!artifactInfoPanel.activeSelf) OpenWindow();
        else CloseWindow();
    }
    public void OpenWindow()
    {
        artifactInfoPanel.SetActive(true);

        int towerTypeNumber = 0;

        for (int i = 0; i < artifactPanel.GetComponent<ArtifactPanel>().towerTypes.Length; i++)
        {
            if (artifactPanel.GetComponent<ArtifactPanel>().towerTypes[i] == artifactPanel.GetComponent<ArtifactPanel>().currentTowerType)
            {
                towerTypeNumber = i;
                break;
            }
        }

        description.text = artifactPanel.GetComponent<ArtifactPanel>().descriptions[towerTypeNumber];
    }
    public void CloseWindow()
    {
        artifactInfoPanel.SetActive(false);
    }
}
