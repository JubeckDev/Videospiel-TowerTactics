using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointerFollow : MonoBehaviour
{
    public GameObject target;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.position = target.transform.position;
            transform.position += offset;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AssignTarget(GameObject target)
    {
        this.target = target; 
    }
}
