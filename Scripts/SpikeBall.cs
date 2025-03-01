using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        float splitBulletSize = GetComponent<Bullet>().tower.GetComponent<VariationEffectsCannon>().splitBulletSize;
        transform.localScale = new Vector3(splitBulletSize, splitBulletSize, splitBulletSize);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
