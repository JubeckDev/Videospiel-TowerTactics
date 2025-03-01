using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLook : MonoBehaviour
{
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        var lookPos = cam.transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
