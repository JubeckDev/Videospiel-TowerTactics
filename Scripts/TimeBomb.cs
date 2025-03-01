using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{

    public GameObject timeBombExplosion;
    public GameObject timeBombExplosionSpawned;
    public float explosionDelay;
    public float explosionRadius;

    public GameObject timeBomb;

    public GameObject particleEffects;

    public GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        explosionDelay = tower.GetComponentInParent<VariationEffectsCannon>().explosionDelay;
        explosionRadius = tower.GetComponentInParent<VariationEffectsCannon>().explosionRadius;

        particleEffects = GameObject.Find("Particles");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Obstacle")
        {
            GetComponent<Bullet>().movementSpeed = 0;
            StartCoroutine("Explode");
            
        }
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);
        timeBombExplosionSpawned = Instantiate(timeBombExplosion, transform.position, transform.rotation, particleEffects.transform) as GameObject;
        timeBombExplosionSpawned.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);

        Instantiate(timeBomb, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);

        Destroy(gameObject);
    }

}
