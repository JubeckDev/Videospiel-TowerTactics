using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEffects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUpgradeEffect(int number)
    {
        if (GetComponent<Enhancements>().upgradeOptions[number] == 0)
        {
            //Debug.Log("More Damage");
            GetComponent<Stats>().damage += Mathf.RoundToInt(GetComponent<Enhancements>().statIncreases[number]);
        }
        if (GetComponent<Enhancements>().upgradeOptions[number] == 1)
        {
            //Debug.Log("More Range");
            GetComponent<Stats>().range += GetComponent<Enhancements>().statIncreases[number];
            GetComponent<Stats>().UpdateRange();
        }
        if (GetComponent<Enhancements>().upgradeOptions[number] == 2)
        {
            //Debug.Log("More Attack speed");
            GetComponent<Stats>().attackSpeed += GetComponent<Enhancements>().statIncreases[number];
        }
        if (GetComponent<Enhancements>().upgradeOptions[number] == 3)
        {
            //Debug.Log("More Piercing");
            GetComponent<Stats>().piercing += Mathf.RoundToInt(GetComponent<Enhancements>().statIncreases[number]);
        }
        if (GetComponent<Enhancements>().upgradeOptions[number] == 4)
        {
            //Debug.Log("More Bullet movement speed");
            GetComponent<Stats>().bulletMovementSpeed += GetComponent<Enhancements>().statIncreases[number];
        }
    }
}
