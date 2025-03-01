using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLook : MonoBehaviour
{
    public GameObject range;
    public GameObject enemy;
    public Vector3 rotationOffSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //AssignEnemyAndLook();
    }
    public void AssignEnemyAndLook()
    {
        enemy = range.GetComponent<Range>().enemy;

        if (enemy)
        {
            transform.LookAt(enemy.transform.position);
            transform.Rotate(rotationOffSet);
        }
    }
}
