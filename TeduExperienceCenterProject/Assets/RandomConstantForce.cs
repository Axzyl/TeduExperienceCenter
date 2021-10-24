using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomConstantForce : MonoBehaviour
{
    float a = 0;
    float b = 34;
    float c = 168;
    Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        transform.position = startingPos + (Vector3.up * Mathf.Sin(a) * 0.25f) + (Vector3.right * Mathf.Sin(b) * 0.2f) + (Vector3.forward * Mathf.Sin(c) * 0.01f);
        a += Time.deltaTime * 0.4f;
        b += Time.deltaTime * 0.5f;
        c += Time.deltaTime * 0.1f;
    }
}
