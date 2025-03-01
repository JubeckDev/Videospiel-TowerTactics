using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollision : MonoBehaviour
{
    public GameObject heroStep;

    public GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" && hero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            Instantiate(heroStep, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        }
    }
}
