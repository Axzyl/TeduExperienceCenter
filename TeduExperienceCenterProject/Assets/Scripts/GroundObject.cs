using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * 100), Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
    }
}
