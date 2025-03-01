using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    public int lives;
    public int damageCap = 3;

    public string towerType = "IceDragon";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyLives>().TakeDamage(damageCap, gameObject, towerType);
            lives--;
            if (lives <= 0) Destroy(gameObject);
        }
    }
}
