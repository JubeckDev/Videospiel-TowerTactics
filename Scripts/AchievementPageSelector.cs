using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPageSelector : MonoBehaviour
{
    public int currentPage;

    public GameObject[] pages;

    public GameObject nextButton;
    public GameObject previousButton;

    public AudioSource buttonClickSound;

    public GameObject achievements;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangePage(int pageChange)
    {
        currentPage += pageChange;

        if(currentPage == pages.Length - 1)
        {
            nextButton.SetActive(false);
            previousButton.SetActive(true);
        }
        else if(currentPage == 0)
        {
            nextButton.SetActive(true);
            previousButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
            previousButton.SetActive(true);
        }
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPage);
        }

        achievements.GetComponent<Achievements>().UpdateNotifierVisibility();

        buttonClickSound.Play();
    }
}
