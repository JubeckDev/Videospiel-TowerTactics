using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour
{
    public GameObject content;

    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenWindow()
    {
        content.SetActive(true);
        GetComponent<Image>().enabled = true;
        cam.GetComponent<Zoom>().canMove = false;
        if (GetComponent<WinPanel>() != null) GetComponent<WinPanel>().SetTexts();
        Time.timeScale = 0;
    }
    public void ClosePanel()
    {
        content.SetActive(false);
        GetComponent<Image>().enabled = false;
        cam.GetComponent<Zoom>().canMove = true;
        Time.timeScale = 1;
    }
}