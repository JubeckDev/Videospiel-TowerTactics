using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string towerType;

    public int totalTowerCost;

    public float bulletMovementSpeed = 3;
    public float range = 5;
    public int damage = 5;
    public int piercing = 1;
    public float attackSpeed = 1;

    public float cooldownTime;

    public GameObject rangeDisplay;

    public bool[] buffActive;
    public GameObject heroUpgrades;

    public bool canSlow;
    public float slowTime;
    public bool canBurn;
    public float burnTime;
    public bool canConfuse;
    public float confusionTime;
    public bool canPoison;
    public float poisonTime;

    public string damageType;

    public bool hasArtifact;
    private int towerNumber;
    private GameObject artifactPanel;

    public GameObject buffParticles;

    // Start is called before the first frame update
    void Start()
    {
        heroUpgrades = GameObject.Find("Canvas/GameUI/HeroUpgrades");

        //Check if the tower has an artifact
        artifactPanel = GameObject.Find("Canvas/GameUI/ArtifactPanel");
        //Debug.Log(artifactPanel.name);
        towerNumber = System.Array.IndexOf(artifactPanel.GetComponent<ArtifactPanel>().towerNames, towerType);

        //rangeDisplay.GetComponent<MeshRenderer>().enabled = false;

        UpdateRange();
    }

    // Update is called once per frame
    void Update()
    {
        if (artifactPanel.GetComponent<ArtifactPanel>().hasArtifact[towerNumber] && !hasArtifact)
        {
            hasArtifact = true;
        }
    }
    public void UpdateRange()
    {
        float rangeUsed = (buffActive[4] ? range + heroUpgrades.GetComponent<HeroUpgrades>().buffIncrease[4] : range);

        rangeDisplay.transform.localScale = new Vector3(rangeUsed * 2, rangeUsed * 2, rangeUsed * 2);

        //GetComponent<SphereCollider>().radius = range;
    }
}
