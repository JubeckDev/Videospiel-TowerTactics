using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public int difficulty;

    public Button[] difficultyButtons;
    public Color[] difficultyColors;

    public Button[] mapButtons;

    public float[] difficultyRewards;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDifficulty(int difficulty)
    {
        this.difficulty = difficulty;

        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            difficultyButtons[i].interactable = i != difficulty;
        }
        for (int i = 0; i < mapButtons.Length; i++)
        {
            mapButtons[i].GetComponent<Image>().color = difficultyColors[difficulty];
        }

        GetComponent<Maps>().UpdateRewardInfoTexts();
    }
}
