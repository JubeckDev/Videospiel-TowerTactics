using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    public int damage;
    public bool canSlow;
    public float slowTime;
    public float collisionCooldown = 0.5f;
    public bool canCollide = true;
    public bool canFreeze;
    public float freezeTime;
    public bool canBurn;
    public float burnTime;

    public string towerType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy" && canCollide)
        {
            other.GetComponent<EnemyLives>().TakeDamage(damage, gameObject, towerType);
            if(canSlow)
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
            StartCoroutine("CollisionCooldown");
        }
    }
    IEnumerator CollisionCooldown()
    {
        canCollide = false;
        yield return new WaitForSeconds(collisionCooldown);
        canCollide = true;
    }

}
