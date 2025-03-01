using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariationEffectsMagicTower : MonoBehaviour
{
    public GameObject abilityPanel;
    public float abilityTime;
    public GameObject musicNote;
    public GameObject musicNoteRing;
    public int musicNoteRingDamageIncrease;
    public GameObject musicNoteConfusion;
    public GameObject musicNoteSpin;
    public int musicNoteSpinDamageIncrease;

    public GameObject skillTree;

    public GameObject noteRingSound;

    public string[] variationInfoTexts = new string[3];

    void Start()
    {
        abilityPanel = GameObject.Find("Canvas/GameUI/AbilityPanel");

        skillTree = GameObject.Find("Canvas/Menu/SkillTree");
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[8]) GetComponent<Stats>().bulletMovementSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[8];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[12]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[12];

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
        GetComponent<Shoot>().bullet = musicNoteRing;
        GetComponent<Stats>().damage += musicNoteRingDamageIncrease;
        Instantiate(noteRingSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        yield return new WaitForSeconds(abilityTime);
        GetComponent<Shoot>().bullet = musicNote;
        GetComponent<Stats>().damage -= musicNoteRingDamageIncrease;
    }

    public void SetVariation1()
    {
        Debug.Log("Bought Variation2");
        GetComponent<Stats>().canConfuse = true;
        GetComponent<Shoot>().bullet = musicNoteConfusion;
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[13]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[13];
    }

    public void SetVariation2()
    {
        Debug.Log("Bought Variation3");
        GetComponent<Shoot>().bullet = musicNoteSpin;
        GetComponent<Stats>().damage += musicNoteSpinDamageIncrease;
    }
    public void SetVariationInfoTexts()
    {
        variationInfoTexts[0] = "Damage: +" + musicNoteRingDamageIncrease + "\nDuration: " + abilityTime + "s\nCooldown: " + GetComponent<Stats>().cooldownTime + "s";
        variationInfoTexts[1] = "Knockback Duration: " + GetComponent<Stats>().confusionTime + "s";
        variationInfoTexts[2] = "Damage: +" + musicNoteSpinDamageIncrease;
    }
}