using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEffects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartConfusion()
    {
        StartCoroutine("Confusion");
    }
    public void StartPoison()
    {
        StartCoroutine("Poison");
    }
    public void StartBurn()
    {
        StartCoroutine("Burn");
    }
    IEnumerator Confusion()
    {
        yield return new WaitForSeconds(60);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Movement>().Confuse(1);
        }
        StartCoroutine("Confusion");
    }
    IEnumerator Poison()
    {
        yield return new WaitForSeconds(60);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Movement>().Poison(1);
        }
        StartCoroutine("Poison");
    }
    IEnumerator Burn()
    {
        yield return new WaitForSeconds(60);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Movement>().Burn(1);
        }
        StartCoroutine("Burn");
    }
}
