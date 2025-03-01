using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnObjects : MonoBehaviour
{
    public GameObject towerPanel;
    public GameObject enemyPanel;
    public GameObject heroPanel;
    public GameObject removeObjectPanel;
    public GameObject ressurrectTowerPanel;

    public LayerMask raycastLayers;

    Camera mainCam;

    public bool canClick = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && canClick)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, raycastLayers))
            {
                if (raycastHit.collider.tag == "Tower")
                {
                    towerPanel.GetComponent<TowerPanel>().OpenWindow(raycastHit.collider.gameObject);
                    enemyPanel.GetComponent<EnemyPanel>().CloseWindow();
                    heroPanel.GetComponent<HeroPanel>().CloseWindow();
                    removeObjectPanel.GetComponent<RemoveObjectPanel>().CloseWindow();
                    ressurrectTowerPanel.GetComponent<RessurrectTowerPanel>().CloseWindow();
                }
                else if (raycastHit.collider.tag == "RemovableObject")
                {
                    removeObjectPanel.GetComponent<RemoveObjectPanel>().OpenWindow(raycastHit.collider.gameObject);
                    enemyPanel.GetComponent<EnemyPanel>().CloseWindow();
                    towerPanel.GetComponent<TowerPanel>().CloseWindow();
                    heroPanel.GetComponent<HeroPanel>().CloseWindow();
                    ressurrectTowerPanel.GetComponent<RessurrectTowerPanel>().CloseWindow();
                }
                else if (raycastHit.collider.tag == "Enemy")
                {
                    heroPanel.GetComponent<HeroPanel>().CloseWindow();
                    enemyPanel.GetComponent<EnemyPanel>().OpenWindow(raycastHit.collider.gameObject);
                    towerPanel.GetComponent<TowerPanel>().CloseWindow();
                    heroPanel.GetComponent<HeroPanel>().CloseWindow();
                    ressurrectTowerPanel.GetComponent<RessurrectTowerPanel>().CloseWindow();
                }
                else if(raycastHit.collider.tag == "HeroBanner")
                {
                    heroPanel.GetComponent<HeroPanel>().OpenWindow(raycastHit.collider.gameObject);
                    enemyPanel.GetComponent<EnemyPanel>().CloseWindow();
                    towerPanel.GetComponent<TowerPanel>().CloseWindow();
                    removeObjectPanel.GetComponent<RemoveObjectPanel>().CloseWindow();
                    ressurrectTowerPanel.GetComponent<RessurrectTowerPanel>().CloseWindow();
                }
                else if (raycastHit.collider.tag == "BrokenTower")
                {
                    ressurrectTowerPanel.GetComponent<RessurrectTowerPanel>().OpenWindow(raycastHit.collider.gameObject);
                    heroPanel.GetComponent<HeroPanel>().CloseWindow();
                    enemyPanel.GetComponent<EnemyPanel>().CloseWindow();
                    towerPanel.GetComponent<TowerPanel>().CloseWindow();
                    removeObjectPanel.GetComponent<RemoveObjectPanel>().CloseWindow();
                }
                else
                {
                    CloseAllWindows();
                }
            }
            else
            {
                CloseAllWindows();
            }
        }
    }
    public void CloseAllWindows()
    {
        heroPanel.GetComponent<HeroPanel>().CloseWindow();
        enemyPanel.GetComponent<EnemyPanel>().CloseWindow();
        towerPanel.GetComponent<TowerPanel>().CloseWindow();
        removeObjectPanel.GetComponent<RemoveObjectPanel>().CloseWindow();
        ressurrectTowerPanel.GetComponent<RessurrectTowerPanel>().CloseWindow();
    }
}