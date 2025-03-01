using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public int damage;
    public GameObject dust;

    public string towerType = "Phoenix";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyLives>().TakeDamage(damage, gameObject, towerType);
        }
        else if(other.tag =="Ground")
        {
            GameObject dustSpawned = Instantiate(dust, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }
}
