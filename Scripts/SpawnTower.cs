using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public GameObject[] towers;

    public GameObject towersParent;

    public GameObject towerPanel;
    public GameObject heroPanel;
    public GameObject infoPanel;

    public GameObject cam;

    public AudioSource buttonClickSound;

    public GameObject[] towerButtons;
    public GameObject gameUI;

    public float placeDelay;

    public int towerSpawnAnimStepsTotal;
    public int towerSpawnAnimStepsLeft;
    public float towerSpawnAnimTime;
    public float towerSpawnOffsetBeginning;
    public float towerSpawnOffsetCurrent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SpawnNewTower(0 + GetComponent<TowerSelector>().towerCategorySelected * 4, true);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SpawnNewTower(1 + GetComponent<TowerSelector>().towerCategorySelected * 4, true);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SpawnNewTower(2 + GetComponent<TowerSelector>().towerCategorySelected * 4, true);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SpawnNewTower(3 + GetComponent<TowerSelector>().towerCategorySelected * 4, true);
    }
    public void SpawnNewTower(int towerNumber, bool checkIfGoldRequirementIsMet)
    {
        if (towerNumber == GetComponent<PlaceTower>().towerSelectedNumber && GetComponent<PlaceTower>().tower) return; //already placing that tower

        if (checkIfGoldRequirementIsMet)
        {
            if (gameUI.GetComponent<Gold>().gold < int.Parse(towerButtons[towerNumber].GetComponent<DragTower>().goldCost.text)) return; //not enough gold
        }

        if (GetComponent<PlaceTower>().tower != null)
        {
            Destroy(GetComponent<PlaceTower>().tower);
        }

        GetComponent<PlaceTower>().canPlace = false;
        GameObject towerSpawned = Instantiate(towers[towerNumber], gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        towerSpawned.transform.SetParent(towersParent.transform);
        towerSpawned.GetComponent<InitializeMeshes>().meshDefault.SetActive(false);
        GetComponent<PlaceTower>().timeTillTowerIsPlacable = placeDelay;
        GetComponent<PlaceTower>().lastTowerPositions.Clear();
        GetComponent<PlaceTower>().tower = towerSpawned;
        //GetComponent<PlaceTower>().canPlace = GetComponent<PlaceTower>().CheckCollision(towerSpawned.GetComponent<BoxCollider>());
        //GetComponent<PlaceTower>().canPlace = true;
        GetComponent<PlaceTower>().towerSelectedNumber = towerNumber;
        towerPanel.GetComponent<TowerPanel>().CloseWindow();
        heroPanel.GetComponent<HeroPanel>().CloseWindow();
        infoPanel.GetComponent<InfoPanel>().CloseWindow();
        cam.GetComponent<ClickOnObjects>().enabled = false;
        
        StopCoroutine("TowerSpawnAnim");
        towerSpawned.transform.position += new Vector3(0, towerSpawnOffsetBeginning, 0);
        towerSpawnOffsetCurrent = towerSpawnOffsetBeginning;
        towerSpawnAnimStepsLeft = towerSpawnAnimStepsTotal;
        StartCoroutine("TowerSpawnAnim");

        buttonClickSound.Play();
    }
    IEnumerator TowerSpawnAnim()
    {
        yield return new WaitForSeconds(towerSpawnAnimTime / towerSpawnAnimStepsTotal);
        if (GetComponent<PlaceTower>().tower)
        {
            towerSpawnOffsetCurrent -= towerSpawnOffsetBeginning / towerSpawnAnimStepsTotal;
            towerSpawnAnimStepsLeft--;
            if (towerSpawnAnimStepsLeft > 0) StartCoroutine("TowerSpawnAnim");
        }
    }
}
