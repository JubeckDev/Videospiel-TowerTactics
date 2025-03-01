using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawned;
    public GameObject[] shootPoints;
    public GameObject[] cannonTops;

    public bool isShooting;

    public int currentActiveSkin;
    public bool canSplit;

    public bool shouldTargetEnemies = true;

    public GameObject muzzleFlash;

    public GameObject shootSound;

    public GameObject projectiles;
    public GameObject particleEffects;

    float maxDistanceIncrease;

    // Start is called before the first frame update
    void Start()
    {
        projectiles = GameObject.Find("Projectiles");
        particleEffects = GameObject.Find("Particles");

        maxDistanceIncrease = 2.2f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GetComponent<Stats>().rangeDisplay.GetComponent<Range>().enemy != null && isShooting == false && shouldTargetEnemies) StartCoroutine("ShootBullet");

    }
    IEnumerator ShootBullet()
    {
        isShooting = true;

        bulletSpawned = Instantiate(bullet, shootPoints[currentActiveSkin].transform.position, cannonTops[currentActiveSkin].transform.rotation, projectiles.transform) as GameObject;

        var bulletScript = bulletSpawned.GetComponent<Bullet>();

        //Apply stats to bullet (with appropriate buffs)
        bulletScript.damage = GetComponent<Stats>().buffActive[0] ? GetComponent<Stats>().damage + Mathf.RoundToInt(GetComponent<Stats>().heroUpgrades.GetComponent<HeroUpgrades>().buffIncrease[0]) : GetComponent<Stats>().damage;
        bulletScript.movementSpeed = GetComponent<Stats>().buffActive[1] ? GetComponent<Stats>().bulletMovementSpeed * GetComponent<Stats>().heroUpgrades.GetComponent<HeroUpgrades>().buffIncrease[1] : GetComponent<Stats>().bulletMovementSpeed;
        bulletScript.piercing = GetComponent<Stats>().buffActive[3] ? GetComponent<Stats>().piercing + Mathf.RoundToInt(GetComponent<Stats>().heroUpgrades.GetComponent<HeroUpgrades>().buffIncrease[3]) : GetComponent<Stats>().piercing;
        bulletScript.maxDistance = GetComponent<Stats>().buffActive[4] ? GetComponent<Stats>().range + maxDistanceIncrease + GetComponent<Stats>().heroUpgrades.GetComponent<HeroUpgrades>().buffIncrease[4] : GetComponent<Stats>().range + maxDistanceIncrease;
        bulletScript.canSlow = GetComponent<Stats>().canSlow;
        bulletScript.slowTime = GetComponent<Stats>().slowTime;
        bulletScript.canBurn = GetComponent<Stats>().canBurn;
        bulletScript.burnTime = GetComponent<Stats>().burnTime;
        bulletScript.canConfuse = GetComponent<Stats>().canConfuse;
        bulletScript.confusionTime = GetComponent<Stats>().confusionTime;
        bulletScript.canPoison = GetComponent<Stats>().canPoison;
        bulletScript.poisonTime = GetComponent<Stats>().poisonTime;
        bulletScript.tower = gameObject;

        if(GetComponent<ArtifactBuff>().projectileBuffIsOn && bulletScript.aura != null) bulletSpawned.GetComponent<Bullet>().aura.SetActive(true);

        if (muzzleFlash)
        {
            Instantiate(muzzleFlash, shootPoints[currentActiveSkin].transform.position, cannonTops[currentActiveSkin].transform.rotation, particleEffects.transform);
        }
        if (shootSound)
        {
            Instantiate(shootSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        }

        yield return new WaitForSeconds(GetComponent<Stats>().buffActive[2] ? GetComponent<Stats>().attackSpeed * GetComponent<Stats>().heroUpgrades.GetComponent<HeroUpgrades>().buffIncrease[2] : GetComponent<Stats>().attackSpeed);

        isShooting = false;

       
    }
}