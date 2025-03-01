using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLook : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AssignEnemyAndLook();
    }
    public void AssignEnemyAndLook()
    {
        enemy = GetComponent<Stats>().rangeDisplay.GetComponent<Range>().enemy;
        if (enemy)
        {
            var lookPos = enemy.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);
            transform.Rotate(0, 180, 0);
        }
    }
}
