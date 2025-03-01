using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariationEffectsCannon : MonoBehaviour
{
    public GameObject cannonBulletVariation1;
    public GameObject cannonBulletVariation2;
    public float attackSpeedMultiplierVariation1;
    public float bulletSpeedMultiplierVariation1;
    public int damageChangeVariation1;
    public float offsetChangeVariation1;
    public float offsetChangeVariation0;

    public GameObject abilityPanel;

    public List<Transform> waypoints = new List<Transform>();

    public List<Transform> pathMaps = new List<Transform>();
    public GameObject[] wayPointParents;

    public Transform closestWaypoint = null;

    public float abilityTime;

    public GameObject timeBomb;
    public GameObject timeBombSpawned;

    public float explosionDelay = 5;
    public float explosionRadius = 1;
    public float impactEffectRadius = 1;
    public int bounces = 4;
    public float splitBulletSize = 1;
    public float splitBulletParentSize = 0.3f;
    public float attackSpeedMultiplierVariation2;

    public GameObject skillTree;

    public GameObject projectiles;

    public string[] variationInfoTexts = new string[3];

    void Start()
    {
        projectiles = GameObject.Find("Projectiles");
        abilityPanel = GameObject.Find("Canvas/GameUI/AbilityPanel");
        wayPointParents[0] = GameObject.Find("PathMaps/PathMap1/Waypoints");
        wayPointParents[1] = GameObject.Find("PathMaps/PathMap2/Waypoints");
        wayPointParents[2] = GameObject.Find("PathMaps/PathMap3/Waypoints");
        wayPointParents[3] = GameObject.Find("PathMaps/PathMap4/Waypoints");
        wayPointParents[4] = GameObject.Find("PathMaps/PathMap5/Waypoints");
        wayPointParents[5] = GameObject.Find("PathMaps/PathMap6/Waypoints");
        wayPointParents[6] = GameObject.Find("PathMaps/PathMap7/Waypoints");
        wayPointParents[7] = GameObject.Find("PathMaps/PathMap8/Waypoints");
        wayPointParents[8] = GameObject.Find("PathMaps/PathMap9/Waypoints");

        skillTree = GameObject.Find("Canvas/Menu/SkillTree");
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[5]) GetComponent<Stats>().range += skillTree.GetComponent<SkillTree>().achievementIncreases[5];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[6]) GetComponent<Stats>().attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[6];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[7]) GetComponent<Stats>().bulletMovementSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[7];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[27]) GetComponent<Enhancements>().goldCosts[3] += Mathf.RoundToInt(skillTree.GetComponent<SkillTree>().achievementIncreases[27]);

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

        for (int i = 0; i < wayPointParents.Length; i++)
        {
            foreach (Transform child in wayPointParents[i].transform)
            { waypoints.Add(child.transform); }
        }

        GetClosestWaypoint();
    }

    public void GetClosestWaypoint()
    {
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in waypoints)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                closestWaypoint = t;
                minDist = dist;
            }
        }
        
    }

    public void TriggerAbility()
    {
        StartCoroutine("TriggerAbilityCoroutine");
    }
    IEnumerator TriggerAbilityCoroutine()
    {
        GetComponent<Stats>().rangeDisplay.GetComponent<Range>().FocusOnNewTarget(closestWaypoint);
        for (int i = 0; i < GetComponent<Shoot>().cannonTops.Length; i++)
        {
            GetComponent<Shoot>().cannonTops[i].GetComponent<LookAtEnemy>().offsetRotation = offsetChangeVariation0;
        }
        yield return new WaitForSeconds(abilityTime / 2);
        timeBombSpawned = Instantiate(timeBomb, GetComponent<Shoot>().shootPoints[GetComponent<Shoot>().currentActiveSkin].transform.position, GetComponent<Shoot>().cannonTops[GetComponent<Shoot>().currentActiveSkin].transform.rotation, projectiles.transform) as GameObject;
        timeBombSpawned.GetComponent<TimeBomb>().tower = gameObject;

        yield return new WaitForSeconds(abilityTime / 2);
        GetComponent<Stats>().rangeDisplay.GetComponent<Range>().FocusBackOnEnemies();
        for (int i = 0; i < GetComponent<Shoot>().cannonTops.Length; i++)
        {
            GetComponent<Shoot>().cannonTops[i].GetComponent<LookAtEnemy>().offsetRotation = 0;
        }
    }

    public void SetVariation1()
    {
        Debug.Log("Bought Variation2");
        GetComponent<Shoot>().bullet = cannonBulletVariation1;
        GetComponent<Stats>().attackSpeed *= attackSpeedMultiplierVariation1;
        GetComponent<Stats>().bulletMovementSpeed *= bulletSpeedMultiplierVariation1;
        GetComponent<Stats>().damage += damageChangeVariation1;

        for (int i = 0; i < GetComponent<Shoot>().cannonTops.Length; i++)
        {
            GetComponent<Shoot>().cannonTops[i].GetComponent<LookAtEnemy>().offsetRotation = offsetChangeVariation1;
        }

        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[4]) GetComponent<Stats>().damage += Mathf.RoundToInt(skillTree.GetComponent<SkillTree>().achievementIncreases[4]);
    }
    public void SetVariation2()
    {
        Debug.Log("Bought Variation3");
        GetComponent<Shoot>().bullet = cannonBulletVariation2;
        GetComponent<Shoot>().canSplit = true;
        GetComponent<Stats>().attackSpeed *= attackSpeedMultiplierVariation2;
    }
    public void SetVariationInfoTexts()
    {
        variationInfoTexts[0] = "Damage: " + timeBomb.GetComponent<TimeBomb>().timeBombExplosion.GetComponent<ImpactEffect>().damage + "\nCooldown Time: " + GetComponent<Stats>().cooldownTime + "s";
        variationInfoTexts[1] = "Damage: +" + damageChangeVariation1 + "\nAttack Speed: " + Mathf.RoundToInt(1 / attackSpeedMultiplierVariation1 * 100) + "%\n Bounces: " + bounces;
        variationInfoTexts[2] = "Attack Speed: " + Mathf.RoundToInt(1 / attackSpeedMultiplierVariation2 * 100) + "%\nSplit Bullets: 3";
    }
}