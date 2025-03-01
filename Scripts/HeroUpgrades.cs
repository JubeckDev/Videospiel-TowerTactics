using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUpgrades : MonoBehaviour
{
    public string[] title;
    public Sprite[] icon;
    public string[] description;
    public GameObject[] upgradeOptionObject;
    public List<int> upgradeNumber = new List<int>();
    public bool[] shouldRemoveFromList;
    public int randomizer;
    public int[] chosenNumbers;
    public int upgradeSelected;
    public GameObject heroSword;
    public GameObject heroSwordParent;
    public float swordSizeMultiplier;
    public GameObject hero;
    public float[] buffIncrease;
    public float[] heroIncrease;

    public float[] buffTimes;
    public float buffCooldown = 20;

    public GameObject heroPanel;
    public GameObject towerPanel;
    public GameObject towerMenu;

    public GameObject abilityButtonBanner;
    public GameObject abilityButtonBannerSpawned;
    public GameObject abilityPanel;

    public GameObject[] cards;

    public GameObject cam;

    public GameObject header;

    public GameObject skillTree;

    public GameObject bannerAbilitySound;

    public GameObject removeObjectPanel;

    public GameObject[] upgradeChosen;
    public Color upgradeChosenColor;
    public Color upgradeNotChosenColor;

    public GameObject speedUpButton;

    public AudioSource claimCard;

    delegate void UpgradeMethod();
    delegate void BuffEffectMethod();

    private void Start()
    {
        ResetHeroUpgrades();
    }

    public void ResetHeroUpgrades()
    // Start is called before the first frame update
    {
        ResetPool();
        //RandomizeUpgrades();

        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[26]) buffTimes[3] += skillTree.GetComponent<SkillTree>().achievementIncreases[26];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[27]) buffTimes[4] += skillTree.GetComponent<SkillTree>().achievementIncreases[27];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[28]) buffTimes[0] += skillTree.GetComponent<SkillTree>().achievementIncreases[28];
        if (skillTree.GetComponent<SkillTree>().achievementUnlocked[30]) buffTimes[2] += skillTree.GetComponent<SkillTree>().achievementIncreases[30];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowCards()
    {
        GetComponent<Image>().enabled = true;
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetActive(true);
        }
        header.SetActive(true);
        cam.GetComponent<Zoom>().canMove = false;

        heroPanel.GetComponent<HeroPanel>().CloseWindow();
        towerPanel.GetComponent<TowerPanel>().CloseWindow();
        removeObjectPanel.GetComponent<RemoveObjectPanel>().CloseWindow();
        towerMenu.GetComponent<PlaceTower>().CancelPlacingTower();

        if (heroPanel.GetComponent<PlaceHero>().banner)
        {
            Destroy(heroPanel.GetComponent<PlaceHero>().banner);
            cam.GetComponent<ClickOnObjects>().canClick = true;

        }

        RandomizeUpgrades();
        Time.timeScale = 0;
    }
    public void HideCards()
    {
        GetComponent<Image>().enabled = false;
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetActive(false);
        }
        header.SetActive(false);
        cam.GetComponent<Zoom>().canMove = true;
        Time.timeScale = speedUpButton.GetComponent<SpeedUpButton>().isNormalSpeed ? 1 : speedUpButton.GetComponent<SpeedUpButton>().speedUpValue;
    }
    public void DisplayUpgrades(int upgradeOption, int upgradeNumber)
    {
        upgradeOptionObject[upgradeOption].GetComponent<HeroUpgradeOption>().title.text  = title[upgradeNumber];
        upgradeOptionObject[upgradeOption].GetComponent<HeroUpgradeOption>().icon.sprite  = icon[upgradeNumber];
        upgradeOptionObject[upgradeOption].GetComponent<HeroUpgradeOption>().abilityIcon.SetActive(1 <= upgradeNumber && upgradeNumber <= 5);
        upgradeOptionObject[upgradeOption].GetComponent<HeroUpgradeOption>().description.text = description[upgradeNumber];
        upgradeOptionObject[upgradeOption].GetComponent<HeroUpgradeOption>().upgradeNumber = upgradeNumber;
    }
    public void RandomizeUpgrades()
    {
        chosenNumbers[0] = -2;
        chosenNumbers[1] = -2;
        chosenNumbers[2] = -2;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                randomizer = Random.Range(0, upgradeNumber.Count);
            }
            while (upgradeNumber[randomizer] == chosenNumbers[0] || upgradeNumber[randomizer] == chosenNumbers[1]); //Repeat randomizing until the itÄs a number that hasnÄt been chosen yet
            //Debug.Log("Randomizer:" + randomizer);
            //Debug.Log("UpgradeNumber Randomizer:" + upgradeNumber[randomizer]);
            DisplayUpgrades(i, upgradeNumber[randomizer]);
            chosenNumbers[i] = upgradeNumber[randomizer];
        }
    }

    public void ResetPool()
    {
        upgradeNumber = new List<int>();

        for (int i = 0; i<title.Length; i++)
        {
            upgradeNumber.Add(i);
        }

        for (int i = 0; i < upgradeChosen.Length; i++)
        {
            upgradeChosen[i].GetComponent<Image>().sprite = null;
            upgradeChosen[i].GetComponent<Image>().color = upgradeNotChosenColor;
            upgradeChosen[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void SelectUpgrade(int upgradeOption)
    {
        HideCards();

        //set upgrade selected to the number of the selected card
        upgradeSelected = cards[upgradeOption].GetComponent<HeroUpgradeOption>().upgradeNumber;

        //upgradeSelected = chosenNumbers[upgradeOption];
        //Debug.Log("UpgradeOption: " + upgradeOption);
        //Debug.Log("UpgradeSelected: " + upgradeSelected);
        //check if selected upgrade should be removed
        if (shouldRemoveFromList[upgradeSelected])
        {
            //Remove the slot item from the list where it is equal to the selected upgrade
            upgradeNumber.RemoveAt(upgradeNumber.IndexOf(upgradeSelected));
        }
        List<UpgradeMethod> upgrade = new List<UpgradeMethod>();
        upgrade.Add(HeavyHit);
        upgrade.Add(DamageBuff);
        upgrade.Add(BulletSpeedBuff);
        upgrade.Add(AttackSpeedBuff);
        upgrade.Add(PiercingBuff);
        upgrade.Add(RangeBuff);
        upgrade.Add(HeroDamage);
        upgrade.Add(HeroSpeed);
        upgrade.Add(HeroAttackSpeedy);
        upgrade.Add(BannerRange);
        upgrade.Add(HeroSwordSize);
        upgrade[upgradeSelected]();

        //set the corresponding sprite in hero panel
        upgradeChosen[hero.GetComponent<StatsHero>().level - 2].GetComponent<Image>().sprite = icon[upgradeSelected];
        upgradeChosen[hero.GetComponent<StatsHero>().level - 2].GetComponent<Image>().color = upgradeChosenColor;
        upgradeChosen[hero.GetComponent<StatsHero>().level - 2].transform.GetChild(0).gameObject.SetActive(false);

        claimCard.Play();
    }
    public void HeavyHit()
    {
        heroSword.GetComponent<SwordCollider>().heavySwordActive = true;
        heroSword.GetComponent<MeshRenderer>().material = heroSword.GetComponent<SwordCollider>().swordGlow;
    }
    public void DamageBuff()
    {
        SpawnBuffButton();
        abilityButtonBannerSpawned.GetComponent<AbilityButtonBanner>().buffSelected = 0;
        abilityButtonBannerSpawned.GetComponent<Image>().sprite = icon[1];
        abilityButtonBannerSpawned.transform.localScale = new Vector3(1, 1, 1);
    }
    public void BulletSpeedBuff()
    {
        SpawnBuffButton();
        abilityButtonBannerSpawned.GetComponent<AbilityButtonBanner>().buffSelected = 1;
        abilityButtonBannerSpawned.GetComponent<Image>().sprite = icon[2];
        abilityButtonBannerSpawned.transform.localScale = new Vector3(1, 1, 1);
    }
    public void AttackSpeedBuff()
    {
        SpawnBuffButton();
        abilityButtonBannerSpawned.GetComponent<AbilityButtonBanner>().buffSelected = 2;
        abilityButtonBannerSpawned.GetComponent<Image>().sprite = icon[3];
        abilityButtonBannerSpawned.transform.localScale = new Vector3(1, 1, 1);
    }
    public void PiercingBuff()
    {
        SpawnBuffButton();
        abilityButtonBannerSpawned.GetComponent<AbilityButtonBanner>().buffSelected = 3;
        abilityButtonBannerSpawned.GetComponent<Image>().sprite = icon[4];
        abilityButtonBannerSpawned.transform.localScale = new Vector3(1, 1, 1);
    }
    public void RangeBuff()
    {
        SpawnBuffButton();
        abilityButtonBannerSpawned.GetComponent<AbilityButtonBanner>().buffSelected = 4;
        abilityButtonBannerSpawned.GetComponent<Image>().sprite = icon[5];
        abilityButtonBannerSpawned.transform.localScale = new Vector3(1, 1, 1);
    }
    public void HeroDamage()
    {
        hero.GetComponent<StatsHero>().IncreaseDamage(Mathf.RoundToInt(heroIncrease[0]));
    }
    public void HeroSpeed()
    {
        hero.GetComponent<StatsHero>().IncreaseSpeed(heroIncrease[1]);
    }
    public void HeroAttackSpeedy()
    {
        hero.GetComponent<StatsHero>().IncreaseAttackSpeed(heroIncrease[2]);
    }
    public void BannerRange()
    {
        hero.GetComponent<StatsHero>().IncreaseRange(heroIncrease[3]);
    }
    public void HeroSwordSize()
    {
        heroSwordParent.transform.localScale = new Vector3(heroSwordParent.transform.localScale.x * swordSizeMultiplier, heroSwordParent.transform.localScale.y * swordSizeMultiplier, heroSwordParent.transform.localScale.z * swordSizeMultiplier);
    }
    public void SpawnBuffButton()
    {
        abilityButtonBannerSpawned = Instantiate(abilityButtonBanner, abilityPanel.transform.position, abilityPanel.transform.rotation) as GameObject;
        abilityButtonBannerSpawned.transform.SetParent(abilityPanel.transform);
    }
    public void TriggerBuffEffect(int buffSelected)
    {
        List<BuffEffectMethod> buffEffect = new List<BuffEffectMethod>();
        buffEffect.Add(DamageBuffEffect);
        buffEffect.Add(BulletSpeedBuffEffect);
        buffEffect.Add(AttackSpeedBuffEffect);
        buffEffect.Add(PiercingBuffBuffEffect);
        buffEffect.Add(RangeBuffEffect);
        buffEffect[buffSelected]();

        Instantiate(bannerAbilitySound, heroPanel.GetComponent<PlaceHero>().bannerPlaced.transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);   
    }
    public void DamageBuffEffect()
    {
        StartCoroutine("DamageBuffTimer");
    }
    public void BulletSpeedBuffEffect()
    {
        StartCoroutine("BulletSpeedBuffTimer");
    }
    public void AttackSpeedBuffEffect()
    {
        StartCoroutine("AttackSpeedBuffTimer");
    }
    public void PiercingBuffBuffEffect()
    {
        StartCoroutine("PiercingBuffBuffTimer");
    }
    public void RangeBuffEffect()
    {
        StartCoroutine("RangeBuffTimer");
    }
    IEnumerator DamageBuffTimer()
    {
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().ActivateDamageBuff();
        yield return new WaitForSeconds(buffTimes[0]);
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().DeactivateDamageBuff();
    }
    IEnumerator BulletSpeedBuffTimer()
    {
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().ActivateBulletSpeedBuff();
        yield return new WaitForSeconds(buffTimes[1]);
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().DeactivateBulletSpeedBuff();
    }
    IEnumerator AttackSpeedBuffTimer()
    {
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().ActivateAttackSpeedBuff();
        yield return new WaitForSeconds(buffTimes[2]);
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().DeactivateAttackSpeedBuff();
    }
    IEnumerator PiercingBuffBuffTimer()
    {
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().ActivatePiercingBuff();
        yield return new WaitForSeconds(buffTimes[3]);
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().DeactivatePiercingBuff();
    }
    IEnumerator RangeBuffTimer()
    {
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().ActivateRangeBuff();
        yield return new WaitForSeconds(buffTimes[4]);
        heroPanel.GetComponent<PlaceHero>().bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().DeactivateRangeBuff();
    }
}
