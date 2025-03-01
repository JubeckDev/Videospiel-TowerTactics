using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariationEffectsBallista : MonoBehaviour
{
    public GameObject abilityPanel;
    public float abilityTime;
    public float attackSpeedBuffMultiplier;
    public GameObject arrowSplit;
    public float arrowSplitSize = 60;
    public float arrowSize = 100;
    public float attackSpeedMultiplierVariation2;

    public GameObject skillTree;

    public string[] variationInfoTexts = new string[3];

    void Start()
    {
        abilityPanel = GameObject.Find("Canvas/GameUI/AbilityPanel");

        skillTree = GameObject.Find("Canvas/Menu/SkillTree");
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[0]) GetComponent<Stats>().cooldownTime += skillTree.GetComponent<SkillTree>().achievementIncreases[0];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[1]) GetComponent<Stats>().poisonTime += skillTree.GetComponent<SkillTree>().achievementIncreases[1];
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
        StartCoroutine("TriggerAbilityCoroutine");
    }
    IEnumerator TriggerAbilityCoroutine()
    {
        GetComponent<Stats>().attackSpeed *= attackSpeedBuffMultiplier;
        if(GetComponent<Shoot>().isShooting)
        {
            GetComponent<Shoot>().StopCoroutine("ShootBullet");
            GetComponent<Shoot>().StartCoroutine("ShootBullet");
        }
        
        yield return new WaitForSeconds(abilityTime);
        GetComponent<Stats>().attackSpeed /= attackSpeedBuffMultiplier;
    }


    public void SetVariation1()
    {
        Debug.Log("Bought Variation2");
        GetComponent<Stats>().canPoison = true;
      
    }
    public void SetVariation2()
    {
        Debug.Log("Bought Variation3");
        GetComponent<Shoot>().bullet = arrowSplit;
        GetComponent<Stats>().piercing = 1;
        GetComponent<Stats>().attackSpeed *= attackSpeedMultiplierVariation2;
    }
    public void SetVariationInfoTexts()
    {
        variationInfoTexts[0] = "Attack Speed: " + Mathf.RoundToInt(1 / attackSpeedBuffMultiplier * 100) + "%\nDuration: " + abilityTime + "s\nCooldown: " + GetComponent<Stats>().cooldownTime + "s";
        //variationInfoTexts[0] = "Duration: " + abilityTime + "s\nCooldown: " + GetComponent<Stats>().cooldownTime + "s\nAttack Speed: " + Mathf.RoundToInt(1 / attackSpeedBuffMultiplier * 100) + "%";
        variationInfoTexts[1] = "Duration: " + GetComponent<Stats>().poisonTime + "s";
        variationInfoTexts[2] = "Attack Speed: " + Mathf.RoundToInt(1 / attackSpeedMultiplierVariation2 * 100) + "%\nSplit Arrow Amount: 2";
    }
}