using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariationEffectsCauldron : MonoBehaviour
{
    public GameObject abilityPanel;
    public Vector3 puddleOffset;
    public GameObject potionBurn;
    public GameObject potionSlow;
    public float puddleLifetime = 5;
    public float puddleSize = 0.4f;
    public Vector3 splashPotionAimOffset;

    public GameObject skillTree;

    public GameObject projectiles;

    public string[] variationInfoTexts = new string[3];

    void Start()
    {
        projectiles = GameObject.Find("Projectiles");
        abilityPanel = GameObject.Find("Canvas/GameUI/AbilityPanel");

        skillTree = GameObject.Find("Canvas/Menu/SkillTree");
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[3]) GetComponent<Stats>().burnTime += skillTree.GetComponent<SkillTree>().achievementIncreases[3];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[5]) GetComponent<Stats>().range += skillTree.GetComponent<SkillTree>().achievementIncreases[5];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[6]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[6];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[7]) GetComponent<Stats>().bulletMovementSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[7];

        SetVariationInfoTexts();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetVariationEffect(int number)
    {
        if (number == 0)
            SetVariation0();
        if (number == 1)
            SetVariation1();
        if (number == 2)
            SetVariation2();
    }
    public void SetVariation0()
    {
        abilityPanel.GetComponent<AbilityPanel>().SpawnAbilityButton(gameObject);
    }

    public void TriggerAbility()
    {
        GameObject potionSpawned = Instantiate(GetComponent<Shoot>().bullet, GetComponent<Shoot>().shootPoints[GetComponent<Shoot>().currentActiveSkin].transform.position+ puddleOffset, transform.parent.rotation, projectiles.transform) as GameObject;
        potionSpawned.transform.LookAt(GetComponent<Stats>().rangeDisplay.GetComponent<Range>().enemy.transform.position + splashPotionAimOffset);
        potionSpawned.GetComponent<Bullet>().canSplash = true;
        potionSpawned.GetComponent<Bullet>().piercing = 20;
        potionSpawned.GetComponent<Bullet>().puddleSize = puddleSize;
        potionSpawned.GetComponent<Bullet>().puddleLifetime = puddleLifetime;
    }

    public void SetVariation1()
    {
        Debug.Log("Bought Variation2");
        GetComponent<Shoot>().bullet = potionBurn;
        GetComponent<Stats>().canBurn = true;
    }
    public void SetVariation2()
    {
        Debug.Log("Bought Variation3");
        GetComponent<Shoot>().bullet = potionSlow;
        GetComponent<Stats>().canSlow = true;
    }
    public void SetVariationInfoTexts()
    {
        variationInfoTexts[0] = "Splash Duration: " + puddleLifetime + "s\nBurn Duration: " + GetComponent<Shoot>().bullet.GetComponent<Bullet>().splashObject.GetComponent<DamageToEnemy>().burnTime + "s\nCooldown: " + GetComponent<Stats>().cooldownTime + "s";
        variationInfoTexts[1] = "Duration: " + GetComponent<Stats>().burnTime + "s";
        variationInfoTexts[2] = "Duration: " + GetComponent<Stats>().slowTime + "s";
    }
}