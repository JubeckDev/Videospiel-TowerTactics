using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject waves;

    public string towerType;
    public int towerTypeNumber;

    public List<GameObject> towers = new List<GameObject>();
    public List<float> cooldownTimeLeft = new List<float>();

    public int towersOffCooldown;

    public GameObject towerPointer;
    public GameObject towerPointerSpawned;
    public Vector3[] towerPointerOffsets;

    public GameObject activateAbilityEffect;

    public GameObject towerPanel;

    void Start()
    {
        waves = GameObject.Find("Waves");
        towerPanel = GameObject.Find("Canvas/GameUI/TowerPanel");

        for (int i = 0; i < towerPanel.GetComponent<TowerPanel>().towerTypes.Length; i++)
        {
            if (towerPanel.GetComponent<TowerPanel>().towerTypes[i] == towerType)
            {
                towerTypeNumber = i;
            }
        }

        StartCoroutine("Cooldown");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().interactable = cooldownTimeLeft[0] <= 0 && waves.GetComponent<Waves>().isInWave;

        CalculateTowersOffCooldown();
        UpdateNumberText();
        RemoveDestroyedTowers();

        if (towerPointerSpawned && !GetComponent<Button>().interactable) DestroyTowerPointer();
    }
    public void RemoveDestroyedTowers()
    {
        for (int i = 0; i < towers.Count; i++)
        {
            if (towers[i] == null)
            {
                towers.RemoveAt(i);
                cooldownTimeLeft.RemoveAt(i);
            }
        }
        if (towers.Count == 0) Destroy(gameObject);
    }
    public void CalculateTowersOffCooldown()
    {
        towersOffCooldown = 0;
        foreach (var currentCooldownTimeLeft in cooldownTimeLeft)
        {
            if (currentCooldownTimeLeft <= 0) towersOffCooldown++;
        }
    }
    public void UpdateNumberText()
    {
        transform.GetChild(0).gameObject.SetActive(towersOffCooldown > 1);

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = towersOffCooldown.ToString();
    }
    public void RequeueTower()
    {
        GameObject tower = towers[0];
        towers.RemoveAt(0);
        towers.Add(tower);

        cooldownTimeLeft.RemoveAt(0);
        cooldownTimeLeft.Add(towers[0].GetComponent<Stats>().cooldownTime);
    }
    IEnumerator Cooldown ()
    {
        yield return new WaitForSeconds(1);
        if (waves.GetComponent<Waves>().isInWave)
        {
            for (int i = 0; i < cooldownTimeLeft.Count; i++)
            {
                if (cooldownTimeLeft[i] > 0)
                {
                    cooldownTimeLeft[i]--;
                }
            }
        }
        StartCoroutine("Cooldown");
    }
    public void AddTower(GameObject tower)
    {
        towers.Insert(0, tower);
        cooldownTimeLeft.Insert(0, 0);
    }
    public void ClickAbility()
    {
        if (!waves.GetComponent<Waves>().isInWave) return;

        if(towerTypeNumber == 0) towers[0].GetComponent<VariationEffectsCannon>().TriggerAbility();
        if (towerTypeNumber == 1) towers[0].GetComponent<VariationEffectsCrystalTower>().TriggerAbility();
        if (towerTypeNumber == 2) towers[0].GetComponent<VariationEffectsCauldron>().TriggerAbility();
        if (towerTypeNumber == 3) towers[0].GetComponent<VariationEffectsBallista>().TriggerAbility();
        if (towerTypeNumber == 4) towers[0].GetComponent<VariationEffectsShip>().TriggerAbility();
        if (towerTypeNumber == 5) towers[0].GetComponent<VariationEffectsMagicTower>().TriggerAbility();
        if (towerTypeNumber == 6) towers[0].GetComponent<VariationEffectsIceDragon>().TriggerAbility();
        if (towerTypeNumber == 7) towers[0].GetComponent<VariationEffectsPhoenix>().TriggerAbility();

        Instantiate(activateAbilityEffect, towers[0].transform.position + towerPointerOffsets[towerTypeNumber], transform.rotation);
        RequeueTower();
        DestroyTowerPointer();
        if (GetComponent<Button>().interactable) SpawnTowerPointer();
    }
    public void SpawnTowerPointer()
    {
        towerPointerSpawned = Instantiate(towerPointer, towers[0].transform.position + towerPointerOffsets[towerTypeNumber], transform.rotation);
    }
    public void DestroyTowerPointer()
    {
        Destroy(towerPointerSpawned);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GetComponent<Button>().interactable) SpawnTowerPointer();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyTowerPointer();
    }
}
