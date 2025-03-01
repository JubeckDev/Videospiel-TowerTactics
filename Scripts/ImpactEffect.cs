using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    public int damage;
    public float lifetime = 0.3f;

    public string towerType = "Cannon";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Lifetime");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyLives>().TakeDamage(damage, gameObject, towerType);
           
        }
        
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}