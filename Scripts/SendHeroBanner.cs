using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendHeroBanner : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject enemy;

    public GameObject hero;

    public bool canPassEnemy = true;

   // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        RemoveDestroyedEnemies();

        enemy = GetClosestEnemy();

        if(canPassEnemy) hero.GetComponent<HeroMovement>().enemy = enemy;
    }
    public void RemoveDestroyedEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            //Debug.Log("Going through list");
            if (enemy == null)
            {
                //Debug.Log("Removed enemy from list");
                enemyList.Remove(enemy);
                RemoveDestroyedEnemies();
                break;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!enemyList.Contains(other.gameObject)) enemyList.Add(other.gameObject);

        }
        if(other.name == "Hero")
        {
            hero.GetComponent<HeroMovement>().heroIsInBannerRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
        if (other.name == "Hero")
        {
            hero.GetComponent<HeroMovement>().heroIsInBannerRange = false;
        }
    }
    public GameObject GetClosestEnemy()
    {
        GameObject enemyClosest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentHeroPos = hero.transform.position;
        foreach (GameObject tempEnemy in enemyList)
        {
            float dist = Vector3.Distance(tempEnemy.transform.position, currentHeroPos);
            if (dist < minDist)
            {
                enemyClosest = tempEnemy;
                minDist = dist;
            }
        }
        return enemyClosest;
    }
}


