using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBuff : MonoBehaviour
{
    public string buff;
    public float buffIncrease;

    public float buffTime = 4;
    public float buffCooldown = 30;

    private GameObject artifactPanel;
    private int towerNumber;

    public GameObject aura;

    public bool projectileBuffIsOn;
    public bool buffCoroutineHasStarted;

    // Start is called before the first frame update
    void Start()
    {
        artifactPanel = GameObject.Find("Canvas/GameUI/ArtifactPanel");
        towerNumber = CalculateTowerNumber();
    }
    public void ActivateBuffIfUnlocked()
    {
        if (!artifactPanel.GetComponent<ArtifactPanel>().hasArtifact[towerNumber]) return;

        buffCoroutineHasStarted = true;

        if (buff == "Damage") StartCoroutine("DamageBuff");
        if (buff == "Range") StartCoroutine("RangeBuff");
        if (buff == "Piercing") StartCoroutine("Piercinguff");
        if (buff == "Bullet Speed") StartCoroutine("BulletSpeedBuff");
        if (buff == "Duration") StartCoroutine("DurationBuff");
    }
    private void Update()
    {
        if(!buffCoroutineHasStarted) ActivateBuffIfUnlocked();
    }
    public int CalculateTowerNumber()
    {
        for (int i = 0; i < artifactPanel.GetComponent<ArtifactPanel>().towerNames.Length; i++)
        {
            if(artifactPanel.GetComponent<ArtifactPanel>().towerNames[i] == GetComponent<Stats>().towerType)
            {
                return i;
            }
        }
        return -1;
    }
    IEnumerator DamageBuff()
    {
        GetComponent<Stats>().damage += Mathf.RoundToInt(buffIncrease);
        aura.SetActive(true);
        projectileBuffIsOn = true;
        yield return new WaitForSeconds(buffTime);
        GetComponent<Stats>().damage -= Mathf.RoundToInt(buffIncrease);
        aura.SetActive(false);
        projectileBuffIsOn = false;
        yield return new WaitForSeconds(buffCooldown);
        StartCoroutine("DamageBuff");
    }
    IEnumerator RangeBuff()
    {
        GetComponent<Stats>().range += buffIncrease;
        GetComponent<Stats>().UpdateRange();
        aura.SetActive(true);
        yield return new WaitForSeconds(buffTime);
        GetComponent<Stats>().range -= buffIncrease;
        GetComponent<Stats>().UpdateRange();
        aura.SetActive(false);
        yield return new WaitForSeconds(buffCooldown);
        StartCoroutine("DamageBuff");
    }
    IEnumerator Piercinguff()
    {
        GetComponent<Stats>().piercing += Mathf.RoundToInt(buffIncrease);
        aura.SetActive(true);
        projectileBuffIsOn = true;
        yield return new WaitForSeconds(buffTime);
        GetComponent<Stats>().piercing -= Mathf.RoundToInt(buffIncrease);
        aura.SetActive(false);
        projectileBuffIsOn = false;
        yield return new WaitForSeconds(buffCooldown);
        StartCoroutine("DamageBuff");
    }
    IEnumerator BulletSpeedBuff()
    {
        GetComponent<Stats>().bulletMovementSpeed += buffIncrease;
        aura.SetActive(true);
        projectileBuffIsOn = true;
        yield return new WaitForSeconds(buffTime);
        GetComponent<Stats>().bulletMovementSpeed -= buffIncrease;
        aura.SetActive(false);
        projectileBuffIsOn = false;
        yield return new WaitForSeconds(buffCooldown);
        StartCoroutine("DamageBuff");
    }
    IEnumerator DurationBuff()
    {
        GetComponent<Stats>().slowTime += buffIncrease;
        GetComponent<Stats>().burnTime += buffIncrease;
        GetComponent<Stats>().confusionTime += buffIncrease;
        GetComponent<Stats>().poisonTime += buffIncrease;
        aura.SetActive(true);
        projectileBuffIsOn = true;
        yield return new WaitForSeconds(buffTime);
        GetComponent<Stats>().slowTime -= buffIncrease;
        GetComponent<Stats>().burnTime -= buffIncrease;
        GetComponent<Stats>().confusionTime -= buffIncrease;
        GetComponent<Stats>().poisonTime -= buffIncrease;
        aura.SetActive(false);
        projectileBuffIsOn = false;
        yield return new WaitForSeconds(buffCooldown);
        StartCoroutine("DamageBuff");
    }
}
