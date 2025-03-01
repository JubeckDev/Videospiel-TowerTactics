using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RemoveObjectPanel : MonoBehaviour
{
    public GameObject content;

    public TextMeshProUGUI removeCostText;

    public GameObject currentRemovableObject;

    public GameObject gameUI;

    public Button removeButton;

    public GameObject placeTowerSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (content.activeSelf)
        {
            removeButton.interactable = gameUI.GetComponent<Gold>().gold >= currentRemovableObject.GetComponent<RemovableObject>().removeCost;
        }
    }
    public void OpenWindow(GameObject removableObject)
    {
        content.SetActive(true);
        removeCostText.text = removableObject.GetComponent<RemovableObject>().removeCost.ToString();

        currentRemovableObject = removableObject;

        currentRemovableObject.GetComponent<MeshRenderer>().enabled = true;
    }
    public void CloseWindow()
    {
        if(currentRemovableObject) currentRemovableObject.GetComponent<MeshRenderer>().enabled = false;

        content.SetActive(false);
    }
    public void RemoveRemovableObject()
    {
        gameUI.GetComponent<Gold>().ChangeGold(-currentRemovableObject.GetComponent<RemovableObject>().removeCost);

        Instantiate(placeTowerSound, currentRemovableObject.transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);

        currentRemovableObject.SetActive(false);

        CloseWindow();
    }
}