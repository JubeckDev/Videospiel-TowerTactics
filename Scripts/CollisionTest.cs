using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public GameObject towerMenu;
    public bool shouldTestCollision = true;

    // Start is called before the first frame update
    void Start()
    {
        towerMenu = GameObject.Find("Canvas/GameUI/TowerMenu");
        shouldTestCollision = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (shouldTestCollision)
        {

            if (other.gameObject.tag != "Ground" && other.gameObject.name != "Range")
            {
                towerMenu.GetComponent<PlaceTower>().canPlace = false;
                Debug.Log("InCollider");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (shouldTestCollision)
        {
            if (other.gameObject.tag != "Ground" && other.gameObject.name != "Range")
            {
                towerMenu.GetComponent<PlaceTower>().canPlace = true;
                Debug.Log("ExitCollider");
            }
        }
    }

}
