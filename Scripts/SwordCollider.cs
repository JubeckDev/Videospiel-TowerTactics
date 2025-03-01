using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public GameObject hero;
    public int damage;
    public bool extraDamage = true;
    public int damageMultiplier = 3;
    public float damageMultiplierCooldown = 5;
    public bool heavySwordActive;

    public Material swordDefault;
    public Material swordGlow;

    public bool hasAlreadyHitBallon;

    public string towerType = "Hero";

    public bool canOnlyDamageOneEnemyPerHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(heavySwordActive && extraDamage)
            {
                other.GetComponent<EnemyLives>().TakeDamage(damage* damageMultiplier, gameObject, towerType);
                StartCoroutine("DamageMultiplierCooldown");
            }
            else
            {
                other.GetComponent<EnemyLives>().TakeDamage(damage, gameObject, towerType);
            }
            if (!canOnlyDamageOneEnemyPerHit)
            {
                hasAlreadyHitBallon = true;
                GetComponent<BoxCollider>().enabled = false;
            }
            //Debug.Log("DisableColInSword");

            if (!hero.GetComponent<StatsHero>().maxLevelReached) hero.GetComponent<StatsHero>().EarnXP();
        }
    }
    IEnumerator DamageMultiplierCooldown()
    {
        extraDamage = false;
        GetComponent<MeshRenderer>().material = swordDefault;
        yield return new WaitForSeconds(damageMultiplierCooldown);
        extraDamage = true;
        GetComponent<MeshRenderer>().material = swordGlow;
    }
}