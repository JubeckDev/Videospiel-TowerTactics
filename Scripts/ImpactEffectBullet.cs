using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffectBullet : MonoBehaviour
{
    public int initialBounces;
    public int bouncesLeft;
    public GameObject impactEffect;
    public GameObject impactEffectSpawned;
    public float impactEffectRadius = 1;

    public GameObject bounceBallSound;

    // Start is called before the first frame update
    void Start()
    {
        initialBounces = GetComponent<Bullet>().tower.GetComponent<VariationEffectsCannon>().bounces; 
        bouncesLeft = initialBounces;

        impactEffectRadius = GetComponent<Bullet>().tower.GetComponent<VariationEffectsCannon>().impactEffectRadius;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {     
        bouncesLeft--;

        //Spawn particle system
        impactEffectSpawned = Instantiate(impactEffect, transform.position, transform.rotation) as GameObject;
        impactEffectSpawned.GetComponent<ImpactEffect>().damage = GetComponent<Bullet>().damage;
        impactEffectSpawned.transform.localScale = new Vector3(impactEffectRadius, impactEffectRadius, impactEffectRadius);

        if (bouncesLeft <= 0)
        {
            Destroy(gameObject);
        }

        Instantiate(bounceBallSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
    }



}
