using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPanel : MonoBehaviour
{
    public GameObject abilityButton;
    public GameObject abilityButtonSpawned;

    public List<GameObject> abilityButtons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < abilityButtons.Count; i++)
        {
            if (abilityButtons[i] == null) abilityButtons.RemoveAt(i);
        }
    }
    public void SpawnAbilityButton(GameObject tower)
    {
        foreach (var currentAbilityButton in abilityButtons)
        {
            /*if (tower.GetComponent<StatsHero>())
            {
                if (currentAbilityButton.GetComponent<AbilityButton>().towerType == tower.GetComponent<StatsHero>().towerType)
                {
                    currentAbilityButton.GetComponent<AbilityButton>().AddTower(tower);
                    return;
                }
            }
            else
            {*/
                if (currentAbilityButton.GetComponent<AbilityButton>().towerType == tower.GetComponent<Stats>().towerType)
                {
                    currentAbilityButton.GetComponent<AbilityButton>().AddTower(tower);
                    return;
                }
            //}
        }

        abilityButtonSpawned = Instantiate(abilityButton, transform.position, transform.rotation, transform) as GameObject;

        if (tower.GetComponent<StatsHero>()) abilityButtonSpawned.GetComponent<AbilityButton>().towerType = tower.GetComponent<StatsHero>().towerType;
        else abilityButtonSpawned.GetComponent<AbilityButton>().towerType = tower.GetComponent<Stats>().towerType;

        abilityButtonSpawned.GetComponent<Image>().sprite = tower.GetComponent<Enhancements>().variationSprites[0];
        abilityButtonSpawned.transform.localScale = new Vector3(1, 1, 1);
        abilityButtonSpawned.GetComponent<AbilityButton>().AddTower(tower);

        //? idk what it does and it gives errors pls help me im dying
        /*for (int i = 0; i < transform.childCount; i++)
        {
            if (tower.GetComponent<StatsHero>())
            {
                if (transform.GetChild(i).GetComponent<AbilityButton>().towerType == tower.GetComponent<StatsHero>().towerType)
                {
                    abilityButtons.Add(transform.GetChild(i).gameObject);
                }
            }
            else
            {
                if (transform.GetChild(i).GetComponent<AbilityButton>().towerType == tower.GetComponent<Stats>().towerType)
                {
                    abilityButtons.Add(transform.GetChild(i).gameObject);
                }
            }
        }*/
    }
}
