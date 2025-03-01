using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersInRange : MonoBehaviour
{
    public List<GameObject> towerList = new List<GameObject>();

    public bool[] buffActive;

    public GameObject achievements;

    public GameObject towerBuffParticles;

    // Start is called before the first frame update
    void Start()
    {
        achievements = GameObject.Find("Canvas/Menu/Achievements");
    }

    void Update()
    {
        foreach (GameObject tower in towerList)
        {
            //Debug.Log("Going through list");
            if (tower == null)
            {
                //Debug.Log("Removed enemy from list");
                towerList.Remove(tower);
                break;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tower")
        {
            if (!towerList.Contains(other.gameObject)) towerList.Add(other.gameObject);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tower")
        {
            towerList.Remove(other.gameObject);
        }
    }
    public void ActivateDamageBuff()
    {
        ActivateSelectedBuff(0);
    }
    public void DeactivateDamageBuff()
    {
        DeactivateSelectedBuff(0);
    }
    public void ActivateBulletSpeedBuff()
    {
        ActivateSelectedBuff(1);
    }
    public void DeactivateBulletSpeedBuff()
    {
        DeactivateSelectedBuff(1);
    }
    public void ActivateAttackSpeedBuff()
    {
        ActivateSelectedBuff(2);
    }
    public void DeactivateAttackSpeedBuff()
    {
        DeactivateSelectedBuff(2);
    }
    public void ActivatePiercingBuff()
    {
        ActivateSelectedBuff(3);
    }
    public void DeactivatePiercingBuff()
    {
        DeactivateSelectedBuff(3);
    }
    public void ActivateRangeBuff()
    {
        ActivateSelectedBuff(4);
    }
    public void DeactivateRangeBuff()
    {
        DeactivateSelectedBuff(4);
    }
    public void DeactivateSelectedBuff(int buffSelected)
    {
        foreach (GameObject tower in towerList)
        {
            tower.GetComponent<Stats>().buffActive[buffSelected] = false;
            tower.GetComponent<Stats>().UpdateRange();

            bool towerHasNoActiveBuffs = true;
            for (int i = 0; i < tower.GetComponent<Stats>().buffActive.Length; i++)
            {
                if (tower.GetComponent<Stats>().buffActive[i] == true) towerHasNoActiveBuffs = false;
            }

            if(towerHasNoActiveBuffs) tower.GetComponent<Stats>().buffParticles.SetActive(false);
        }
        buffActive[buffSelected] = false;
    }
    public void ActivateSelectedBuff(int buffSelected)
    {
        foreach (GameObject tower in towerList)
        {
            tower.GetComponent<Stats>().buffActive[buffSelected] = true;
            tower.GetComponent<Stats>().UpdateRange();
            tower.GetComponent<Stats>().buffParticles.SetActive(true);
        }
        buffActive[buffSelected] = true;
        achievements.GetComponent<Achievements>().SetNewProgress(3, CalculateAmountOfActiveBuffs());
        achievements.GetComponent<Achievements>().SetNewProgress(1, towerList.Count);
    }
    public int CalculateAmountOfActiveBuffs()
    {
        int buffsActive = 0;
        for (int i = 0; i < buffActive.Length; i++)
        {
            if (buffActive[i])
            {
                buffsActive++;
            }
        }
        return buffsActive;
    }
}
