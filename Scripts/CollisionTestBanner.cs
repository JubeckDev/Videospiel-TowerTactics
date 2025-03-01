using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTestBanner : MonoBehaviour
{
    public GameObject heroPanel;
    public bool shouldTestCollision = true;

    // Start is called before the first frame update
    void Start()
    {
        heroPanel= GameObject.Find("Canvas/GameUI/HeroPanel");
        shouldTestCollision = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionStay(Collision collision)
    {
        if (shouldTestCollision)
        {
            if (collision.gameObject.tag != "Ground" && collision.gameObject.name != "ClickRange")
            {
                heroPanel.GetComponent<PlaceHero>().canPlace = false;
                //Debug.Log("InColliderCollision");
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (shouldTestCollision)
        {
            if (collision.gameObject.tag != "Ground" && collision.gameObject.name != "ClickRange")
            {
                heroPanel.GetComponent<PlaceHero>().canPlace = true;
            }
            //Debug.Log("ExitCollider");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (shouldTestCollision)
        {
            if (other.gameObject.tag != "Ground" && other.gameObject.name != "ClickRange" && other.gameObject.name != "Range")
            {
                heroPanel.GetComponent<PlaceHero>().canPlace = false;
                //Debug.Log("InCollider");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (shouldTestCollision)
        {
            if (other.gameObject.tag != "Ground" && other.gameObject.name != "ClickRange" && other.gameObject.name != "Range")
            {
                heroPanel.GetComponent<PlaceHero>().canPlace = true;
            }
        }
        //Debug.Log("ExitCollider");
    }


}
