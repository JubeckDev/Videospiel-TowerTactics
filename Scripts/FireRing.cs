using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour
{
    public int damage;
    public float damageDelay;

    public GameObject fireRingSound;
    public float soundDelay;

    public string towerType = "Phoenix";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DamageEnemy");
    }
    IEnumerator DamageEnemy()
    {
        yield return new WaitForSeconds(soundDelay);
        Instantiate(fireRingSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        yield return new WaitForSeconds(damageDelay - soundDelay);
        GetComponentInParent<EnemyLives>().TakeDamage(damage, gameObject, towerType);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
