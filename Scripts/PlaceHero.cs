using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHero : MonoBehaviour
{
    public Camera mainCam;
    public GameObject banner;
    public GameObject bannerPlaced;
    public LayerMask targetLayer;

    public bool canPlace;

    public GameObject meshDefault;
    public GameObject meshCanPlace;
    public GameObject meshCantPlace;

    public GameObject rangeVisualizer;

    public GameObject cam;
    public GameObject hero;

    public GameObject placeTowerSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (banner)
        {
            //Debug.Log("bannerexist");
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, targetLayer))
            {
                //Debug.Log("RaycastHit");
                banner.transform.position = raycastHit.point;
            }
            if (canPlace)
            {
                if (meshCanPlace) SetMatCanPlace();
            }
            if (!canPlace)
            {
                if (meshCanPlace) SetMatCantPlace();
            }
        }
        if (banner && canPlace && Input.GetMouseButtonDown(0)) //Place Tower
        {
            hero.GetComponent<HeroMovement>().heroIsInBannerRange = false;

            //Debug.Log("Placed Banner");
            SetMatDefault();
            banner.GetComponent<StatsBanner>().rangeDisplay.GetComponent<SphereCollider>().enabled = true;
            banner.GetComponent<CollisionTestBanner>().shouldTestCollision = false;
            banner.GetComponent<CollisionTestBanner>().enabled = false;
            banner.GetComponent<CapsuleCollider>().isTrigger = false;
            banner.GetComponent<StatsBanner>().UpdateRange(hero.GetComponent<StatsHero>().range);
            banner.GetComponent<StatsBanner>().rangeDisplay.GetComponent<SendHeroBanner>().canPassEnemy = true;
            // banner.GetComponent<BannerClick>().enabled = true;

            if (rangeVisualizer) rangeVisualizer.GetComponent<MeshRenderer>().enabled = false;
            rangeVisualizer = null;
            if (bannerPlaced)
            {
                Destroy(bannerPlaced);
            }

            bannerPlaced.GetComponent<StatsBanner>().rangeDisplay.GetComponent<TowersInRange>().DeactivateRangeBuff();

            bannerPlaced = banner;
            banner = null;
            meshDefault = null;
            meshCanPlace = null;
            meshCantPlace = null;

            cam.GetComponent<ClickOnObjects>().enabled = true;

            Instantiate(placeTowerSound, bannerPlaced.transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (banner)
            {
                Destroy(banner);
                cam.GetComponent<ClickOnObjects>().enabled = true;

            }
        }
    }
    public void SetMatDefault()
    {
        if (meshDefault)
        {
            meshDefault.SetActive(true);
            meshCanPlace.SetActive(false);
            meshCantPlace.SetActive(false);
        }
    }
    public void SetMatCanPlace()
    {
        meshCanPlace.SetActive(true);
        meshCantPlace.SetActive(false);
    }
    public void SetMatCantPlace()
    {
        meshCantPlace.SetActive(true);
        meshCanPlace.SetActive(false);
    }
}
