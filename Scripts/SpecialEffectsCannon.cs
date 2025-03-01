using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsCannon : MonoBehaviour
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
        GetComponent<VariationEffectsCannon>().explosionDelay += specialStatChanges[0];
        Debug.Log("Bought Special0");
    }
    public void SetSpecial1()
    {
        GetComponent<VariationEffectsCannon>().explosionRadius += specialStatChanges[1];
        Debug.Log("Bought Special1");
    }
    public void SetSpecial2()
    {
        GetComponent<VariationEffectsCannon>().impactEffectRadius += specialStatChanges[2];
        Debug.Log("Bought Special2");
    }
    public void SetSpecial3()
    {
        GetComponent<VariationEffectsCannon>().bounces += Mathf.RoundToInt(specialStatChanges[3]);
        Debug.Log("Bought Special3");
    }
    public void SetSpecial4()
    {
        GetComponent<VariationEffectsCannon>().splitBulletSize *= specialStatChanges[4];
        GetComponent<VariationEffectsCannon>().splitBulletParentSize *= specialStatChanges[4];

        Debug.Log("Bought Special4");
    }
    public void SetSpecial5()
    {
        GetComponent<Stats>().damage += Mathf.RoundToInt(specialStatChanges[5]);
        Debug.Log("Bought Special5");
    }

    public void SetSpecialInfoTexts()
    {
        specialInfoTexts[0] = "Explosion Delay: " + specialStatChanges[0] + "s";
        specialInfoTexts[1] = "Explosion Radius: +" + specialStatChanges[1];
        specialInfoTexts[2] = "Impact Radius: +" + specialStatChanges[2];
        specialInfoTexts[3] = "Bounces: +" + specialStatChanges[3];
        specialInfoTexts[4] = "Bullet Size: +" + specialStatChanges[4];
        specialInfoTexts[5] = "Damage: +" + specialStatChanges[5];
    }





}
