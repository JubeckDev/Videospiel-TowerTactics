using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariationEffectsShip : MonoBehaviour
{
    public GameObject abilityPanel;
    public GameObject bigFireball;
    public GameObject bigFireballSpawned;

    public float randomEffectCycleAttackSpeedDebuff = 0.3f;

    public GameObject fireball;
    public GameObject poisonball;
    public GameObject slowball;

    public float bigFireballSize = 3;

    public GameObject skillTree;

    public GameObject bigFireballSound;

    public GameObject projectiles;

    public GameObject enemyClosest;
    public GameObject[] enemies;

    public string[] variationInfoTexts = new string[3];

    // Start is called before the first frame update
    void Start()
    {
        projectiles = GameObject.Find("Projectiles");
        abilityPanel = GameObject.Find("Canvas/GameUI/AbilityPanel");

        skillTree = GameObject.Find("Canvas/Menu/SkillTree");
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[8]) GetComponent<Stats>().bulletMovementSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[8];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[10]) GetComponent<Stats>().range += skillTree.GetComponent<SkillTree>().achievementIncreases[10];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[12]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[12];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[31]) GetComponent<Enhancements>().goldCosts[0] += Mathf.RoundToInt(skillTree.GetComponent<SkillTree>().achievementIncreases[31]);

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
        if(enemyClosest != null)
        {
            bigFireballSpawned = Instantiate(bigFireball, GetComponent<Shoot>().shootPoints[GetComponent<Shoot>().currentActiveSkin].transform.position, GetComponent<Shoot>().cannonTops[GetComponent<Shoot>().currentActiveSkin].transform.rotation, projectiles.transform) as GameObject;
            bigFireballSpawned.transform.LookAt(enemyClosest.transform.position);
            bigFireballSpawned.transform.localScale = new Vector3(bigFireballSize, bigFireballSize, bigFireballSize);
            Debug.Log("fireball");

            Instantiate(bigFireballSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        }
       
    }
    public void SetVariation1()
    {
        Debug.Log("Bought Variation2");
        GetComponent<Stats>().attackSpeed += randomEffectCycleAttackSpeedDebuff;
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[11]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[11];
        StartCoroutine("RandomEffectCycle");
    }
    IEnumerator RandomEffectCycle()
    {
        int randomEffect = Random.Range(0, 3);
        GetComponent<Stats>().canBurn = false;
        GetComponent<Stats>().canSlow = false;
        GetComponent<Stats>().canPoison = false;
        if (randomEffect == 0)
        {
            GetComponent<Stats>().canBurn = true;
            GetComponent<Shoot>().bullet = fireball;
        }
        if (randomEffect == 1)
        {
            GetComponent<Stats>().canSlow = true;
            GetComponent<Shoot>().bullet = slowball;
        }
        if (randomEffect == 2)
        {
            GetComponent<Stats>().canPoison = true;
            GetComponent<Shoot>().bullet = poisonball;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("RandomEffectCycle");
    }
    public void SetVariation2()
    {
        Debug.Log("Bought Variation3");
        GetComponent<Stats>().canBurn = true;
    }
    public void SetVariationInfoTexts()
    {
        variationInfoTexts[0] = "Damage: " + bigFireball.GetComponent<Bullet>().damage + "\nPiercing: " + bigFireball.GetComponent<Bullet>().piercing + "\nCooldown: " + GetComponent<Stats>().cooldownTime + "s";
        variationInfoTexts[1] = "Attack Speed: " + Mathf.RoundToInt(randomEffectCycleAttackSpeedDebuff * 100) + "%\nDuration: " + GetComponent<Stats>().burnTime + "s";
        variationInfoTexts[2] = "Duration: " + GetComponent<Stats>().burnTime + "s";
    }
}
