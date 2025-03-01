using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float minHeight;
    public float maxHeight;

    public float minRotation;
    public float maxRotation;

    public float scrollSensitiviy;

    public float movementSpeed;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            SetHeight();
            SetRotation();
            Move();
        }
    }
    public void SetHeight()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.position -= Vector3.up * Input.GetAxis("Mouse ScrollWheel") * scrollSensitiviy * Time.deltaTime / Time.timeScale;
        }
        if (transform.position.y > maxHeight)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
        if (minHeight > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
        }
    }
    public void SetRotation()
    {
        float heightRange = maxHeight - minHeight;
        float rotationRange = maxRotation - minRotation;

        float heightAboveMinimum = transform.position.y - minHeight;

        transform.eulerAngles = new Vector3(minRotation + (heightAboveMinimum / heightRange * rotationRange), transform.eulerAngles.y, transform.eulerAngles.z);
    }
    public void Move()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xDirection, 0, zDirection);

        transform.position += moveDirection * movementSpeed * Time.deltaTime / Time.timeScale;

        if(minX > transform.position.x)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        if (minZ > transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
        if (transform.position.z > maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }
    }
}
