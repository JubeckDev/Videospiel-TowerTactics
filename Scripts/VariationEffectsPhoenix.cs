using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariationEffectsPhoenix : MonoBehaviour
{
    public GameObject abilityPanel;
    public float abilityTime;
    public ParticleSystem flames;
    public GameObject fireRing;
    public GameObject[] enemies;
    public float fireRingCooldown = 20;
    public int fireRingDamageMultiplier = 5;
    public bool hasUnlockedMeteor;
    public bool meteorIsOnCooldown;
    public GameObject enemy;
    public GameObject meteor;
    public Vector3 meteorOffSet;
    public int meteorDamageMultiplier = 2;
    public float meteorCooldown = 30;
    public float meteorSize = 1.6f;
    public GameObject maps;
    public float flamesColliderDelay = 0.2f;

    public GameObject skillTree;

    public GameObject flamesSound;

    public string[] variationInfoTexts = new string[3];

    void Start()
    {
        abilityPanel = GameObject.Find("Canvas/GameUI/AbilityPanel");

        skillTree = GameObject.Find("Canvas/Menu/SkillTree");
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[8]) GetComponent<Stats>().bulletMovementSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[8];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[9]) fireRingCooldown += skillTree.GetComponent<SkillTree>().achievementIncreases[9];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[12]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[12];

        SetVariationInfoTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasUnlockedMeteor == true)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                int randomizer = Random.Range(0, enemies.Length - 1);
                enemy = enemies[randomizer];
                if (!meteorIsOnCooldown)
                {
                    StartCoroutine("SpawnMeteor");

                }
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
        flames.gameObject.GetComponent<DamageToEnemy>().damage = GetComponent<Stats>().damage;
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
        GetComponent<Shoot>().shouldTargetEnemies = true;
    }

    public void SetVariation1()
    {
        Debug.Log("Bought Variation2");
        StartCoroutine("FireRingCoroutine");
    }
    IEnumerator FireRingCoroutine() 
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length>0)
        {
            GameObject strongestEnemy = GetStrongestEnemy();
            GameObject fireRingSpawned = Instantiate(fireRing, strongestEnemy.transform.position, strongestEnemy.transform.rotation) as GameObject;
            fireRingSpawned.transform.SetParent(strongestEnemy.transform);
            fireRingSpawned.GetComponent<FireRing>().damage = GetComponent<Stats>().damage * fireRingDamageMultiplier;
        }
        yield return new WaitForSeconds(fireRingCooldown);
        StartCoroutine("FireRingCoroutine");
    }
    public GameObject GetStrongestEnemy()
    {
        int maxLives = 0;
        GameObject strongestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            int lives = enemy.GetComponent<EnemyLives>().lives; 
            if (lives > maxLives)
            {
                strongestEnemy = enemy;
                maxLives = lives;
            }
        }
        return strongestEnemy;
    }
    public void SetVariation2()
    {
        Debug.Log("Bought Variation3");
        hasUnlockedMeteor = true;
    }
    IEnumerator SpawnMeteor()
    {
        GameObject meteorSpawned = Instantiate(meteor, enemy.transform.position + meteorOffSet, transform.rotation) as GameObject;
        meteorSpawned.transform.Rotate(-90, 0, 0);
        meteorSpawned.GetComponent<Meteor>().damage = GetComponent<Stats>().damage * meteorDamageMultiplier;
        meteorSpawned.transform.localScale = new Vector3(meteorSize, meteorSize, meteorSize);
        meteorIsOnCooldown = true;
        yield return new WaitForSeconds(meteorCooldown);
        meteorIsOnCooldown = false;

    }
    public void SetVariationInfoTexts()
    {
        variationInfoTexts[0] = "Burn Duration: " + flames.GetComponent<DamageToEnemy>().burnTime + "s\nDuration: " + flames.GetComponent<ParticleSystem>().main.duration + "s\nCooldown: " + GetComponent<Stats>().cooldownTime + "s";
        variationInfoTexts[1] = "Damage: " + GetComponent<Stats>().damage * fireRingDamageMultiplier + "\nCooldown: " + fireRingCooldown + "s";
        variationInfoTexts[2] = "Damage: " + GetComponent<Stats>().damage * meteorDamageMultiplier + "\nBurn Duration: " + meteor.GetComponent<Meteor>().dust.GetComponent<DamageToEnemy>().burnTime + "s\n Cooldown: " + meteorCooldown + "s";
    }
}