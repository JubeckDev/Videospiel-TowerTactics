using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariationEffectsCrystalTower : MonoBehaviour
{
    public GameObject abilityPanel;

    public GameObject maps;
 

    public float abilityTime;

    public GameObject[] enemies;
    public float swordCoolDown;
    public bool swordIsOnCoolDown;
    public GameObject spawnSword;
    public GameObject enemy;
    public GameObject enemyClosest;
    public Vector3 swordOffset;
    public bool hasUnlockedSpawnSword;
    public int spawnSwordDamageMultiplier;

    public GameObject crystalRain;
    public Vector3 crystalRainOffset;

    public float crystalSize = 0.5f;

    public GameObject skillTree;

    public GameObject magicSwordSound;

    public GameObject projectiles;

    public string[] variationInfoTexts = new string[3];

    void Start()
    {
        projectiles = GameObject.Find("Projectiles");
        maps = GameObject.Find("Canvas/Menu/Maps");
        abilityPanel = GameObject.Find("Canvas/GameUI/AbilityPanel");

        skillTree = GameObject.Find("Canvas/Menu/SkillTree");
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[2]) GetComponent<Stats>().cooldownTime += skillTree.GetComponent<SkillTree>().achievementIncreases[2];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[5]) GetComponent<Stats>().range += skillTree.GetComponent<SkillTree>().achievementIncreases[5];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[6]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[6];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[7]) GetComponent<Stats>().bulletMovementSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[7];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[26]) GetComponent<Enhancements>().goldCosts[3] += Mathf.RoundToInt(skillTree.GetComponent<SkillTree>().achievementIncreases[26]);

        SetVariationInfoTexts();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            for (int i = 0; i < enemies.Length; i++)
            {
                float dist = Vector3.Distance(enemies[i].transform.position, currentPos);
                if (dist < minDist)
                {
                    enemyClosest = enemies[i];
                    minDist = dist;
                }
            }
        }
        else
        {
            enemyClosest = null;
        }

        if (hasUnlockedSpawnSword == true) 
        {
            if (enemies.Length > 0)
            {
                int randomizer = Random.Range(0, enemies.Length - 1);
                enemy = enemies[randomizer];
                if(!swordIsOnCoolDown) 
                {
                    StartCoroutine("SpawnSword");
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
    }

   
    public void TriggerAbility()
    {
        if (enemyClosest != null)
        {
            GameObject crystalRainSpawned = Instantiate(crystalRain, enemyClosest.transform.position + crystalRainOffset, transform.rotation, projectiles.transform);
            crystalRainSpawned.GetComponent<CrystalRainCollision>().damage = GetComponent<Stats>().damage;
            var main = crystalRainSpawned.GetComponent<ParticleSystem>().main;
            main.startSize = crystalSize;
        }
    }
   


    public void SetVariation1()
    {
        GetComponent<Stats>().canSlow = true;
        Debug.Log("Bought Variation2");
      

    }
    public void SetVariation2()
    {
        hasUnlockedSpawnSword = true;
        Debug.Log("Bought Variation3");
    }

    IEnumerator SpawnSword()
    {
        GameObject spawnSwordSpawned = Instantiate(spawnSword, enemy.transform.position + swordOffset, transform.rotation, projectiles.transform);
        spawnSwordSpawned.GetComponent<SpawnSword>().damage = GetComponent<Stats>().buffActive[0] ? (GetComponent<Stats>().damage + Mathf.RoundToInt(GetComponent<Stats>().heroUpgrades.GetComponent<HeroUpgrades>().buffIncrease[0])) * spawnSwordDamageMultiplier : GetComponent<Stats>().damage * spawnSwordDamageMultiplier;

        Instantiate(magicSwordSound, spawnSwordSpawned.transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        swordIsOnCoolDown = true;
        yield return new WaitForSeconds(swordCoolDown);
        swordIsOnCoolDown = false;

    }
    public void SetVariationInfoTexts()
    {
        variationInfoTexts[0] = "Duration: " + crystalRain.GetComponent<ParticleSystem>().emission.GetBurst(0).cycleCount *
            crystalRain.GetComponent<ParticleSystem>().emission.GetBurst(0).repeatInterval + "s\nCooldown: " + GetComponent<Stats>().cooldownTime + "s";
        variationInfoTexts[1] = "Duration: " + GetComponent<Stats>().slowTime + "s";
        variationInfoTexts[2] = "Damage: " + GetComponent<Stats>().damage * spawnSwordDamageMultiplier + "\nCooldown: " + swordCoolDown + "s";
    }

}