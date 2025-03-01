using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsBallista : MonoBehaviour
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
        GetComponent<VariationEffectsBallista>().abilityTime += specialStatChanges[0];
        Debug.Log("Bought Special0");
    }
    public void SetSpecial1()
    {
        GetComponent<Stats>().range += specialStatChanges[1];
        Debug.Log("Bought Special1");
    }
    public void SetSpecial2()
    {
        GetComponent<VariationEffectsBallista>().arrowSize *= specialStatChanges[2];
        Debug.Log("Bought Special2");
    }
    public void SetSpecial3()
    {
        GetComponent<Stats>().poisonTime += specialStatChanges[3];
        Debug.Log("Bought Special3");
    }
    public void SetSpecial4()
    {
        GetComponent<VariationEffectsBallista>().arrowSplitSize *= specialStatChanges[4];
        Debug.Log("Bought Special4");
    }
    public void SetSpecial5()
    {
        GetComponent<Stats>().bulletMovementSpeed += specialStatChanges[5];
        Debug.Log("Bought Special5");
    }

    public void SetSpecialInfoTexts()
    {
        specialInfoTexts[0] = "Rapid Fire Duration: +" + specialStatChanges[0] + "s";
        specialInfoTexts[1] = "Range: +" + specialStatChanges[1];
        specialInfoTexts[2] = "Arrow Size: " + specialStatChanges[2] * 100 + "%";
        specialInfoTexts[3] = "Poison Duration: +" + specialStatChanges[3] + "s";
        specialInfoTexts[4] = "Split Arrow Size: " + specialStatChanges[4] * 100 + "%";
        specialInfoTexts[5] = "Arrow Speed: +" + specialStatChanges[5];
    }
}
