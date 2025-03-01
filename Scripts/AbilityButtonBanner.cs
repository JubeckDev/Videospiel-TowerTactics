using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButtonBanner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject heroUpgrades;
    public float cooldownTimeLeft;
    public int buffSelected;

    public GameObject towerPointer;
    public GameObject towerPointerSpawned;
    public Vector3 towerPointerOffset;

    public GameObject activateAbilityEffect;

    public GameObject waves;

    // Start is called before the first frame update
    void Start()
    {
        heroUpgrades = GameObject.Find("Canvas/GameUI/HeroUpgrades");
        waves = GameObject.Find("Waves");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartCooldown()
    {
        cooldownTimeLeft = heroUpgrades.GetComponent<HeroUpgrades>().buffCooldown;
        StartCoroutine("Cooldown");
        GetComponent<Button>().interactable = false;

    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        if (waves.GetComponent<Waves>().isInWave) cooldownTimeLeft--;
        if (cooldownTimeLeft <= 0)
        {
            GetComponent<Button>().interactable = true;
        }
        else StartCoroutine("Cooldown");
    }

    public void ClickAbility()
    {
        StartCooldown();
        heroUpgrades.GetComponent<HeroUpgrades>().TriggerBuffEffect(buffSelected);

        Instantiate(activateAbilityEffect, heroUpgrades.GetComponent<HeroUpgrades>().heroPanel.GetComponent<PlaceHero>().bannerPlaced.transform.position + towerPointerOffset, transform.rotation);

        DestroyTowerPointer();
    }
    public void SpawnTowerPointer()
    {
        towerPointerSpawned = Instantiate(towerPointer, heroUpgrades.GetComponent<HeroUpgrades>().heroPanel.GetComponent<PlaceHero>().bannerPlaced.transform.position + towerPointerOffset, transform.rotation);
    }
    public void DestroyTowerPointer()
    {
        Destroy(towerPointerSpawned);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable) SpawnTowerPointer();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyTowerPointer();
    }
}
