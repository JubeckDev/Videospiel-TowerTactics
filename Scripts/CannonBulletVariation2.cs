using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBulletVariation2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float splitBulletParentSize = GetComponent<Bullet>().tower.GetComponent<VariationEffectsCannon>().splitBulletParentSize;
        transform.localScale = new Vector3(splitBulletParentSize, splitBulletParentSize, splitBulletParentSize);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
