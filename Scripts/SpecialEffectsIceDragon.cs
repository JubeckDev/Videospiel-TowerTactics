using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsIceDragon : MonoBehaviour
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
        GetComponent<VariationEffectsIceDragon>().abilityTime += specialStatChanges[0];
        bool particleSystemWasPlaying = false;
        if(GetComponent<VariationEffectsIceDragon>().flames.isPlaying)
        {
            GetComponent<VariationEffectsIceDragon>().flames.Stop();
            GetComponent<VariationEffectsIceDragon>().flames.Clear();
            particleSystemWasPlaying = true;
        }

        var main = GetComponent<VariationEffectsIceDragon>().flames.main;
        main.duration+= specialStatChanges[0];

        if(particleSystemWasPlaying) GetComponent<VariationEffectsIceDragon>().flames.Play();
        Debug.Log("Bought Special0");
    }
    public void SetSpecial1()
    {
        GetComponent<Stats>().damage += Mathf.RoundToInt(specialStatChanges[1]);
        GetComponent<VariationEffectsIceDragon>().flames.gameObject.GetComponent<DamageToEnemy>().damage = GetComponent<Stats>().damage;
        Debug.Log("Bought Special1");
    }
    public void SetSpecial2()
    {
        GetComponent<VariationEffectsIceDragon>().iceSpikeFieldDuration += specialStatChanges[2];
        Debug.Log("Bought Special2");
    }
    public void SetSpecial3()
    {
        GetComponent<VariationEffectsIceDragon>().iceSpikeFieldCooldown += Mathf.RoundToInt(specialStatChanges[3]);
        Debug.Log("Bought Special3");
    }
    public void SetSpecial4()
    {
        GetComponent<VariationEffectsIceDragon>().iceWallLives += Mathf.RoundToInt(specialStatChanges[4]);
        Debug.Log("Bought Special4");
    }
    public void SetSpecial5()
    {
        GetComponent<VariationEffectsIceDragon>().iceWallCooldown += Mathf.RoundToInt(specialStatChanges[5]);
        Debug.Log("Bought Special5");
    }

    public void SetSpecialInfoTexts()
    {
        specialInfoTexts[0] = "Dragon Breath Duration: +" + specialStatChanges[0] + "s";
        specialInfoTexts[1] = "Damage: +" + specialStatChanges[1];
        specialInfoTexts[2] = "Ice Spike Field Duration: +" + specialStatChanges[2] + "s";
        specialInfoTexts[3] = "Ice Spike Field Cooldown: " + specialStatChanges[3] + "s";
        specialInfoTexts[4] = "Ice Wall Lives: +" + specialStatChanges[4];
        specialInfoTexts[5] = "Ice Wall Cooldown: " + specialStatChanges[5] + "s";
    }
}
