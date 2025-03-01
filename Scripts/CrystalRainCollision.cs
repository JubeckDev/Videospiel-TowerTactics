using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRainCollision : MonoBehaviour
{
    public int damage;

    public GameObject crystalTowerCrystalRainSound;

    public float crystalTowerCrystalRainSoundCooldown = 1f;

    public bool soundIsOnCooldown;

    public string towerType = "CrystalTower";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyLives>().TakeDamage(damage, gameObject, towerType);
            Debug.Log("Hit");
        }
        if (!soundIsOnCooldown)
        {
            Instantiate(crystalTowerCrystalRainSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
            StartCoroutine("CrystalRainSound");
        }
    }
    IEnumerator CrystalRainSound()
    {
        soundIsOnCooldown = true;
        yield return new WaitForSeconds(crystalTowerCrystalRainSoundCooldown);
        soundIsOnCooldown = false;
    }
}
