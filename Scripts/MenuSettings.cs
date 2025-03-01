using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    public AudioSource buttonClickSound;

    public GameObject settings;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CloseWindow()
    {
        settings.SetActive(true);
        credits.SetActive(false);

        buttonClickSound.Play();
        gameObject.SetActive(false);
    }
    public void OpenSettings()
    {
        settings.SetActive(true);
        credits.SetActive(false);
    }
    public void OpenCredits()
    {
        settings.SetActive(false);
        credits.SetActive(true);
    }
}
