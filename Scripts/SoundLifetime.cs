using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLifetime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Lifetime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
    }
}
