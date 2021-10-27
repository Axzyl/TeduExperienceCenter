using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = -transform.GetChild(0).localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
