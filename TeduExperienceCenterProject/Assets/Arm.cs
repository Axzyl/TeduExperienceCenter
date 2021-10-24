using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] Transform targetPoint;
    [SerializeField] Transform basePoint;
    [SerializeField] Transform arm1;
    [SerializeField] Transform arm2;

    Vector3 relativePoint;

    // Start is called before the first frame update
    void Start()
    {
        // Calibration
        relativePoint = targetPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        arm1.LookAt(targetPoint);
        arm2.LookAt(basePoint);
        Debug.Log(Vector3.Distance(targetPoint.position, basePoint.position));
    }
}
