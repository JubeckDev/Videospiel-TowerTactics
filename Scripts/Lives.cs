using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lives : MonoBehaviour
{
    public int initialLives = 35;
    public int lives;

    public int[] startingLives;

    public TextMeshProUGUI livesText;

    public GameObject maps;

    public GameObject losePanel;

    public AudioSource loseSound;
    public GameObject playerDamage;

    public Transform portal;

    // Start is called before the first frame update
    void Start()
    {
        SetLives();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReduceLives(int damage)
    {
        //Debug.Log("Damage");
        lives -= damage;

        Instantiate(playerDamage, portal.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);

        UpdateLivesText();

        if (lives <= 0)
        {
            losePanel.GetComponent<GameEndPanel>().OpenWindow();
            loseSound.Play();
        }
    }
    public void SetLives()
    {
        initialLives = startingLives[maps.GetComponent<Difficulty>().difficulty];
        ResetLives();
        UpdateLivesText();
    }
    public void UpdateLivesText()
    {
        if(lives > 0)
        {
            livesText.text = lives.ToString();
        }
        else
        {
            livesText.text = "0";
        }
    }
    public void ResetLives()
    {
        lives = initialLives;
        UpdateLivesText();
    }
}