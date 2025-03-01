using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpButton : MonoBehaviour
{
    public bool isNormalSpeed = true;
    public float speedUpValue = 3;

    public Color spedUp;
    public Color notSpedUp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpeedUp()
    {
        isNormalSpeed = !isNormalSpeed;
        if (isNormalSpeed)
        {
            GetComponent<Image>().color = notSpedUp;
            Time.timeScale = 1;
        }
        else
        {
            GetComponent<Image>().color = spedUp;
            Time.timeScale = speedUpValue;
        }
    }
}