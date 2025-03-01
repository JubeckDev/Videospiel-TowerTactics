using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnHeroBanner : MonoBehaviour
{
    public GameObject banner;
    public GameObject bannerSpawned;
    public GameObject cam;

    public Quaternion flagRotOffset;

    public GameObject hero;
    public Vector3 heroPosOffset;

    public GameObject maps;

    // Start is called before the first frame update
    void Start()
    {
        bannerSpawned = Instantiate(banner, maps.GetComponent<Maps>().bannerSpawnPoints[maps.GetComponent<Maps>().mapSelected].position, maps.GetComponent<Maps>().bannerSpawnPoints[maps.GetComponent<Maps>().mapSelected].rotation) as GameObject;
        bannerSpawned.transform.Rotate(0, 90, 0);
        GetComponent<PlaceHero>().SetMatDefault();
        bannerSpawned.GetComponent<StatsBanner>().rangeDisplay.GetComponent<SphereCollider>().enabled = true;
        bannerSpawned.GetComponent<CollisionTestBanner>().shouldTestCollision = false;
        bannerSpawned.GetComponent<CollisionTestBanner>().enabled = false;
        bannerSpawned.GetComponent<CapsuleCollider>().isTrigger = false;
        bannerSpawned.GetComponent<StatsBanner>().UpdateRange(GetComponent<PlaceHero>().hero.GetComponent<StatsHero>().range);
        bannerSpawned.GetComponent<StatsBanner>().rangeDisplay.GetComponent<MeshRenderer>().enabled = false;
        bannerSpawned.GetComponent<CapsuleCollider>().isTrigger = false;
        GetComponent<PlaceHero>().bannerPlaced = bannerSpawned;

        SetHeroPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnNewBanner()
    {
       bannerSpawned = Instantiate(banner, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
       bannerSpawned.transform.Rotate(0, 90, 0);
       bannerSpawned.GetComponent<InitializeMeshesBanner>().enabled = true;
       bannerSpawned.GetComponent<StatsBanner>().rangeDisplay.GetComponent<SendHeroBanner>().canPassEnemy = false;
       GetComponent<PlaceHero>().banner = bannerSpawned;
       GetComponent<PlaceHero>().canPlace = true;
       GetComponent<HeroPanel>().CloseWindow();
        cam.GetComponent<ClickOnObjects>().enabled = false;
    }
    public void SetHeroPosition()
    {
        hero.GetComponent<NavMeshAgent>().enabled = false;
        if(bannerSpawned) hero.transform.position = bannerSpawned.transform.position + heroPosOffset;
        else hero.transform.position = GetComponent<PlaceHero>().bannerPlaced.transform.position + heroPosOffset;
        hero.GetComponent<NavMeshAgent>().enabled = true;
    }
}
