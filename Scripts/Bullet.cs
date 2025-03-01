using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementSpeed = 3;
    public float maxDistance = 5;
    public int damage = 5;
    public int piercing = 1;


    public Vector3 startingCords;

    public bool canSplit;
    public bool canSplitTwice;
    public GameObject splitBullet;
    public GameObject splitBulletSpawned;
    public Vector3[] rotationDifferences;

    public bool canCollide = true;

    public bool canSlow;
    public float slowTime;
    public bool canFreeze;
    public float freezeTime;
    public bool canBurn;
    public float burnTime;
    public bool canConfuse;
    public float confusionTime;
    public bool canPoison;
    public float poisonTime;
    public bool canSplash;
    public GameObject splashObject;
    public GameObject splashObjectSpawned;
    public float puddleLifetime;
    public float puddleSize;

    public string damageType;

    public GameObject impactSound;

    public string towerType;
    public GameObject tower;

    public GameObject aura;

    // Start is called before the first frame update
    void Start()
    {
        startingCords = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, startingCords) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!canCollide) return;
      
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyLives>().TakeDamage(damage, gameObject, towerType);
            piercing--;

            if (impactSound) Instantiate(impactSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);

            if (canSlow == true)
            {
                other.GetComponent<Movement>().Slow(slowTime);
            }
            if (canFreeze == true)
            {
                other.GetComponent<Movement>().Freeze(freezeTime);
            }
            if (canBurn == true)
            {
                other.GetComponent<Movement>().Burn(burnTime);
            }
            if (canConfuse == true)
            {
                other.GetComponent<Movement>().Confuse(confusionTime);
            }
            if (canPoison == true)
            {
                other.GetComponent<Movement>().Poison(poisonTime);
            }
            if (piercing == 0)
            {
                if (canSplit || canSplitTwice)
                {
                    for(int i = 0; i < rotationDifferences.Length; i++)
                    {
                        splitBulletSpawned = Instantiate(splitBullet, transform.position, transform.rotation) as GameObject;
                        splitBulletSpawned.transform.parent = transform.parent;
                        splitBulletSpawned.transform.eulerAngles += rotationDifferences[i];
                        splitBulletSpawned.GetComponent<Bullet>().damage = damage;
                        splitBulletSpawned.GetComponent<Bullet>().piercing = piercing;
                        splitBulletSpawned.GetComponent<Bullet>().maxDistance = maxDistance;
                        splitBulletSpawned.GetComponent<Bullet>().movementSpeed = movementSpeed;
                        splitBulletSpawned.GetComponent<Bullet>().tower = tower;
                        if (aura.activeSelf) splitBulletSpawned.GetComponent<Bullet>().aura.SetActive(true);
                        if (canSplitTwice)
                        {
                            splitBulletSpawned.GetComponent<Bullet>().piercing = 2;
                        }
                    }
                }


                Destroy(gameObject);
            }
        }
        else if (other.tag == "Ground" || other.tag == "Obstacle")
        {
            if (canSplash)
            {
                splashObjectSpawned = Instantiate(splashObject, transform.position, transform.parent.rotation) as GameObject;
                //splashObjectSpawned.transform.eulerAngles = new Vector3(0, 0, 0);
                splashObjectSpawned.transform.parent = transform.parent;
                splashObjectSpawned.transform.localScale = new Vector3(puddleSize, puddleSize, puddleSize);
                splashObjectSpawned.GetComponent<Lifetime>().lifetime = puddleLifetime;
            }

            if (impactSound) Instantiate(impactSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);

            Destroy(gameObject);
        }
    }
}