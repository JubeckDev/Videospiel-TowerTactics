using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBuffHero : MonoBehaviour
{
    public float buffIncrease;

    public float buffTime = 4;
    public float buffCooldown = 30;

    private GameObject artifactPanel;

    public GameObject aura;

    // Start is called before the first frame update
    void Start()
    {
        artifactPanel = GameObject.Find("Canvas/GameUI/ArtifactPanel");
    }
    public void ActivateBuff(bool toTrue)
    {
        artifactPanel = GameObject.Find("Canvas/GameUI/ArtifactPanel");

        if (toTrue) StartCoroutine("Buff");
        else StopCoroutine("Buff");
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Buff()
    {
        GetComponent<StatsHero>().IncreaseAttackSpeed(buffIncrease);
        aura.SetActive(true);
        yield return new WaitForSeconds(buffTime);
        GetComponent<StatsHero>().IncreaseAttackSpeed(1 / buffIncrease);
        aura.SetActive(false);
        yield return new WaitForSeconds(buffCooldown);
        StartCoroutine("Buff");
    }
}
