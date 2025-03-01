using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public List<GameObject> waypoints;
    public GameObject waypointsParent;
    public int nextWaypointIndex = 1;

    public GameObject nextWaypoint;
    public Vector3 waypointOffset = new Vector3(0, 1, 0);

    public float movementSpeed = 1;
    public float pushbackSpeed = 1.5f;

    public float minDistance = 0.05f;

    private GameObject gameUI;
    private GameObject waves;

    public float difference;

    public float slowMultiplier = 0.5f;
    public bool isSlowed;
    public float slowTimeLeft;
    public bool isFrozen;
    public float freezeTimeLeft;
    public bool isConfused;
    public float confusionTimeLeft;
    public bool isPoisoned;
    public float poisonTimeLeft;
    public bool isBurning;
    public float burnTimeLeft;
    public ParticleSystem effectSlow;
    public ParticleSystem effectFreeze;
    public ParticleSystem effectBurn;
    public ParticleSystem effectConfusion;
    public ParticleSystem effectPoison;

    public bool isImmuneToSlow;
    public bool isImmuneToFreeze;
    public bool isImmuneToConfusion;
    public bool isImmuneToPoison;
    public bool isImmuneToBurn;
    public bool isImmuneToPhysical;

    public GameObject slowSound;
    public GameObject freezeSound;
    public GameObject confusionSound;
    public GameObject poisonSound;
    public GameObject burnSound;

    public string towerType;

    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameObject.Find("Canvas/GameUI");
        waves = GameObject.Find("Waves");

        for (int i = 0; i < waves.GetComponent<Waves>().currentWaypointParent.transform.childCount; i++)
        {
            waypoints.Add(waves.GetComponent<Waves>().currentWaypointParent.transform.GetChild(i).gameObject);
        }

        nextWaypoint = waypoints[nextWaypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen) return;

        if (isConfused)
        {
            //move backward
            transform.position -= transform.forward * pushbackSpeed * Time.deltaTime;
            return;
        }
        else
        {
            //move forward
            transform.position += isSlowed ? transform.forward * movementSpeed * Time.deltaTime * slowMultiplier : transform.forward * movementSpeed * Time.deltaTime;
        }


        transform.LookAt(nextWaypoint.transform.position + waypointOffset);

        if (CalculateDistance() <= minDistance)
        {
            if (nextWaypointIndex < waypoints.Count)
            {
                nextWaypoint = waypoints[nextWaypointIndex];

                nextWaypointIndex++;
            }
            else
            {
                //Debug.Log("Dealing Damage " + GetComponent<EnemyLives>().lifeAmounts[GetComponent<EnemyLives>().stage]);
                gameUI.GetComponent<Lives>().ReduceLives(GetComponent<EnemyLives>().lifeAmounts[GetComponent<EnemyLives>().stage]);

                Destroy(gameObject);
            }
        }
    }
    public float CalculateDistance()
    {
        float differenceX;
        float differenceZ;

        differenceX = Mathf.Abs(nextWaypoint.transform.position.x) - Mathf.Abs(transform.position.x);
        differenceZ = Mathf.Abs(nextWaypoint.transform.position.z) - Mathf.Abs(transform.position.z);

        difference = Mathf.Abs(differenceX) + Mathf.Abs(differenceZ);

        return difference;
    }

    public void Slow(float slowTime)
    {
        if (isImmuneToSlow) return;

        if (slowTime >= slowTimeLeft)
        {
            slowTimeLeft = slowTime;
            StopCoroutine("SlowCoroutine");
            StartCoroutine("SlowCoroutine");
        }

        Instantiate(slowSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
    }
    IEnumerator SlowCoroutine()
    {
        isSlowed = true;
        effectSlow.Play();
        while (slowTimeLeft > 0)
        {
            slowTimeLeft--;
            yield return new WaitForSeconds(1);
        }
        isSlowed = false;
        effectSlow.Stop();
    }
    public void Freeze(float freezeTime)
    {
        if (isImmuneToFreeze) return;

        if (freezeTime >= freezeTimeLeft)
        {
            freezeTimeLeft = freezeTime;
            StopCoroutine("FreezeCoroutine");
            StartCoroutine("FreezeCoroutine");
        }

        Instantiate(freezeSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
    }
    IEnumerator FreezeCoroutine()
    {
        isFrozen = true;
        effectFreeze.Play();
        while (freezeTimeLeft > 0)
        {
            freezeTimeLeft--;
            yield return new WaitForSeconds(1);
        }
        isFrozen = false;
        effectFreeze.Stop();
    }
    public void Confuse(float confusionTime)
    {
        if (isImmuneToConfusion) return;

        if (confusionTime >= confusionTimeLeft)
        {
            confusionTimeLeft = confusionTime;
            StopCoroutine("ConfuseCoroutine");
            StartCoroutine("ConfuseCoroutine");
        }

        Instantiate(confusionSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
    }
    IEnumerator ConfuseCoroutine()
    {
        //Debug.Log("Confusion"+ confusionTimeLeft);
        isConfused = true;
        effectConfusion.Play();
        while (confusionTimeLeft > 0)
        {

            if (confusionTimeLeft >= 1)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return new WaitForSeconds(confusionTimeLeft);
            }
            confusionTimeLeft--;
        }
        isConfused = false;
        effectConfusion.Stop();
    }
    public void Burn(float burnTime)
    {
        if (isImmuneToBurn) return;

        if (burnTime >= burnTimeLeft)
        {
            burnTimeLeft = burnTime;
            StopCoroutine("BurnCoroutine");
            StartCoroutine("BurnCoroutine");
        }

        Instantiate(burnSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
    }
    IEnumerator BurnCoroutine()
    {
        isBurning = true;
        effectBurn.Play();
        while (burnTimeLeft > 0)
        {
            burnTimeLeft--;
            yield return new WaitForSeconds(1);
            GetComponent<EnemyLives>().TakeDamage(1, null, null);
        }
        isBurning = false;
        effectBurn.Stop();
    }
    public void Poison(float poisonTime)
    {
        if (isImmuneToPoison) return;

        if (poisonTime >= poisonTimeLeft)
        {
            poisonTimeLeft = poisonTime;
            StopCoroutine("PoisonCoroutine");
            StartCoroutine("PoisonCoroutine");
        }

        Instantiate(poisonSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
    }
    IEnumerator PoisonCoroutine()
    {
        effectPoison.Play();
        while (poisonTimeLeft > 0)
        {
            poisonTimeLeft--;
            yield return new WaitForSeconds(1);
            GetComponent<EnemyLives>().TakeDamage(1, null, null);
        }
        effectPoison.Stop();
    }
}