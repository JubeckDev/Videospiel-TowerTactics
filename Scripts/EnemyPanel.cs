using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyPanel : MonoBehaviour
{
    public string[] enemyTypes;
    public Sprite enemyPreviewSprite;
    public Sprite[] enemyPreviewOverlays;
    public string[] immunities;

    public Slider livesLeftSlider;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI immunitiesText;
    public TextMeshProUGUI type;
    public Image enemyPreview;
    public Image enemyPreviewOverlay;

    public GameObject enemySelected;
    public int enemySelectedNumber;

    public GameObject enemyPointer;
    public GameObject enemyPointerSpawned;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemySelected == null) CloseWindow();
        else
        {
            SetEnemyPreview();
            SetTexts();
            UpdateSlider();
        }
    }
    public void SetEnemyPreview()
    {

        //Check where the name of the selected tower and the sprite match
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            if (enemySelected.GetComponent<EnemyLives>().enemyType == enemyTypes[i])
            {
                enemySelectedNumber = i;
            }
        }

        enemyPreview.sprite = enemyPreviewSprite;
        enemyPreview.color = enemySelected.GetComponent<EnemyLives>().colors[enemySelected.GetComponent<EnemyLives>().stage];
        enemyPreviewOverlay.sprite = enemyPreviewOverlays[enemySelectedNumber];
    }
    public void SetTexts()
    {
        type.text = enemyTypes[enemySelectedNumber];
        stageText.text = enemySelected.GetComponent<EnemyLives>().stage.ToString();
        immunitiesText.text = immunities[enemySelectedNumber];
    }
    public void UpdateSlider()
    {
        livesLeftSlider.maxValue = enemySelected.GetComponent<EnemyLives>().lifeAmounts[enemySelected.GetComponent<EnemyLives>().stage];
        livesLeftSlider.value = enemySelected.GetComponent<EnemyLives>().lives;
    }
    public void OpenWindow(GameObject enemySelected)
    {
        this.enemySelected = enemySelected;

        gameObject.SetActive(true);

        SpawnEnemyPointer();
    }
    public void CloseWindow()
    {
        if (enemyPointerSpawned) Destroy(enemyPointerSpawned);

        gameObject.SetActive(false);
    }
    public void SpawnEnemyPointer()
    {
        if (enemyPointerSpawned) Destroy(enemyPointerSpawned);

        enemyPointerSpawned = Instantiate(enemyPointer, enemySelected.transform.position, transform.rotation);
        enemyPointerSpawned.GetComponent<EnemyPointerFollow>().AssignTarget(enemySelected);
    }
}
