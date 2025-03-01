 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBanner : MonoBehaviour
{
    public GameObject hero;
    public GameObject rangeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        UpdateRange(hero.GetComponent<StatsHero>().range);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateRange(float range)
    {
        rangeDisplay.transform.localScale = new Vector3(range * 2, range * 2, range * 2);

        //GetComponent<SphereCollider>().radius = range;
    }
}
