using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] Transform controllerTarget;

    bool freeze;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleFreeze(bool f)
    {
        freeze = f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {
            transform.position = controllerTarget.position;
            transform.rotation = controllerTarget.rotation;
        }
    }
}
