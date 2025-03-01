using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DifficultyHoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPanel;
    public TextMeshProUGUI description;

    public GameObject maps;
    public GameObject gameUI;
    public GameObject waves;

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        //description = infoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.SetActive(true);
        description.text = "";
        description.text =
            "Waves: " + waves.GetComponent<Waves>().waveGoals[difficulty] +
            "\nLives: " + gameUI.GetComponent<Lives>().startingLives[difficulty] +
            "\nGold: " + gameUI.GetComponent<Gold>().startingGold[difficulty] +
            "\nXP Reward: +" + maps.GetComponent<Difficulty>().difficultyRewards[difficulty];

        description.GetComponent<TextMeshProUGUI>().color = GetComponent<Image>().color;

        infoPanel.transform.position = new Vector3(transform.position.x, infoPanel.transform.position.y, infoPanel.transform.position.z);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
    }
}
