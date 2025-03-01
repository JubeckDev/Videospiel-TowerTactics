using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroMovement : MonoBehaviour
{
    public GameObject enemy;
    public Animator anim;
    public NavMeshAgent agent;
    public float additionalStoppingDistance = 0.1f;
    public float remainingDistance;
    public Vector3 destination;

    private GameObject target;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

    public float rotationSlerpMultiplier;

    public float swordColliderDelay = 0.4f;
    public float swordColliderTime = 0.5f;

    public bool swordColliderCoroutineIsRunning;

    public GameObject heroAutoAttack;

    public bool heroIsInBannerRange;

    public GameObject heroPanel;

    // Start is called before the first frame update
    void Start()
    {

    }
// Update is called once per frame
    void Update()
    {
        destination= agent.destination;
        remainingDistance = agent.remainingDistance;
        if (enemy && agent.remainingDistance < Mathf.Infinity)
        {
            agent.SetDestination(enemy.transform.position);
            if(Vector3.Distance(transform.position, enemy.transform.position) <= agent.stoppingDistance+ additionalStoppingDistance && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
            {
                //Debug.Log("Attack");
                anim.Play("Attacking");
                //StartCoroutine("SwordColliderCoroutine");
                Instantiate(heroAutoAttack, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);

            }
            else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Running"))
            {
                //Debug.Log("Running");
                anim.Play("Running");
            }
        }
        else
        {
            if (heroIsInBannerRange)
            {
                //Debug.Log("NoDistance");
                agent.SetDestination(transform.position);
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    anim.Play("Idle");
                }
            }
            else
            {
                agent.SetDestination(heroPanel.GetComponent<SpawnHeroBanner>().bannerSpawned.transform.position);
                anim.Play("Running");
            }
        }


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            agent.speed = 0;
            if(!swordColliderCoroutineIsRunning) StartCoroutine("SwordColliderCoroutine");
            if (enemy)
            {
                targetPoint = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z) - transform.position;
                targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSlerpMultiplier);
                //transform.LookAt(enemy.transform.position);
            }
        }
        else
        {
            agent.speed = GetComponent<StatsHero>().movementSpeed;
            GetComponent<StatsHero>().sword.GetComponent<BoxCollider>().enabled = false;
        }
    }
    IEnumerator SwordColliderCoroutine()
    {
        swordColliderCoroutineIsRunning = true;
        //Debug.Log("EnableCol");
        GetComponent<StatsHero>().sword.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1.5f / GetComponent<StatsHero>().attackSpeed);
        GetComponent<StatsHero>().sword.GetComponent<BoxCollider>().enabled = false;
        //Debug.Log("DisableCol");
        swordColliderCoroutineIsRunning = false;
    }
    public void ResetSwordCoroutine()
    {
        StopCoroutine("SwordColliderCoroutine");
        GetComponent<StatsHero>().sword.GetComponent<BoxCollider>().enabled = false;
        swordColliderCoroutineIsRunning = false;
    }
}