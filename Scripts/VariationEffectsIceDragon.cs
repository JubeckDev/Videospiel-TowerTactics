using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariationEffectsIceDragon : MonoBehaviour
{
    public GameObject abilityPanel;
    public float abilityTime;
    public ParticleSystem flames;
    public float iceWallCooldown = 30;
    public GameObject iceWall;
    public GameObject iceWallSpawned;
    public int iceWallLives = 6;
    public GameObject dragon;
    public GameObject iceSpikeField;
    public float iceSpikeFieldCooldown;
    public float iceSpikeFieldDuration;
    public Vector3 spikeFieldOffSet;
    public GameObject maps;
    public bool hasUnlockedIceSpikeField;
    public bool iceSpikeFieldIsOnCooldown;
    public float flamesColliderDelay = 0.2f;
    public float iceSpikeFieldSlowTime = 2.5f;

    public bool hasUnlockedIceWall;
    public bool iceWallIsOnCooldown;

    public GameObject skillTree;

    public GameObject flamesSound;
    public GameObject wallSound;

    public GameObject projectiles;

    public string[] variationInfoTexts = new string[3];

    void Start()
    {
        projectiles = GameObject.Find("Projectiles");
        abilityPanel = GameObject.Find("Canvas/GameUI/AbilityPanel");

        skillTree = GameObject.Find("Canvas/Menu/SkillTree");
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[8]) GetComponent<Stats>().bulletMovementSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[8];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[12]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[12];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[14]) iceWallLives += Mathf.RoundToInt(skillTree.GetComponent<SkillTree>().achievementIncreases[14]);
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[15]) iceSpikeFieldSlowTime += skillTree.GetComponent<SkillTree>().achievementIncreases[15];

        SetVariationInfoTexts();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GetComponent<Stats>().rangeDisplay.GetComponent<Range>().enemy)
        {
            if (hasUnlockedIceSpikeField && !iceSpikeFieldIsOnCooldown)
            {
                StartCoroutine("IceSpikeFieldCoroutine");
            }
            if (hasUnlockedIceWall && !iceWallIsOnCooldown && iceWallSpawned == null)
            {
                StartCoroutine("IceWallCoroutine");
            }
        }
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
        flames.Play();
        Instantiate(flamesSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        flames.GetComponent<DamageToEnemy>().damage = GetComponent<Stats>().damage;
        yield return new WaitForSeconds(flamesColliderDelay);
        flames.gameObject.GetComponent<BoxCollider>().enabled = true;
        GetComponent<Shoot>().shouldTargetEnemies = false;
        yield return new WaitForSeconds(abilityTime - flamesColliderDelay);
        flames.gameObject.GetComponent<BoxCollider>().enabled = false;
        GetComponent<Shoot>().shouldTargetEnemies= true;
    }

    public void SetVariation1()
    {
        Debug.Log("Bought Variation2");
        hasUnlockedIceSpikeField = true;
    }
    IEnumerator IceSpikeFieldCoroutine()
    {
        iceSpikeFieldIsOnCooldown = true; 
        GameObject iceSpikeFieldSpawned = Instantiate(iceSpikeField, GetComponent<Stats>().rangeDisplay.GetComponent<Range>().enemy.transform.position+spikeFieldOffSet, transform.rotation, projectiles.transform) as GameObject;
        iceSpikeFieldSpawned.GetComponent<Lifetime>().lifetime = iceSpikeFieldDuration;
        iceSpikeFieldSpawned.GetComponent<DamageToEnemy>().damage = GetComponent<Stats>().damage;
        //iceSpikeFieldSpawned.GetComponent<DamageToEnemy>().slowTime = iceSpikeFieldCooldown;
        yield return new WaitForSeconds(iceSpikeFieldCooldown);
        iceSpikeFieldIsOnCooldown = false;
    }
    public void SetVariation2()
    {
        Debug.Log("Bought Variation3");
        hasUnlockedIceWall = true;
    }
    IEnumerator IceWallCoroutine()
    {
        iceWallIsOnCooldown = true;
        iceWallSpawned = Instantiate(iceWall, transform.position, transform.rotation) as GameObject;
        iceWallSpawned.transform.rotation = dragon.transform.rotation;
        iceWallSpawned.transform.Rotate(0, -90 ,0);
        iceWallSpawned.GetComponent<IceWall>().lives = iceWallLives;
        Instantiate(wallSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        yield return new WaitForSeconds(iceWallCooldown);
        iceWallIsOnCooldown = false;
    }
    public void SetVariationInfoTexts()
    {
        variationInfoTexts[0] = "Freeze Duration: " + flames.GetComponent<DamageToEnemy>().freezeTime + "s\nDuration: " + abilityTime + "s\nCooldown: " + GetComponent<Stats>().cooldownTime + "s";
        variationInfoTexts[1] = "Slow Duration: " + iceSpikeField.GetComponent<DamageToEnemy>().slowTime + "s\nSpike Duration: " + iceSpikeField.GetComponent<Lifetime>().lifetime + "s\nCooldown: " + iceSpikeFieldCooldown + "s";
        variationInfoTexts[2] = "Damage: " + iceWall.GetComponent<IceWall>().damageCap + "\nLives: " + iceWallLives + "\nCooldown: " + iceWallCooldown + "s";
    }
}