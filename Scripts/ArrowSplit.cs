using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSplit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float arrowSplitSize = GetComponent<Bullet>().tower.GetComponent<VariationEffectsBallista>().arrowSplitSize;
        transform.localScale = new Vector3(arrowSplitSize, arrowSplitSize, arrowSplitSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
