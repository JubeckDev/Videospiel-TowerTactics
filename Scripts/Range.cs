using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject enemy;

    public GameObject[] cannonTops;

    public bool shouldTargetEnemies = true;

    public int currentActiveSkin;

    public int targetMethod;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        RemoveDestroyedEnemies();

        if (shouldTargetEnemies)
        {
            switch(targetMethod)
            {
                case 3:
                    enemy = GetLastEnemy();
                    break;
                case 2:
                    enemy = GetFirstEnemy();
                    break;
                case 1:
                    enemy = GetStrongestEnemy();
                    break;
                default:
                    enemy = GetClosestEnemy();
                    break;
            }
        }


        for (int i = 0; i < cannonTops.Length; i++)
        {
            cannonTops[i].GetComponent<LookAtEnemy>().AssignEnemy(enemy);
        }

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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }
    public GameObject GetClosestEnemy()
    {
        GameObject enemyClosest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject tempEnemy in enemyList)
        {
            float dist = Vector3.Distance(tempEnemy.transform.position, currentPos);
            if (dist < minDist)
            {
                enemyClosest = tempEnemy;
                minDist = dist;
            }
        }
        return enemyClosest;
    }
    public GameObject GetStrongestEnemy()
    {
        GameObject enemyStrongest = null;
        int minStage = -1;

        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject tempEnemy in enemyList)
        {
            int stage = tempEnemy.GetComponent<EnemyLives>().stage;
            float dist = Vector3.Distance(tempEnemy.transform.position, currentPos);
            if (stage > minStage || (stage == minStage && dist < minDist))
            {
                enemyStrongest = tempEnemy;
                minStage = stage;
                minDist = dist;
            }
        }
        return enemyStrongest;
    }
    public GameObject GetFirstEnemy()
    {
        GameObject enemyFirst = null;
        int minNextWaypointIndex = -1;
        float minDistToNextWaypoint = Mathf.Infinity;
        foreach (GameObject tempEnemy in enemyList)
        {
            int nextWaypointIndex = tempEnemy.GetComponent<Movement>().nextWaypointIndex;
            float distToNextWaypoint = tempEnemy.GetComponent<Movement>().difference;
            if (nextWaypointIndex > minNextWaypointIndex || (nextWaypointIndex == minNextWaypointIndex) && distToNextWaypoint < minDistToNextWaypoint)
            {
                enemyFirst = tempEnemy;
                minNextWaypointIndex = nextWaypointIndex;
                minDistToNextWaypoint = distToNextWaypoint;
            }
        }
        return enemyFirst;
    }
    public GameObject GetLastEnemy()
    {
        GameObject enemyLast = null;
        int minNextWaypointIndex = 1000;
        float minDistToNextWaypoint = -1;
        foreach (GameObject tempEnemy in enemyList)
        {
            int nextWaypointIndex = tempEnemy.GetComponent<Movement>().nextWaypointIndex;
            float distToNextWaypoint = tempEnemy.GetComponent<Movement>().difference;
            if (nextWaypointIndex < minNextWaypointIndex || (nextWaypointIndex == minNextWaypointIndex) && distToNextWaypoint > minDistToNextWaypoint)
            {
                enemyLast = tempEnemy;
                minNextWaypointIndex = nextWaypointIndex;
                minDistToNextWaypoint = distToNextWaypoint;
            }
        }
        return enemyLast;
    }
    public void FocusOnNewTarget(Transform newTarget)
    {
        shouldTargetEnemies = false;
        transform.GetComponentInParent<Shoot>().shouldTargetEnemies = false;
        enemy = newTarget.gameObject;
        cannonTops[currentActiveSkin].GetComponent<LookAtEnemy>().AssignEnemy(enemy);
    }
    public void FocusBackOnEnemies()
    {
        shouldTargetEnemies = true;
        transform.GetComponentInParent<Shoot>().shouldTargetEnemies = true;
        enemy = null;
    }
}
