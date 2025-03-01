using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enhancements : MonoBehaviour
{
    public int upgradePointsMax = 3;

    public int upgradePointsLeft;
    public bool hasBoughtVariation;
    public bool hasBoughtSpecial;

    public string[] variationNames;
    public string[] specialNames;

    public string[] upgradeOptionPossibilities;
    public int[] upgradeOptions;
    public float[] statIncreases;

    public Sprite[] variationSprites;
    public Sprite[] specialSprites;

    public int[] goldCosts;

    [Header("Current Upgrades bought")]
    public int[] upgradeLevels;
    public int variationBought = 10;
    public int specialBought = 10;

    // Start is called before the first frame update
    void Start()
    {
        upgradePointsLeft = upgradePointsMax;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
