using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(GetComponent<Bullet>().tower.GetComponent<VariationEffectsBallista>().arrowSize, GetComponent<Bullet>().tower.GetComponent<VariationEffectsBallista>().arrowSize, GetComponent<Bullet>().tower.GetComponent<VariationEffectsBallista>().arrowSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
