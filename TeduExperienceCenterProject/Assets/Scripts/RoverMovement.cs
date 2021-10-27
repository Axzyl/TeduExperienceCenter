using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("Vertical") + " " + Input.GetAxis("Horizontal"));
        GetComponent<Rigidbody>().AddForce(transform.forward * Input.GetAxis("Vertical") * 3);
        transform.Rotate(Vector3.up * Time.deltaTime * 5 * Input.GetAxis("Horizontal"));
    }
}
