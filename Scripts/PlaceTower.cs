using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour
{
    public Camera mainCam;
    public GameObject tower;

    public LayerMask targetLayer;

    public bool canPlace;
    public float timeTillTowerIsPlacable;
    public int numberOfLastPositionsSaved;
    public int towerSelectedNumber;

    public GameObject meshDefault;
    public GameObject meshCanPlace;
    public GameObject meshCantPlace;

    public GameObject rangeVisualizer;

    public GameObject gameUI;

    public GameObject cam;

    public GameObject achievements;

    public TextMeshProUGUI[] goldCostTexts;

    public GameObject placeTowerSound;
    public GameObject placeTowerEffect;

    public List<GameObject> hitObjects = new List<GameObject>();

    public List<Vector3> lastTowerPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (tower)
        {
            //Debug.Log("a" + canPlace);
            //Debug.Log("b" + CheckCollision(tower.GetComponent<BoxCollider>()));
            if(timeTillTowerIsPlacable <= 0) canPlace = CheckCollision(tower.GetComponent<BoxCollider>());

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, targetLayer))
            {
                tower.transform.position = raycastHit.point/* - new Vector3(0,GetComponent<SpawnTower>().towerSpawnOffsetCurrent, 0)*/;
            }

            if (canPlace)
            {
                if (meshCanPlace)SetMatCanPlace();
            }
            else
            {
                if (meshCanPlace) SetMatCantPlace();
            }

            if (canPlace)
            {
                if (lastTowerPositions.Count >= numberOfLastPositionsSaved) lastTowerPositions.RemoveAt(numberOfLastPositionsSaved - 1);
                lastTowerPositions.Insert(0, tower.transform.position);
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                if (canPlace && !EventSystem.current.IsPointerOverGameObject() && timeTillTowerIsPlacable <= 0) //Place Tower
                {
                    //Debug.Log(canPlace);
                    //Debug.Log(CheckCollision(tower.GetComponent<BoxCollider>()));
                    PlaceCurrentTower();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                CancelPlacingTower();
            }

            if (timeTillTowerIsPlacable > 0) timeTillTowerIsPlacable -= 1 * Time.deltaTime;
        }

    }
    public bool CheckCollision(Collider towerCollider)
    {
        Vector3 center = towerCollider.bounds.center;
        Vector3 size = towerCollider.bounds.size;

        // Check for overlapping colliders within the box
        Collider[] colliders = Physics.OverlapBox(center, size / 2f, tower.transform.rotation);

        // Check each collider individually
        hitObjects.Clear();
        foreach (var collider in colliders)
        {
            if (collider.gameObject != tower.gameObject &&
                collider.gameObject.tag != "Ground" &&
                collider.gameObject.name != "Range")
            {
                hitObjects.Add(collider.gameObject);
                return false;
            }
        }

        return true;
    }
    public void PlaceCurrentTower()
    {
        SetMatDefault();
        tower.GetComponent<Shoot>().enabled = true;
        tower.GetComponent<Stats>().rangeDisplay.GetComponent<SphereCollider>().enabled = true;
        tower.GetComponent<ArtifactBuff>().enabled = true;
        gameUI.GetComponent<Gold>().ChangeGold(-int.Parse(goldCostTexts[towerSelectedNumber].text));
        tower.GetComponent<Stats>().totalTowerCost = int.Parse(goldCostTexts[towerSelectedNumber].text);
        //tower.GetComponent<CollisionTest>().shouldTestCollision = false;
        //tower.GetComponent<CollisionTest>().enabled = false;
        tower.GetComponent<BoxCollider>().isTrigger = false;

        //tower.transform.position = lastTowerPositions[lastTowerPositions.Count - 1];

        Instantiate(placeTowerSound, tower.transform.position, transform.rotation, GameObject.Find("Audio/SFX/3D").transform);
        Instantiate(placeTowerEffect, tower.transform.position, transform.rotation, GameObject.Find("Particles").transform);

        if (rangeVisualizer) rangeVisualizer.GetComponent<MeshRenderer>().enabled = false;
        rangeVisualizer = null;
        tower = null;
        meshDefault = null;
        meshCanPlace = null;
        meshCantPlace = null;

        //see how many towers are in the scene
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        int numberOfTowers = towers.Length;
        achievements.GetComponent<Achievements>().SetNewProgress(7, numberOfTowers);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<SpawnTower>().SpawnNewTower(towerSelectedNumber, true);
        }
        else
        {
            cam.GetComponent<ClickOnObjects>().enabled = true;
        }
    }
    public void CancelPlacingTower()
    {
        Destroy(tower);
        cam.GetComponent<ClickOnObjects>().enabled = true;
    }
    public void SetMatDefault()
    {
        if (meshDefault)
        {
            meshDefault.SetActive(true);
            meshCanPlace.SetActive(false);
            meshCantPlace.SetActive(false);
        }
    }
    public void SetMatCanPlace()
    {
        meshCanPlace.SetActive(true);
        meshCantPlace.SetActive(false);
    }
    public void SetMatCantPlace()
    {
        meshCantPlace.SetActive(true);
        meshCanPlace.SetActive(false);
    }
}
