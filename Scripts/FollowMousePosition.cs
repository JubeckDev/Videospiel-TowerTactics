using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowMousePosition : MonoBehaviour
{
    public Camera mainCam;

    public LayerMask targetLayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, targetLayer))
        {
            transform.position = raycastHit.point;
        }
    }
}
