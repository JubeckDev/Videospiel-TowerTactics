using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public GameObject enemy;

    public Vector3 offset;

    public float offsetRotation;

    public GameObject rotateObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotateObject)
        {
            if (rotateObject.GetComponent<DragonLook>()) rotateObject.GetComponent<DragonLook>().AssignEnemyAndLook();
            if (rotateObject.GetComponent<ShipLook>()) rotateObject.GetComponent<ShipLook>().AssignEnemyAndLook();
        }

        if (enemy != null) 
        {
            transform.LookAt(enemy.transform.position + offset);
            transform.eulerAngles += new Vector3(offsetRotation,transform.rotation.y, transform.rotation.z);
        }
    }
    public void AssignEnemy(GameObject enemy)
    {
        this.enemy = enemy;
    }
}