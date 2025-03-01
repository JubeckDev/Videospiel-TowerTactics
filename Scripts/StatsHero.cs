using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class StatsHero : MonoBehaviour

{
    public int defaultDamage = 1;
    public float defaultMovementSpeed = 3;
    public float defaultAttackSpeed = 1;
    public float defaultRange = 15;

    public int damage;
    public float movementSpeed;
    public float attackSpeed;
    public float range;
    public GameObject sword;
    public Animator anim;
    public GameObject heroPanel;

    public int level;
    public int xp;
    public int[] xpRemaining;

    public GameObject heroUpgrades;

    public bool maxLevelReached;
    public int maxLevel = 10;

    public TextMeshProUGUI heroLevelText;
    public Slider heroLevelSlider;

    public GameObject achievements;

    public GameObject skillTree;

    public GameObject towerMenu;

    public GameObject heroLevelUp;

    public bool hasArtifact;
    private int towerNumber;
    public GameObject artifactPanel;

    public string towerType;

    private void Start()
    {
        ResetHeroStats();
    }

    // Start is called before the first frame update
    public void ResetHeroStats()
    {
        level = 1;
        xp = 0;
        UpdateHeroLevelSlider();
        UpdateHeroLevelText();

        damage = defaultDamage;
        movementSpeed = defaultMovementSpeed;
        attackSpeed = defaultAttackSpeed;
        range = defaultRange;

        sword.GetComponent<SwordCollider>().damage = damage;
        GetComponent<NavMeshAgent>().speed = movementSpeed;
        anim.SetFloat("AttackSpeedMultiplier", attackSpeed);
        if (heroPanel.GetComponent<PlaceHero>().banner) heroPanel.GetComponent<PlaceHero>().banner.GetComponent<StatsBanner>().UpdateRange(range);
        UpdateHeroLevelText();

        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[19]) IncreaseRange(skillTree.GetComponent<SkillTree>().achievementIncreases[19]);
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[21]) sword.GetComponent<SwordCollider>().damageMultiplierCooldown += skillTree.GetComponent<SkillTree>().achievementIncreases[21];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[22]) movementSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[22];
        UpdateMovementSpeed();
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[23]) attackSpeed += skillTree.GetComponent<SkillTree>().achievementIncreases[23];

        //Check if the tower has an artifact
        //artifactPanel = GameObject.Find("Canvas/GameUI/ArtifactPanel");
        towerNumber = System.Array.IndexOf(artifactPanel.GetComponent<ArtifactPanel>().towerNames, towerType);
    }
    public void EarnXP()
    {
        if (!maxLevelReached)
        {
            xp++;
            UpdateHeroLevelSlider();
            if (xp >= xpRemaining[level - 1])
            {
                xp = 0;
                level++;
                Instantiate(heroLevelUp, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
                UpdateHeroLevelText();
                towerMenu.GetComponent<PlaceTower>().CancelPlacingTower();
                heroUpgrades.GetComponent<HeroUpgrades>().ShowCards();
                if (level >= maxLevel) //max level reached
                {
                    maxLevelReached = true;
                    heroLevelSlider.gameObject.SetActive(false);
                }
                achievements.GetComponent<Achievements>().SetNewProgress(2, level);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (artifactPanel.GetComponent<ArtifactPanel>().hasArtifact[8] && !hasArtifact)
        {
            hasArtifact = true;
            GetComponent<ArtifactBuffHero>().ActivateBuff(true);
        }
        else if (!artifactPanel.GetComponent<ArtifactPanel>().hasArtifact[8] && hasArtifact)
        {
            hasArtifact = false;
            GetComponent<ArtifactBuffHero>().ActivateBuff(false);
        }
    }
    public void IncreaseDamage(int damageIncrease)
    {
        damage += damageIncrease;
        sword.GetComponent<SwordCollider>().damage = damage;
    }
    public void IncreaseSpeed(float speedIncrease)
    {
        movementSpeed *= speedIncrease;
        UpdateMovementSpeed();
    }
    public void UpdateMovementSpeed()
    {
        GetComponent<NavMeshAgent>().speed = movementSpeed;
    }
    public void IncreaseAttackSpeed(float attackSpeedIncrease)
    {
        attackSpeed *= attackSpeedIncrease;
        anim.SetFloat("AttackSpeedMultiplier", attackSpeed);
    }
    public void IncreaseRange(float rangeIncrease)
    {
        range += rangeIncrease;
        if (heroPanel.GetComponent<PlaceHero>().bannerPlaced) heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().UpdateRange(range);
    }
    public void UpdateHeroLevelText()
    {
        heroLevelText.text = "Hero level: " + level;
    }
    public void UpdateHeroLevelSlider()
    {
        heroLevelSlider.maxValue = xpRemaining[level];
        heroLevelSlider.value = xp;
    }
}
