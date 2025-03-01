using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsCrystalTower : MonoBehaviour
{
    public float[] specialStatChanges;

    public string[] specialInfoTexts = new string[6];

    // Start is called before the first frame update
    void Start()
    {
        SetSpecialInfoTexts();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetSpecialEffect(int number)
    {
        int specialBought = GetComponent<Enhancements>().variationBought * 2 + number;

        if (specialBought == 0)
            SetSpecial0();
        if (specialBought == 1)
            SetSpecial1();
        if (specialBought == 2)
            SetSpecial2();
        if (specialBought == 3)
            SetSpecial3();
        if (specialBought == 4)
            SetSpecial4();
        if (specialBought == 5)
            SetSpecial5();
    }
    public void SetSpecial0()
    {
        GetComponent<Stats>().cooldownTime += specialStatChanges[0];
        Debug.Log("Bought Special0");
    }
    public void SetSpecial1()
    {
        GetComponent<VariationEffectsCrystalTower>().crystalSize += specialStatChanges[1];
        Debug.Log("Bought Special1");
    }
    public void SetSpecial2()
    {
        GetComponent<Stats>().slowTime += specialStatChanges[2];
        Debug.Log("Bought Special2");
    }
    public void SetSpecial3()
    {
        GetComponent<Stats>().attackSpeed *= specialStatChanges[3];
        Debug.Log("Bought Special3");
    }
    public void SetSpecial4()
    {
        GetComponent<VariationEffectsCrystalTower>().swordCoolDown += specialStatChanges[4];
        Debug.Log("Bought Special4");
    }
    public void SetSpecial5()
    {
        GetComponent<Stats>().damage += Mathf.RoundToInt (specialStatChanges[5]);
        Debug.Log("Bought Special5");
    }

    public void SetSpecialInfoTexts()
    {
        specialInfoTexts[0] = "Crystal Rain Cooldown " + specialStatChanges[0] + "s";
        specialInfoTexts[1] = "Crystal Size: +" + specialStatChanges[1];
        specialInfoTexts[2] = "Slow Duration: +" + specialStatChanges[2] * 100 + "%";
        specialInfoTexts[3] = "Attack Speed: " + Mathf.RoundToInt(1 / specialStatChanges[3] * 100) + "%";
        specialInfoTexts[4] = "Sword Cooldown: " + specialStatChanges[4] + "s";
        specialInfoTexts[5] = "Damage: +" + specialStatChanges[5];
    }
}