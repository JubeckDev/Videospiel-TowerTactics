using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenEgg : MonoBehaviour
{
    public GameObject achievements;

    public GameObject clickGoldenEggSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        achievements.GetComponent<Achievements>().SetNewProgress(6, 1);

        Instantiate(clickGoldenEggSound, transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);

        Destroy(gameObject);
    }
}
